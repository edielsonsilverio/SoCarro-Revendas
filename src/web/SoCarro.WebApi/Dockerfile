FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./web/SoCarro.WebApi/SoCarro.WebApi.csproj", "web/SoCarro.WebApi/"]
COPY ["./building blocks/SoCarro.Core/SoCarro.Core.csproj", "building blocks/SoCarro.Core/"]
COPY ["./building blocks/SoCarro.Core.WebApi/SoCarro.Core.WebApi.csproj", "building blocks/SoCarro.Core.WebApi/"]
COPY ["./services/SoCarro.DataAccess/SoCarro.DataAccess.csproj", "services/SoCarro.DataAccess/"]
COPY ["./services/SoCarro.Business/SoCarro.Business.csproj", "services/SoCarro.Business/"]
COPY ["./building blocks/SoCarro.MessageBus/SoCarro.MessageBus.csproj", "building blocks/SoCarro.MessageBus/"]

RUN dotnet restore "web/SoCarro.WebApi/SoCarro.WebApi.csproj"
COPY . .
WORKDIR "web/SoCarro.WebApi"
RUN dotnet build "SoCarro.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoCarro.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoCarro.WebApi.dll"]