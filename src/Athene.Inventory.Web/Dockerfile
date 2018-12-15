FROM microsoft/dotnet:2.1.401-sdk-alpine as build

RUN apk update && \
    apk add sqlite && \
    rm -rf /etc/apk/repositories

COPY . /src

WORKDIR /src/src/Athene.Inventory.Web/
RUN dotnet restore Athene.Inventory.Web.csproj
# RUN dotnet build Athene.Inventory.Web.csproj -c Release -o /app

FROM build as publish
# RUN dotnet ef database update --startup-project Athene.Inventory.Web.csproj
RUN dotnet publish Athene.Inventory.Web.csproj --no-restore -c Release -o /app

FROM microsoft/dotnet:2.1.5-aspnetcore-runtime-alpine AS base
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Athene.Inventory.Web.dll"]