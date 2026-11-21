# VPS Deployment Guide — Viviliam API (apis.viviliam.com)

> Last updated: 2026-11-21

## Overview

This guide covers deploying the **Viviliam API** (`vivapis`) on a Linux VPS using Docker Compose with Nginx reverse proxy and free SSL via Let's Encrypt.

**Key changes from previous setup:**
- Domain changed from `api.viviliam.com` → `apis.viviliam.com`
- SQL Server **removed** from VPS (no user login page on backend)
- Bible data (`ViviliamConnection`) remains in appsettings.json for local dev only — the VPS deployment does not require a database

---

## Prerequisites

| Item | Requirement |
|------|-------------|
| OS | Ubuntu 22.04 LTS (or newer) |
| Docker | ≥ 24.0 |
| Docker Compose | ≥ 2.20 |
| Domain | `apis.viviliam.com` pointing to VPS IP via A record |

### DNS Configuration

```
Type   Name               Value             TTL
A      apis.viviliam.com  <YOUR_VPS_IP>    3600
```

---

## Step 1: Install Docker & Docker Compose

```bash
# Update system
sudo apt update && sudo apt upgrade -y

# Install Docker
curl -fsSL https://get.docker.com | sh

# Add user to docker group (skip if running as root)
sudo usermod -aG docker $USER
newgrp docker

# Verify
docker --version
docker compose version
```

---

## Step 2: Clone & Prepare the Project

```bash
# Navigate to working directory
cd /opt

# Clone the repository
git clone https://github.com/<your-org>/vivapis.git
cd vivapis
```

### File Structure

```
vivapis/
├── WebApi/
│   ├── appsettings.json          # ViviliamConnection (local dev only)
│   ├── Dockerfile
│   └── Notifications/            # XML content files
├── nginx.conf                    # SSL + reverse proxy config
├── docker-compose.yml            # API + Nginx containers
├── .env                          # Secrets (never commit)
└── LINUX_VPS_SETUP.md           # This file
```

---

## Step 3: Configure Nginx

The `nginx.conf` is already configured for `apis.viviliam.com`:

```nginx
server {
    listen 80;
    server_name apis.viviliam.com;
    return 301 https://$host$request_uri;
}

server {
    listen 443 ssl;
    server_name apis.viviliam.com;

    ssl_certificate     /etc/letsencrypt/live/apis.viviliam.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/apis.viviliam.com/privkey.pem;

    client_max_body_size 50m;

    location / {
        proxy_pass http://api:8080;
        proxy_http_version 1.1;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto https;
        proxy_set_header X-Forwarded-Host $host;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
    }
}
```

---

## Step 4: Obtain SSL Certificate

```bash
# Install Certbot
sudo apt install certbot python3-certbot-nginx -y

# Obtain certificate (standalone mode since port 443 may be in use)
sudo certbot certonly --standalone -d apis.viviliam.com --email your-email@example.com --agree-tos --non-interactive

# Verify certificate files exist
ls -la /etc/letsencrypt/live/apis.viviliam.com/
```

### Auto-Renewal (Recommended)

Certbot installs a systemd timer that runs twice daily at ~00:00 and ~12:00. When a certificate is within 30 days of expiry it renews automatically.

```bash
# Verify the certbot renewal timer is active
sudo systemctl list-timers | grep certbot

# Expected output:
# Wed 2026-11-25 00:00:00 UTC  ...  certbot.timer          certbot.service
```

#### Hook Nginx to reload after renewal

Create a hook script so Nginx picks up new certs without a manual restart:

```bash
sudo tee /etc/letsencrypt/live/apis.viviliam.com/reload-nginx.sh << 'EOF'
#!/bin/bash
docker exec vivapis-nginx nginx -s reload
echo "$(date): SSL renewed and Nginx reloaded" >> /var/log/certbot-renew.log
EOF
chmod +x /etc/letsencrypt/live/apis.viviliam.com/reload-nginx.sh
```

Add the `--deploy-hook` to the certbot renewal command:

```bash
sudo certbot renew --deploy-hook "/etc/letsencrypt/live/apis.viviliam.com/reload-nginx.sh" --dry-run
```

> **Note:** The above flag works for manual renewal runs. For the systemd timer, add the hook via a config file instead (see below).

#### Configure renewal hook for the systemd timer

Create `/etc/letsencrypt/renewal/apis.viviliam.com.conf` with this line inside the `[renewalparams]` section:

```ini
deploy_hook = /etc/letsencrypt/live/apis.viviliam.com/reload-nginx.sh
```

#### Test the full renewal flow

```bash
# Dry run (no actual renewal, tests hook execution)
sudo certbot renew --dry-run

# Check the log after the dry run
sudo tail -5 /var/log/letsencrypt/letsencrypt.log
```

#### Manual renewal (if needed)

```bash
sudo certbot renew
sudo docker exec vivapis-nginx nginx -s reload
```

