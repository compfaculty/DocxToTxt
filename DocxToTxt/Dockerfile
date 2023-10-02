FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DocxToTxt/DocxToTxt.csproj", "DocxToTxt/"]
RUN dotnet restore "DocxToTxt/DocxToTxt.csproj"
COPY . .
WORKDIR "/src/DocxToTxt"
RUN dotnet build "DocxToTxt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocxToTxt.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocxToTxt.dll"]
