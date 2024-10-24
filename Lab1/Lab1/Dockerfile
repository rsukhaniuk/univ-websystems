FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
    && echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
    && wget https://download.newrelic.com/548C16BF.gpg && apt-key add 548C16BF.gpg \
    && apt-get update && apt-get install -y newrelic-dotnet-agent

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Lab1.csproj", "."]
RUN dotnet restore "./Lab1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Lab1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Lab1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Lab1.dll"]