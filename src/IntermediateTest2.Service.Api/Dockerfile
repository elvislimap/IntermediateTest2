FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/IntermediateTest2.Service.Api/IntermediateTest2.Service.Api.csproj", "src/IntermediateTest2.Service.Api/"]
RUN dotnet restore "src/IntermediateTest2.Service.Api/IntermediateTest2.Service.Api.csproj"
COPY . .
WORKDIR "/src/src/IntermediateTest2.Service.Api"
RUN dotnet build "IntermediateTest2.Service.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IntermediateTest2.Service.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IntermediateTest2.Service.Api.dll"]