FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY Evently.csproj ./
RUN dotnet restore Evently.csproj
COPY . ./
RUN dotnet publish Evently.csproj --configuration Release --no-restore --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV PORT=3000
EXPOSE 3000
ENTRYPOINT ["dotnet", "Evently.dll"]
