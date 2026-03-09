FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore "Fretefy.Test.sln"

RUN dotnet publish "Fretefy.Test.WebApi/Fretefy.Test.WebApi.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "Fretefy.Test.WebApi.dll"]