#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Hepsiorada.GrpcService/Hepsiorada.GrpcService.csproj", "Hepsiorada.GrpcService/"]
RUN dotnet restore "Hepsiorada.GrpcService/Hepsiorada.GrpcService.csproj"
COPY . .
WORKDIR "/src/Hepsiorada.GrpcService"
RUN dotnet build "Hepsiorada.GrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hepsiorada.GrpcService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hepsiorada.GrpcService.dll"]