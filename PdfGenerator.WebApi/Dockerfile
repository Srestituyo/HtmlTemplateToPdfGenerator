FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PdfGenerator.WebApi/PdfGenerator.WebApi.csproj", "PdfGenerator.WebApi/"]
RUN dotnet restore "PdfGenerator.WebApi/PdfGenerator.WebApi.csproj"
COPY . .
WORKDIR "/src/PdfGenerator.WebApi"
RUN dotnet build "PdfGenerator.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PdfGenerator.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfGenerator.WebApi.dll"]
