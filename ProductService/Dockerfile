# ���������� ����������� ����� SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# �������� ����� ������� � ��������
COPY ["ProductAPI/ProductAPI.csproj", "ProductAPI/"]
COPY ["ProductEntities/ProductEntities.csproj", "ProductEntities/"]
COPY ["ProductServices/ProductServices.csproj", "ProductServices/"]

# ��������������� �����������
RUN dotnet restore "ProductAPI/ProductAPI.csproj"

# �������� ���� �������� ���
COPY . .

# �������� ����������
RUN dotnet build "ProductAPI/ProductAPI.csproj" -c Release -o /app/build

# ��������� ����������
FROM build AS publish
RUN dotnet publish "ProductAPI/ProductAPI.csproj" -c Release -o /app/publish

# ��������� �����
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductAPI.dll"]