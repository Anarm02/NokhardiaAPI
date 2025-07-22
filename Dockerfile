FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY NokhardiaAPI/NokhardiaAPI.csproj NokhardiaAPI/
RUN dotnet restore NokhardiaAPI/NokhardiaAPI.csproj

COPY . .
WORKDIR /src/NokhardiaAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NokhardiaAPI.dll"]
