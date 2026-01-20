# Stage 1: Build the application using the SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code and publish the application
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Create the runtime image (smaller, more secure)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Define the entry point command to run the application
ENTRYPOINT ["dotnet", "TransactionApp.dll"]