FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DataImporter.WebApp/*.csproj", "DataImporter.WebApp/"]
COPY ["DataImporter.Data/*.csproj", "DataImporter.Data/"]
COPY ["DataImporter.Common/*.csproj", "DataImporter.Common/"]
COPY ["DataImporter.Users/*.csproj", "DataImporter.Users/"]
COPY ["DataImporter.Logic/*.csproj", "DataImporter.Logic/"]
RUN dotnet restore "DataImporter.WebApp/DataImporter.WebApp.csproj"

COPY . ./

WORKDIR "/src/DataImporter.WebApp"
RUN dotnet build "DataImporter.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DataImporter.WebApp.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DataImporter.WebApp.dll"]