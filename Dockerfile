FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN sed -i 's/TLSv1.2/TLSv1/g'  /etc/ssl/openssl.cnf

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["PdfGenerator.WebApi/PdfGenerator.WebApi.csproj", "PdfGenerator.WebApi/"]
COPY ["PdfGenerator.Infrastructure/PdfGenerator.Infrastructure.csproj", "PdfGenerator.Infrastructure/"]
COPY ["PdfGenerator.Core/PdfGenerator.Core.csproj", "PdfGenerator.Core/"]
COPY ["PdfGenerator.Application/PdfGenerator.Application.csproj", "PdfGenerator.Application/"]
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
