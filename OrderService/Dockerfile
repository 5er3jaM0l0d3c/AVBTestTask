# ���������� ����������� ����� SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# �������� ����� ������� � ��������
COPY ["OrderService/OrderService.csproj", "OrderService/"]

# ��������� � ���������� ������� � ��������������� �����������
WORKDIR "/src/OrderService"
RUN dotnet restore "OrderService.csproj"

# ������������ � ������ � �������� ���� �������� ���
WORKDIR /src
COPY . .

# �������� ����������
WORKDIR "/src/OrderService"
RUN dotnet build "OrderService.csproj" -c Release -o /app/build

# ��������� ����������
FROM build AS publish
WORKDIR "/src/OrderService"
RUN dotnet publish "OrderService.csproj" -c Release -o /app/publish

# ���������� SSL-���������� (����� ����)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS certgen
RUN dotnet dev-certs https --trust

# ��������� �����
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]