using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace ViviliamApi.Controllers
{
    [Route("")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private IWebHostEnvironment Environment;

        private readonly IConfiguration _configuration;


        public NotificationsController(IWebHostEnvironment _environment, IConfiguration configuration)
        {
            Environment = _environment;
            _configuration = configuration;
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/signup/{username=username}/{emailid=emailid}/{mobile=mobile}/{location=location}/{password=password}/{device=device}")]
        //[Route("api/signup")]
        //[HttpGet]
        public IActionResult signup(string username, string emailid, string password, string device)
        {
            /*
            string mobile = "";
            string location = "";
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_signup_new", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@emailid", SqlDbType.VarChar).Value = emailid;
                        cmd.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile;
                        cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = location;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                    if (dtTemp.Rows[0]["status"].ToString() == "OK")
                    {
                        var filePath = Path.Combine(this.Environment.ContentRootPath, @"MailTemplates/signup_email_verification.txt");
                        StreamReader sr = new StreamReader(filePath);
                        string mail_content = sr.ReadToEnd();
                        mail_content = mail_content.Replace("#verificationcode#", dtTemp.Rows[0]["verification_code"].ToString());
                        sendmail(emailid, mail_content, "Viviliam account verification code " + dtTemp.Rows[0]["verification_code"].ToString());
                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        [Route("api/sendmail")]
        [HttpGet]
        public void sendmail(string strTo, string strBodyMessage, string subject)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtpout.secureserver.net", 587);

                smtpClient.Credentials = new System.Net.NetworkCredential("support@viviliam.com", "#V!V!lm2023");
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("support@viviliam.com", "Viviliam");
                mail.To.Add(new MailAddress(strTo));
                mail.Subject = subject;
                smtpClient.Timeout = 200000;
                mail.Body = strBodyMessage;
                smtpClient.Send(mail);

            }
            catch (Exception)
            {
                throw;
            }
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/edit_profile/{userid=userid}/{username=username}/{emailid=emailid}/{mobile=mobile}/{location=location}/{gender=gender}/{dob=dob}/{device=device}")]
        //[Route("api/edit_profile")]
        //[HttpGet]
        public IActionResult edit_profile(string userid = "", string username = "", string mobile = "", string location = "", string gender = "", string dob = "", string device = "")
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_signup_edit", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile;
                        cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = location;
                        cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender;
                        cmd.Parameters.Add("@dob", SqlDbType.VarChar).Value = dob;

                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }
                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/signup_details/{userid=userid}/{device=device}")]
        //[Route("api/signup_details")]
        //[HttpGet]
        public IActionResult signup_details(string userid, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("[sp_user_details]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }
                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/signup_verification/{userid=userid}/{device=device}")]
        //[Route("api/signup_verification")]
        //[HttpGet]
        public IActionResult signup_verification(string userid, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("[sp_signup_update_verification]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }
                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/delete_account/{userid=userid}/{device=device}")]
        //[Route("api/delete_account")]
        //[HttpGet]
        public IActionResult delete_account(string userid, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("[sp_user_account_delete]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                        if (dtTemp.Rows[0]["status"].ToString() == "OK")
                        {
                            var filePath = Path.Combine(this.Environment.ContentRootPath, @"MailTemplates/account_delete_notification.txt");

                            StreamReader sr = new StreamReader(filePath);
                            string mail_content = sr.ReadToEnd();

                            sendmail(dtTemp.Rows[0]["mailid"].ToString() ?? string.Empty, mail_content, "Viviliam Account Deletion Confirmation");
                        }
                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }
        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/user_login_check/{email=email}/{password=password}/{device=device}")]
        //[Route("api/user_login_check")]
        //[HttpGet]
        public IActionResult user_login_check(string email, string password, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("[sp_user_logincheck]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@emailid", SqlDbType.VarChar).Value = email;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/user_change_password/{userid=userid}/{oldpassword=oldpassword}/{password=password}/{retypepassword=retypepassword}/{medhotd=method}/{device=device}")]
        //[Route("api/user_change_password")]
        //[HttpGet]
        public IActionResult user_change_password(string userid, string password, string method, string device, string oldpassword = "")
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("[sp_user_changepassword]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@oldpassword", SqlDbType.VarChar).Value = oldpassword;
                        cmd.Parameters.Add("@newpassword", SqlDbType.VarChar).Value = password;
                        cmd.Parameters.Add("@retypepassword", SqlDbType.VarChar).Value = password;
                        cmd.Parameters.Add("@method", SqlDbType.VarChar).Value = method;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/signup_edit/{userid=userid}/{username=username}/{mobile=mobile}/{location=location}/{device=device}")]
        //[Route("api/signup_edit")]
        //[HttpGet]
        public IActionResult signup_edit(string userid = "", string username = "", string mobile = "", string location = "", string device = "")
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_signup_edit", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@mobile", SqlDbType.VarChar).Value = mobile;
                        cmd.Parameters.Add("@location", SqlDbType.VarChar).Value = location;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);
                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/generate_verification_code/{userid=userid}/{method=method}/{device=device}")]
        //[Route("api/generate_verification_code")]
        //[HttpGet]
        public IActionResult generate_verification_code(string userid, string method, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_generate_verification_code", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@method", SqlDbType.VarChar).Value = method;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }
                if (dtTemp.Rows[0]["status"].ToString() == "OK")
                {
                    var filePath = Path.Combine(this.Environment.ContentRootPath, @"MailTemplates/password_reset_verification.txt");
                    StreamReader sr = new StreamReader(filePath);
                    string mail_content = sr.ReadToEnd();
                    mail_content = mail_content.Replace("#verificationcode#", dtTemp.Rows[0]["verification_code"].ToString());
                    sendmail(dtTemp.Rows[0]["emailid"].ToString() ?? string.Empty, mail_content, "Viviliam account reset verification code " + dtTemp.Rows[0]["verification_code"].ToString());
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/backup_data/{userid=userid}/{bibleid=bibleid}/{booknumber=booknumber}/{cnumber=cnumber}/{vnumber=vnumber}/{method=texthighlight}/{notes=notes}/{bookmark=bookmark}/{highlighton=highlighton}/{noteson=noteson}/{bookmarkon=bookmarkon}/{device=device}/{deleteflag=deleteflag}")]
        //[Route("api/backup_data")]
        //[HttpGet]
        public IActionResult backup_data(string userid = "", string bibleid = "", string booknumber = "", string cnumber = "", string vnumber = "", string texthighlight = "", string notes = "", string bookmark = "", string highlighton = "", string noteson = "", string bookmarkon = "", string device = "", string deleteflag = "")
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_backup_data_2", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@bibleid", SqlDbType.VarChar).Value = bibleid;
                        cmd.Parameters.Add("@booknumber", SqlDbType.VarChar).Value = booknumber;
                        cmd.Parameters.Add("@cnumber", SqlDbType.VarChar).Value = cnumber;
                        cmd.Parameters.Add("@vnumber", SqlDbType.VarChar).Value = vnumber;
                        cmd.Parameters.Add("@texthighlight", SqlDbType.VarChar).Value = texthighlight;
                        cmd.Parameters.Add("@notes", SqlDbType.NVarChar, -1).Value = notes;
                        cmd.Parameters.Add("@bookmark", SqlDbType.VarChar).Value = bookmark;
                        cmd.Parameters.Add("@highlighton", SqlDbType.VarChar).Value = highlighton;
                        cmd.Parameters.Add("@noteson", SqlDbType.VarChar).Value = noteson;
                        cmd.Parameters.Add("@bookmarkon", SqlDbType.VarChar).Value = bookmarkon;
                        cmd.Parameters.Add("@deleteflag", SqlDbType.VarChar).Value = deleteflag;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/get_lastbackup/{userid=userid}/{device=device}")]
        //[Route("api/get_lastbackup")]
        //[HttpGet]
        public IActionResult get_lastbackup(string userid, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_get_last_backup_date", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/restoredata/{userid=userid}/{method=method}/{device=device}")]
        //[Route("api/restoredata")]
        //[HttpGet]
        public IActionResult restoredata(string userid, string method, string device)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_get_backup_data", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
                        cmd.Parameters.Add("@method", SqlDbType.VarChar).Value = method;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }


                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/dailyquote/{month=month}/{day=day}/{device=device}/{language=language}")]
        //[Route("api/dailyquote")]
        //[HttpGet]
        public IActionResult dailyquote(string month, string day, string device, string language)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_get_bible_QUOTE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = month;
                        cmd.Parameters.Add("@day", SqlDbType.VarChar).Value = day;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;
                        cmd.Parameters.Add("@language", SqlDbType.VarChar).Value = language;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/dailyverse/{month=month}/{day=day}/{device=device}/{language=language}")]
        //[Route("api/dailyverse")]
        //[HttpGet]
        public IActionResult dailyverse(string month, string day, string device, string language)
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_get_bible_verse", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = month;
                        cmd.Parameters.Add("@day", SqlDbType.VarChar).Value = day;
                        cmd.Parameters.Add("@device", SqlDbType.VarChar).Value = device;
                        cmd.Parameters.Add("@language", SqlDbType.VarChar).Value = language;

                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        // REMOVED (2026-11-21): User-dependent endpoint — DefaultConnection removed; no SQL Server on VPS
        //[Route("api/updateUserResponse/{method=method}/{item_id=item_id}/{item_caption=item_caption}/{like_state=like_state}/{comments=comments}/{device_model=device_model}/{device_uuid=device_uuid}/{device_platform=device_platform}/{device_brand=device_brand}")]
        //[Route("api/updateUserResponse")]
        //[HttpGet]
        public IActionResult updateUserResponse(string method = "", string item_id = "", string item_caption = "", int like_state = 0, string comments = "", string device_model = "", string device_uuid = "", string device_platform = "", string device_brand = "")
        {
            /*
            try
            {
                DataTable dtTemp = new DataTable();
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {

                    using (SqlCommand cmd = new SqlCommand("sp_add_user_response", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@method", SqlDbType.VarChar).Value = method;
                        cmd.Parameters.Add("@item_id", SqlDbType.VarChar).Value = item_id;
                        cmd.Parameters.Add("@item_caption", SqlDbType.VarChar).Value = item_caption;
                        cmd.Parameters.Add("@like_state", SqlDbType.TinyInt).Value = like_state;
                        cmd.Parameters.Add("@comments", SqlDbType.VarChar).Value = comments;
                        cmd.Parameters.Add("@device_model", SqlDbType.VarChar).Value = device_model;
                        cmd.Parameters.Add("@device_uuid", SqlDbType.VarChar).Value = device_uuid;
                        cmd.Parameters.Add("@device_platform", SqlDbType.VarChar).Value = device_platform;
                        cmd.Parameters.Add("@device_brand", SqlDbType.VarChar).Value = device_brand;


                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtTemp);

                    }
                }


                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
            */
        }

        [Route("api/audios")]
        [HttpGet]
        public IActionResult audios()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp = ReadAudios("audios.xml");

                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [Route("api/audio")]
        [HttpGet]
        public IActionResult audio(string? date = null, string? type = null)
        {
            try
            {
                DataTable dtTemp = ReadAudio("audio.xml");

                // Filter by date and type
                IEnumerable<DataRow> filteredRows = dtTemp.AsEnumerable();

                if (!string.IsNullOrEmpty(date) && dtTemp.Columns.Contains("date"))
                {
                    filteredRows = filteredRows.Where(row => row.Field<string>("date") == date);
                }

                if (!string.IsNullOrEmpty(type) && dtTemp.Columns.Contains("type"))
                {
                    filteredRows = filteredRows.Where(row =>
                        string.Equals(row.Field<string>("type"), type, StringComparison.OrdinalIgnoreCase));
                }

                var audioList = filteredRows.Select(row => new
                {
                    type = row.Field<string>("type"),
                    date = row.Field<string>("date"),
                    url = row.Field<string>("url"),
                    duration = int.TryParse(row.Field<string>("duration"), out int d) ? d : 0,
                    formattedDuration = FormatDuration(int.TryParse(row.Field<string>("duration"), out int d2) ? d2 : 0)
                }).ToList();

                if (!string.IsNullOrEmpty(date) && audioList.Count == 0)
                {
                    var message = !string.IsNullOrEmpty(type)
                        ? $"No {type} audio found for {date}"
                        : $"No audio found for {date}";
                    return NotFound(new { error = message });
                }

                var response = new
                {
                    date = date,
                    count = audioList.Count,
                    audio = audioList
                };

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred: " + ex.Message });
            }
        }

        [Route("api/audio/types")]
        [HttpGet]
        public IActionResult audioTypes()
        {
            try
            {
                DataTable dtTemp = ReadAudio("audio.xml");
                var types = dtTemp.AsEnumerable()
                    .Select(row => row.Field<string>("type"))
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(t => t)
                    .ToList();

                return new JsonResult(new { types });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred: " + ex.Message });
            }
        }

        [Route("api/notifications")]
        [HttpGet]
        public IActionResult notifications()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp = ReadNotifications("notifications.xml");
                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [Route("api/events")]
        [HttpGet]
        public IActionResult events()
        {
            DataTable dtTemp = new DataTable();
            dtTemp = ReadEvents("events.xml");
            string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
            return Content(json, "application/json");
        }

        [Route("api/misc")]
        [HttpGet]
        public IActionResult misc()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp = ReadMisc("misc.xml");
                string json = JsonConvert.SerializeObject(dtTemp, Formatting.Indented);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [NonAction]
        public DataTable ReadEvents(string file)
        {
            DataTable table = new DataTable("Events");
            var filePath = Path.Combine(this.Environment.ContentRootPath, @"Notifications/" + file);
            DataSet lstNode = new DataSet();
            lstNode.ReadXml(filePath);
            table = lstNode.Tables["Events"]!;
            return table;
        }

        [NonAction]
        public DataTable ReadNotifications(string file)
        {
            DataTable table = new DataTable("Notifications");
            var filePath = Path.Combine(this.Environment.ContentRootPath, @"Notifications/" + file);
            DataSet lstNode = new DataSet();
            lstNode.ReadXml(filePath);
            table = lstNode.Tables["Notifications"]!;
            return table;
        }

        [NonAction]
        public DataTable ReadAudios(string file)
        {
            DataTable table = new DataTable("Audios");
            var filePath = Path.Combine(this.Environment.ContentRootPath, @"Notifications/" + file);
            DataSet lstNode = new DataSet();
            lstNode.ReadXml(filePath);
            table = lstNode.Tables["Audios"]!;
            return table;
        }

        [NonAction]
        public DataTable ReadAudio(string file)
        {
            DataTable table = new DataTable("Audio");
            var filePath = Path.Combine(this.Environment.ContentRootPath, @"Notifications/" + file);
            DataSet lstNode = new DataSet();
            lstNode.ReadXml(filePath);
            table = lstNode.Tables["Audio"]!;
            return table;
        }

        [NonAction]
        public DataTable ReadMisc(string file)
        {
            DataTable table = new DataTable("Misc");
            var filePath = Path.Combine(this.Environment.ContentRootPath, @"Notifications/" + file);
            DataSet lstNode = new DataSet();
            lstNode.ReadXml(filePath);
            table = lstNode.Tables["Misc"]!;
            return table;
        }

        [NonAction]
        public DataTable ReadEventsInstructions(string file)
        {
            DataTable table = new DataTable("EventsInstructions");
            var filePath = Path.Combine(this.Environment.ContentRootPath, "~/Notifications/" + file);
            DataSet lstNode = new DataSet();
            lstNode.ReadXml(filePath);
            table = lstNode.Tables["EventsInstructions"]!;
            return table;
        }

        [NonAction]
        public static HttpResponseMessage CreateJsonResponse(DataTable dt)
        {
            string json = DataTableToJSONWithStringBuilder(dt);
            return new HttpResponseMessage()
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

        }

        [NonAction]
        public static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }

        [NonAction]
        public string decodeBase64(string encodeString)
        {
            byte[] data = Convert.FromBase64String(encodeString);

            string decodedString = Encoding.UTF8.GetString(data);

            return decodedString;
        }

        [NonAction]
        private string FormatDuration(int seconds)
        {
            int mins = seconds / 60;
            int secs = seconds % 60;
            return $"{mins}:{secs.ToString("D2")}";
        }
    }
}
