FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GeneratorConsumer/GeneratorConsumer.csproj", "GeneratorConsumer/"]
RUN dotnet restore "GeneratorConsumer/GeneratorConsumer.csproj"
COPY . .
WORKDIR "/src/GeneratorConsumer"
RUN dotnet build "GeneratorConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeneratorConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeneratorConsumer.dll"]
