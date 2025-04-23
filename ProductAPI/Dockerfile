# Используем официальный образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы решения и проектов
COPY ["ProductAPI/ProductAPI.csproj", "ProductAPI/"]
COPY ["ProductEntities/ProductEntities.csproj", "ProductEntities/"]
COPY ["ProductServices/ProductServices.csproj", "ProductServices/"]

# Восстанавливаем зависимости
RUN dotnet restore "ProductAPI/ProductAPI.csproj"

# Копируем весь исходный код
COPY . .

# Собираем приложение
RUN dotnet build "ProductAPI/ProductAPI.csproj" -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
RUN dotnet publish "ProductAPI/ProductAPI.csproj" -c Release -o /app/publish

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductAPI.dll"]