FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FileMailer.Worker/*.csproj", "FileMailer.Worker/"]
COPY ["DataImporter.Common/*.csproj", "DataImporter.Common/"]
COPY ["DataImporter.Logic/*.csproj", "DataImporter.Logic/"]
COPY ["DataImporter.Data/*.csproj", "DataImporter.Data/"]
RUN dotnet restore "FileMailer.Worker/FileMailer.Worker.csproj"

COPY . ./

WORKDIR "/src/FileMailer.Worker"
RUN dotnet build "FileMailer.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FileMailer.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FileMailer.Worker.dll"]