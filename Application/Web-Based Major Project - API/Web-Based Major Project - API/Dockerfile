FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 8080

# Copy solution file
COPY *.sln ../

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY *. ./
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"]