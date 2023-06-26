FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["JobStream/JobStream.csproj", "JobStream/"]
RUN dotnet restore "JobStream/JobStream.csproj"
COPY . .
WORKDIR "/src/JobStream"
RUN dotnet build "JobStream.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobStream.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobStream.dll"]