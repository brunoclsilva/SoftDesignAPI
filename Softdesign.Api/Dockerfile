FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Softdesign.Api/Softdesign.Api.csproj", "Softdesign.Api/"]
RUN dotnet restore "Softdesign.Api/Softdesign.Api.csproj"
COPY . .
WORKDIR "/src/Softdesign.Api"
RUN dotnet build "Softdesign.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Softdesign.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Softdesign.Api.dll"]