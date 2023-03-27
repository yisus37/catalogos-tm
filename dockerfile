FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8005

ENV ASPNETCORE_URLS=http://+:8005

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["catalogos/catalogos.csproj", "catalogos/"]
#COPY ["dbModel/dbModel.csproj", "dbModel/"]


RUN dotnet restore "catalogos/catalogos.csproj"
COPY . .
WORKDIR "/src/catalogos"
RUN dotnet build "catalogos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "catalogos.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "catalogos.dll"]
