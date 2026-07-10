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

# Hugging Face Spaces exécute le conteneur en non-root (uid 1000) :
# un utilisateur dédié avec un HOME inscriptible évite les erreurs
# de permissions (clés DataProtection d'ASP.NET, notamment).
RUN useradd -m -u 1000 appuser
USER appuser
ENV HOME=/home/appuser

WORKDIR /app
COPY --from=build --chown=appuser /app/publish .

# L'image aspnet écoute par défaut sur 0.0.0.0:8080 (ASPNETCORE_HTTP_PORTS) ;
# render.yaml (PORT=8080) et README.md (app_port: 8080) routent vers ce port.
EXPOSE 8080
ENTRYPOINT ["dotnet", "MotorsportHub.Web.dll"]
