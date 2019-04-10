FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Ebay.Product/Ebay.Product.csproj", "Ebay.Product/"]
RUN dotnet restore "Ebay.Product/Ebay.Product.csproj"
COPY . .
WORKDIR "/src/Ebay.Product"
RUN dotnet build "Ebay.Product.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Ebay.Product.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ebay.Product.dll"]