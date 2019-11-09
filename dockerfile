FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY CoreApplication.sln ./
COPY CoreApplication.Middlewares/*.csproj ./CoreApplication.Middlewares/
COPY CoreApplication.DTO/*.csproj ./CoreApplication.DTO/
COPY CoreApplication.Common/*.csproj ./CoreApplication.Common/
COPY CoreApplication.API/*.csproj ./CoreApplication.API/

RUN dotnet restore
COPY . .

WORKDIR /src/CoreApplication.Middlewares
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.DTO
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.Common
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreApplication.API.dll"]