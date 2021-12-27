FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DataImporter.Worker/*.csproj", "DataImporter.Worker/"]
COPY ["DataImporter.Common/*.csproj", "DataImporter.Common/"]
COPY ["DataImporter.Logic/*.csproj", "DataImporter.Logic/"]
COPY ["DataImporter.Data/*.csproj", "DataImporter.Data/"]
RUN dotnet restore "DataImporter.Worker/DataImporter.Worker.csproj"

COPY . ./

WORKDIR "/src/DataImporter.Worker"
RUN dotnet build "DataImporter.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataImporter.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataImporter.Worker.dll"]