# Stage 1: Build the application using the SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["DataTransfer.Sequential/DataTransfer.Sequential.csproj", "DataTransfer.Sequential/"]
RUN dotnet restore "DataTransfer.Sequential/DataTransfer.Sequential.csproj"

# Copy the rest of the source code and publish the application
COPY . .
WORKDIR "/src/DataTransfer.Sequential"
RUN dotnet publish -c Release -o /app/out

# Stage 2: Create the runtime image (smaller, more secure)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Define the entry point command to run the application
ENTRYPOINT ["dotnet", "DataTransfer.Sequential.dll"]

# bash
# docker build -t data-transfer-sequential -f DataTransfer.Sequential/Dockerfile .