#### Verify certificate expiry

```bash
echo | openssl s_client -connect apis.viviliam.com:443 -servername apis.viviliam.com 2>/dev/null \
  | openssl x509 -noout -dates -serial
# Expected output:
# notBefore=Nov 21 00:00:00 2026 GMT
# notAfter=Feb 19 00:00:00 2027 GMT
```

#### Summary of renewal behavior

| Timer | Runs at | Action if < 30 days to expiry |
|-------|---------|-------------------------------|
| `certbot.timer` | ~00:00 & ~12:00 UTC | Renew + reload Nginx via hook |

Certificate lifespan from Let's Encrypt: **90 days**. Renewal typically triggers at **30 days** remaining.

---

## Step 5: Configure docker-compose.yml

Create or update `docker-compose.yml`:

```yaml
version: '3.8'

services:
  api:
    build:
      context: ./WebApi
      dockerfile: Dockerfile
    container_name: vivapis-api
    restart: unless-stopped
    expose:
      - "8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:8080
    volumes:
      - ./WebApi/logs:/app/logs              # Serilog file output
      - ./WebApi/Notifications:/app/Notifications   # XML content mount
    networks:
      - vivapis-network

  nginx:
    image: nginx:alpine
    container_name: vivapis-nginx
    restart: unless-stopped
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx.conf:/etc/nginx/nginx.conf:ro
      - /etc/letsencrypt/live/apis.viviliam.com:/etc/letsencrypt/live/apis.viviliam.com:ro
      - certbot-conf:/etc/letsencrypt/conf.d
    depends_on:
      - api
    networks:
      - vivapis-network

networks:
  vivapis-network:
    driver: bridge

volumes:
  certbot-conf:
```

---

## Step 6: Build & Deploy

```bash
# Build and start in background
docker compose up -d --build

# Check logs
docker compose logs -f api

# Verify containers running
docker ps
```

Expected output:
```
vivapis-api        Api container listening on http://+:8080
vivapis-nginx      Nginx serving HTTPS on port 443
```

---

## Step 7: Verify Deployment

```bash
# Check API health (internal to Docker network)
curl -k https://localhost/health   # If health endpoint exists

# Check from outside
curl -I https://apis.viviliam.com
# Expected: HTTP/2 200

# Verify SSL certificate
echo | openssl s_client -connect apis.viviliam.com:443 -servername apis.viviliam.com 2>/dev/null | openssl x509 -noout -dates
```

---

## Step 8: Database (Local Development Only)

The `ViviliamConnection` in `appsettings.json` points to **local SQL Server** for Bible data (BSI, KJV, AMP translation tables). This is only needed during local development.

### Local dev connection string:
```json
"ViviliamConnection": "Server=FRED-ACER\\SQLEXPRESS;Database=Viviliam_Web;persist security info=True; Integrated Security=SSPI; TrustServerCertificate=True;"
```

**The VPS does not need SQL Server.** The connection string remains in `appsettings.json` for local debugging but is **not used** on the production server. If Bible data access becomes needed in production, add a remote SQL Server and update the connection string accordingly.

---

## Maintenance

### View Logs

```bash
# API logs (Serilog files)
docker exec vivapis-api cat /app/logs/Log-$(date +%Y%m).txt

# Docker compose logs
docker compose logs -f api
docker compose logs -f nginx

# Latest entries only
docker compose logs --tail=100 -f api
```

### Restart Services

```bash
docker compose restart
docker compose restart nginx     # Nginx-only restart
```

### Update & Redeploy

```bash
git pull
docker compose up -d --build
```

### SSL Certificate Renewal

```bash
# Manual renewal (auto-renew runs via cron)
sudo certbot renew
sudo docker exec vivapis-nginx nginx -s reload
```

### Monitor Disk Space

```bash
# Check volume mounts usage
df -h /var/lib/docker

# Clean unused images
docker image prune -a
```

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| 502 Bad Gateway | Check API container: `docker logs vivapis-api`; verify API is listening on port 8080 |
| SSL error | Verify cert files exist at `/etc/letsencrypt/live/apis.viviliam.com/`; run `certbot renew` |
| DNS not resolving | Check A record for `apis.viviliam.com` points to VPS IP; use `nslookup apis.viviliam.com` |
| CORS errors in frontend | CORS policy is hardcoded in `Program.cs` — add the frontend origin if missing |
| Swagger unavailable | Swagger is disabled in Production (`ASPNETCORE_ENVIRONMENT=Production`) |
| XML content not loading | Verify volume mount: `./WebApi/Notifications:/app/Notifications` |

---

## Security Notes

1. **Never commit** `.env`, `appsettings.Production.json`, SSL certs, or database credentials
2. Firewall only ports 80 and 443 — block all other inbound ports
3. Set up fail2ban for SSH protection: `sudo apt install fail2ban -y`
4. Keep Docker and OS updated regularly
