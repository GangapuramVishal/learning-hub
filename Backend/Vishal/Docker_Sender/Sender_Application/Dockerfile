# Use .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
 
# Copy the project file and restore dependencies
COPY ["Sender_Application.csproj", "."]
RUN dotnet restore "./Sender_Application.csproj"
 
# Copy all project files and build the application
COPY . .
WORKDIR "/src/."
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/out
 
# Use runtime image for final container
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out ./
 
# Set environment variable for Azure Service Bus connection string
ENV SERVICE_BUS_CONNECTION_STRING="Endpoint=sb://demoondocker.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey="
 
# Run the Receiver application
CMD ["dotnet", "Sender_Application.dll"]