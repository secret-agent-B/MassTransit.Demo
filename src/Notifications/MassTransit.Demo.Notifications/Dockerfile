#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Notifications/MassTransit.Demo.Notifications/MassTransit.Demo.Notifications.csproj", "src/Notifications/MassTransit.Demo.Notifications/"]
COPY ["src/Customers/MassTransit.Demo.Customers.Contracts/MassTransit.Demo.Customers.Contracts.csproj", "src/Customers/MassTransit.Demo.Customers.Contracts/"]
COPY ["src/SharedLib/MassTransit.Demo.Communication/MassTransit.Demo.Communication.csproj", "src/SharedLib/MassTransit.Demo.Communication/"]
RUN dotnet restore "src/Notifications/MassTransit.Demo.Notifications/MassTransit.Demo.Notifications.csproj"
COPY . .
WORKDIR "/src/src/Notifications/MassTransit.Demo.Notifications"
RUN dotnet build "MassTransit.Demo.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Demo.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Demo.Notifications.dll"]