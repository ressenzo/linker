# Use the official .NET 10.0 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the solution file and project files
# COPY ["Linker.Link.Api/Linker.Link.Api.csproj", "Linker.Link.Api/"]
# COPY ["Linker.Link.Application/Linker.Link.Application.csproj", "Linker.Link.Application/"]
# COPY ["Linker.Link.Domain/Linker.Link.Domain.csproj", "Linker.Link.Domain/"]
# COPY ["Linker.Link.Infrastructure/Linker.Link.Infrastructure.csproj", "Linker.Link.Infrastructure/"]
COPY **/*.csproj ./

# Restore dependencies
RUN dotnet restore "./Linker.Link.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/Linker.Link.Api"
RUN dotnet build "Linker.Link.Api.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "Linker.Link.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 10.0 ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

# Expose the port the app runs on
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "Linker.Link.Api.dll"]
