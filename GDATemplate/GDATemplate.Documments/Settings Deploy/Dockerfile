FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR "/src"
#COPY . .
COPY "GDATemplate.Api/GDATemplate.Api.csproj" "GDATemplate.Api/"
RUN dotnet restore "GDATemplate.Api/GDATemplate.Api.csproj" --disable-parallel --verbosity normal
COPY . .
WORKDIR "/src/GDATemplate.Api"
RUN dotnet --list-sdks
RUN dotnet build "GDATemplate.Api.csproj" -c Release -o /app/build
RUN dotnet publish "GDATemplate.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
COPY --from=build /app/publish .
ENV LC_ALL pt_BR.UTF-8
ENV LANG pt_BR.UTF-8
ENV LANGUAGE pt_BR.UTF-8
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/' /etc/ssl/openssl.cnf && sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "GDATemplate.Api.dll", "--urls", "http://0.0.0.0:5000"]


