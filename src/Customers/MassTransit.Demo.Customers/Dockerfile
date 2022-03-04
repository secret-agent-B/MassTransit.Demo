#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Customers/MassTransit.Demo.Customers/MassTransit.Demo.Customers.csproj", "src/Customers/MassTransit.Demo.Customers/"]
COPY ["src/Customers/MassTransit.Demo.Customers.Contracts/MassTransit.Demo.Customers.Contracts.csproj", "src/Customers/MassTransit.Demo.Customers.Contracts/"]
COPY ["src/SharedLib/MassTransit.Demo.Communication/MassTransit.Demo.Communication.csproj", "src/SharedLib/MassTransit.Demo.Communication/"]
RUN dotnet restore "src/Customers/MassTransit.Demo.Customers/MassTransit.Demo.Customers.csproj"
COPY . .
WORKDIR "/src/src/Customers/MassTransit.Demo.Customers"
RUN dotnet build "MassTransit.Demo.Customers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MassTransit.Demo.Customers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MassTransit.Demo.Customers.dll"]