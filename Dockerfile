FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WebApi.sln", "."]
COPY ["WebApi/ViviliamAPI.csproj", "WebApi/"]
COPY ["Model/Model.csproj", "Model/"]
RUN dotnet restore "WebApi/ViviliamAPI.csproj"

COPY . .
WORKDIR "/src/WebApi"
RUN dotnet publish "ViviliamAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
EXPOSE 8080

ENTRYPOINT ["dotnet", "ViviliamAPI.dll"]
