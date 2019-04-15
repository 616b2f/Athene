FROM microsoft/dotnet:2.1.401-sdk-alpine as build
COPY . /src
WORKDIR /src/src/Athene.Inventory.Web/
RUN dotnet restore Athene.Inventory.Web.csproj
RUN dotnet publish Athene.Inventory.Web.csproj --no-restore -c Release -o /app

FROM microsoft/dotnet:2.1.5-aspnetcore-runtime-alpine AS base
RUN apk add --no-cache \
    sqlite \
    # add icu for globalization support
    icu-libs
# for more info see https://github.com/dotnet/corefx/blob/master/Documentation/architecture/globalization-invariant-mode.md
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=build /app .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Athene.Inventory.Web.dll"]