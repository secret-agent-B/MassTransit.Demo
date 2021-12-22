#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MassTransit.Demo.Orders/MassTransit.Demo.Orders.csproj", "src/MassTransit.Demo.Orders/"]
COPY ["src/MassTransit.Demo.Orders.Contracts/MassTransit.Demo.Orders.Contracts.csproj", "src/MassTransit.Demo.Orders.Contracts/"]
COPY ["src/MassTransit.Demo.Communication/MassTransit.Demo.Communication.csproj", "src/MassTransit.Demo.Communication/"]
RUN dotnet restore "src/MassTransit.Demo.Orders/MassTransit.Demo.Orders.csproj"
COPY . .
WORKDIR "/src/src/MassTransit.Demo.Orders"
RUN dotnet build "MassTransit.Demo.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Demo.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Demo.Orders.dll"]