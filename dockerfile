FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY CoreApplication.sln ./
COPY CoreApplication.Common/*.csproj ./CoreApplication.Common/

COPY CoreApplication.Data/*.csproj ./CoreApplication.Data/
COPY CoreApplication.Data.Contracts/*.csproj ./CoreApplication.Data.Contracts/
COPY CoreApplication.Data.Entity/*.csproj ./CoreApplication.Data.Entity/

COPY CoreApplication.Business.DTO/*.csproj ./CoreApplication.Business.DTO/
COPY CoreApplication.Business/*.csproj ./CoreApplication.Business/
COPY CoreApplication.Business.Contracts/*.csproj ./CoreApplication.Business.Contracts/

COPY CoreApplication.API/*.csproj ./CoreApplication.API/
COPY CoreApplication.DTO/*.csproj ./CoreApplication.DTO/
COPY CoreApplication.Middlewares/*.csproj ./CoreApplication.Middlewares/

RUN dotnet restore
COPY . .

WORKDIR /src/CoreApplication.Common
RUN dotnet build -c Release -o /app



WORKDIR /src/CoreApplication.Data.Entity
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.Data.Contracts
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.Data
RUN dotnet build -c Release -o /app


WORKDIR /src/CoreApplication.Business.DTO
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.Business.Contracts
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.Business
RUN dotnet build -c Release -o /app



WORKDIR /src/CoreApplication.Middlewares
RUN dotnet build -c Release -o /app

WORKDIR /src/CoreApplication.DTO
RUN dotnet build -c Release -o /app



WORKDIR /src/CoreApplication.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreApplication.API.dll"]