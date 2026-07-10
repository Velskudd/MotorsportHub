# ── Étape de build : SDK .NET 10 ────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copier d'abord les fichiers projet seuls pour mettre en cache le restore
COPY MotorsportHub.sln ./
COPY src/MotorsportHub.Domain/MotorsportHub.Domain.csproj src/MotorsportHub.Domain/
COPY src/MotorsportHub.Application/MotorsportHub.Application.csproj src/MotorsportHub.Application/
COPY src/MotorsportHub.Infrastructure/MotorsportHub.Infrastructure.csproj src/MotorsportHub.Infrastructure/
COPY src/MotorsportHub.Web/MotorsportHub.Web.csproj src/MotorsportHub.Web/
RUN dotnet restore MotorsportHub.sln

COPY . .
RUN dotnet publish src/MotorsportHub.Web/MotorsportHub.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

# ── Étape d'exécution : runtime ASP.NET Core seul ───────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# L'image aspnet écoute par défaut sur 0.0.0.0:8080 (ASPNETCORE_HTTP_PORTS) ;
# render.yaml déclare PORT=8080 pour que Render route le trafic vers ce port.
EXPOSE 8080
ENTRYPOINT ["dotnet", "MotorsportHub.Web.dll"]
