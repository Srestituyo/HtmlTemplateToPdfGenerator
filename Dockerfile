FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN sed -i 's/TLSv1.2/TLSv1/g'  /etc/ssl/openssl.cnf

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src


# Install dependencies
RUN apt-get update && apt-get install -y wget gnupg ca-certificates procps libx11-xcb1 libxcb1 libasound2 libatk1.0-0 libatk-bridge2.0-0 libc6 libcairo2 libcups2 libdbus-1-3 libexpat1 libfontconfig1 libgcc1 libgconf-2-4 libgdk-pixbuf2.0-0 libglib2.0-0 libgtk-3-0 libnspr4 libpango-1.0-0 libpangocairo-1.0-0 libstdc++6 libx11-6 libx11-xcb-dev libxcb-dri3-0 libxcomposite1 libxcursor1 libxdamage1 libxext6 libxfixes3 libxi6 libxrandr2 libxrender1 libxss1 libxtst6 lsb-release unzip

# Download and install Chromium
RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
    && apt-get update \
    && apt-get install -y google-chrome-stable

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

# Set the Chromium path environment variable
ENV PUPPETEER_EXECUTABLE_PATH=/usr/bin/google-chrome-stable

ENTRYPOINT ["dotnet", "PdfGenerator.WebApi.dll"]
