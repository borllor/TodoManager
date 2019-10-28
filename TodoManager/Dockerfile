FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["TodoManager.csproj", "TodoManager/"]

RUN dotnet restore "TodoManager/TodoManager.csproj"
COPY . "TodoManager/"
WORKDIR "/src/TodoManager"
RUN dotnet build "TodoManager.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TodoManager.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TodoManager.dll"]