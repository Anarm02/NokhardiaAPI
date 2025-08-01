﻿FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY NokhardiaAPI/NokhardiaAPI.csproj NokhardiaAPI/
RUN dotnet restore NokhardiaAPI/NokhardiaAPI.csproj

COPY . .
WORKDIR /src/NokhardiaAPI
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app


EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080


COPY --from=build /app/publish .


COPY --from=build /app/publish/Credentials ./Credentials

ENTRYPOINT ["dotnet", "NokhardiaAPI.dll"]
