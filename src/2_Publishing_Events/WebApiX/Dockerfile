#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/2_Publishing_Events/WebApiX/WebApiX.csproj", "src/2_Publishing_Events/WebApiX/"]
COPY ["src/SharedLib/MassTransit.Demo.Shared/MassTransit.Demo.Shared.csproj", "src/SharedLib/MassTransit.Demo.Shared/"]
COPY ["src/SharedLib/MassTransit.Demo.Communication/MassTransit.Demo.Communication.csproj", "src/SharedLib/MassTransit.Demo.Communication/"]
RUN dotnet restore "src/2_Publishing_Events/WebApiX/WebApiX.csproj"
COPY . .
WORKDIR "/src/src/2_Publishing_Events/WebApiX"
RUN dotnet build "WebApiX.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiX.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiX.dll"]