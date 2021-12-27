FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DataExporter.Worker/*.csproj", "DataExporter.Worker/"]
COPY ["DataImporter.Common/*.csproj", "DataImporter.Common/"]
COPY ["DataImporter.Logic/*.csproj", "DataImporter.Logic/"]
COPY ["DataImporter.Data/*.csproj", "DataImporter.Data/"]
RUN dotnet restore "DataExporter.Worker/DataExporter.Worker.csproj"

COPY . ./

WORKDIR "/src/DataExporter.Worker"
RUN dotnet build "DataExporter.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataExporter.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataExporter.Worker.dll"]