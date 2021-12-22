#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MassTransit.Demo.Notifications/MassTransit.Demo.Notifications.csproj", "src/MassTransit.Demo.Notifications/"]
COPY ["src/MassTransit.Demo.Customers.Contracts/MassTransit.Demo.Customers.Contracts.csproj", "src/MassTransit.Demo.Customers.Contracts/"]
COPY ["src/MassTransit.Demo.Orders.Contracts/MassTransit.Demo.Orders.Contracts.csproj", "src/MassTransit.Demo.Orders.Contracts/"]
COPY ["src/MassTransit.Demo.Communication/MassTransit.Demo.Communication.csproj", "src/MassTransit.Demo.Communication/"]
RUN dotnet restore "src/MassTransit.Demo.Notifications/MassTransit.Demo.Notifications.csproj"
COPY . .
WORKDIR "/src/src/MassTransit.Demo.Notifications"
RUN dotnet build "MassTransit.Demo.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Demo.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Demo.Notifications.dll"]