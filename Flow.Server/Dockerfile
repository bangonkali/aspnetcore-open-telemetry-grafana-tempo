FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build
WORKDIR /src
COPY ["Flow.Server/Flow.Server.csproj", "Flow.Server/"]
RUN dotnet restore "Flow.Server/Flow.Server.csproj"
COPY . .
WORKDIR "/src/Flow.Server"
RUN dotnet build "Flow.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flow.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flow.Server.dll"]
