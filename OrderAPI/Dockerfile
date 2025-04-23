# Используем официальный образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы решения и проектов
COPY ["OrderAPI/OrderAPI.csproj", "OrderAPI/"]
COPY ["OrderEntities/OrderEntities.csproj", "OrderEntities/"]
COPY ["OrderServices/OrderServices.csproj", "OrderServices/"]

# Восстанавливаем зависимости
RUN dotnet restore "OrderAPI/OrderAPI.csproj"

# Копируем весь исходный код
COPY . .

# Собираем приложение
RUN dotnet build "OrderAPI/OrderAPI.csproj" -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
RUN dotnet publish "OrderAPI/OrderAPI.csproj" -c Release -o /app/publish

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderAPI.dll"]