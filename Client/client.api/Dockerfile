FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Client/client.api/client.api/client.api.csproj", "client.api/"]
RUN dotnet restore "Client/client.api/client.api/client.api.csproj"
COPY . .
WORKDIR "/src/client.api"
RUN dotnet build "client.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "client.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "client.api.dll"]
