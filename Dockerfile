FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./WebApi.csproj"
RUN dotnet build "./WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-self-contained

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

RUN dotnet tool install --global dotnet-ef --version 8.0.12
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app

COPY --from=build /src /app/src
COPY --from=publish /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "WebApi.dll"]