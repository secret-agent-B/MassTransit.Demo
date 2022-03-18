#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/1_Sending_Message/WebApiA/WebApiA.csproj", "src/1_Sending_Message/WebApiA/"]
COPY ["src/SharedLib/MassTransit.Demo.Shared/MassTransit.Demo.Shared.csproj", "src/SharedLib/MassTransit.Demo.Shared/"]
COPY ["src/SharedLib/MassTransit.Demo.Communication/MassTransit.Demo.Communication.csproj", "src/SharedLib/MassTransit.Demo.Communication/"]
RUN dotnet restore "src/1_Sending_Message/WebApiA/WebApiA.csproj"
COPY . .
WORKDIR "/src/src/1_Sending_Message/WebApiA"
RUN dotnet build "WebApiA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiA.dll"]