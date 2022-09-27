#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

ARG BUILD_VERSION="1.0.0"

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["Crs.Backend/Crs.Backend.csproj", "Crs.Backend/"]
RUN dotnet restore "Crs.Backend/Crs.Backend.csproj"
COPY . .
WORKDIR "/src/Crs.Backend"
ARG BUILD_VERSION
RUN dotnet build "Crs.Backend.csproj" -c Release -o /app/build /p:Version=$BUILD_VERSION

FROM build AS publish
ARG BUILD_VERSION
RUN dotnet publish "Crs.Backend.csproj" -c Release -o /app/publish /p:Version=$BUILD_VERSION /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Crs.Backend.dll"]