FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./web/SoCarro.WebApp.MVC/SoCarro.WebApp.MVC.csproj", "web/SoCarro.WebApp.MVC/"]
#COPY ["./services/SoCarro.Business/SoCarro.Business.csproj", "services/SoCarro.Business/"]
#COPY ["./services/SoCarro.DataAccess/SoCarro.DataAccess.csproj", "services/SoCarro.DataAccess/"]
COPY ["./building blocks/SoCarro.Core/SoCarro.Core.csproj", "building blocks/SoCarro.Core/"]
COPY ["./building blocks/SoCarro.Core.WebApi/SoCarro.Core.WebApi.csproj", "building blocks/SoCarro.Core.WebApi/"]
COPY ["./building blocks/SoCarro.MessageBus/SoCarro.MessageBus.csproj", "building blocks/SoCarro.MessageBus/"]

RUN dotnet restore "web/SoCarro.WebApp.MVC/SoCarro.WebApp.MVC.csproj"
COPY . .
WORKDIR "web/SoCarro.WebApp.MVC"
RUN dotnet build "SoCarro.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoCarro.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoCarro.WebApp.MVC.dll"]