#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1: AS base
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-sac2016 AS base
# Identify the maintainer of an image
LABEL maintainer="Joit Solutions"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["MyFinance/MyFinance.csproj", "MyFinance/"]
COPY ["MyFinance/*.json", "MyFinance/"]
RUN dotnet restore "MyFinance/MyFinance.csproj"
WORKDIR "/src/MyFinance"
COPY . .
RUN dotnet build "MyFinance.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MyFinance.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyFinance.dll"]
