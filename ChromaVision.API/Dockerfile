FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChromaVision.API/ChromaVision.API.csproj", "ChromaVision.API/"]
COPY ["ChromaVision.Application/ChromaVision.Application.csproj", "ChromaVision.Application/"]
COPY ["ChromaVision.Infrastructure/ChromaVision.Infrastructure.csproj", "ChromaVision.Infrastructure/"]
COPY ["ChromaVision.Core/ChromaVision.Core.csproj", "ChromaVision.Core/"]
COPY ["ChromaVision.Domain/ChromaVision.Domain.csproj", "ChromaVision.Domain/"]
RUN dotnet restore "ChromaVision.API/ChromaVision.API.csproj"
COPY . .
WORKDIR "/src/ChromaVision.API"
RUN dotnet build "ChromaVision.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChromaVision.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChromaVision.API.dll"]