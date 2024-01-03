# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore any dependencies (via NuGet)
COPY ["OpenSourceRecipe/OpenSourceRecipe.csproj", "OpenSourceRecipe/"]
RUN dotnet restore "OpenSourceRecipe/OpenSourceRecipe.csproj"

# Copy the project files and build the release
# Be explicit about the files and folders you copy
COPY OpenSourceRecipe/ ./OpenSourceRecipe/
WORKDIR /app/OpenSourceRecipe
RUN dotnet publish "OpenSourceRecipe.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/OpenSourceRecipe/out .
ENTRYPOINT ["dotnet", "OpenSourceRecipe.dll"]
