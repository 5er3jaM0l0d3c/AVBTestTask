# Используем официальный образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы решения и проектов
COPY ["OrderService/OrderService.csproj", "OrderService/"]

# Переходим в директорию проекта и восстанавливаем зависимости
WORKDIR "/src/OrderService"
RUN dotnet restore "OrderService.csproj"

# Возвращаемся в корень и копируем весь исходный код
WORKDIR /src
COPY . .

# Собираем приложение
WORKDIR "/src/OrderService"
RUN dotnet build "OrderService.csproj" -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
WORKDIR "/src/OrderService"
RUN dotnet publish "OrderService.csproj" -c Release -o /app/publish

# Генерируем SSL-сертификат (новый этап)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS certgen
RUN dotnet dev-certs https --trust

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]