#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QuizGame.Service/QuizGame.Service.csproj", "QuizGame.Service/"]
COPY ["QuizGame.Common/QuizGame.Common.csproj", "QuizGame.Common/"]
RUN dotnet restore "QuizGame.Service/QuizGame.Service.csproj"
COPY . .
WORKDIR "/src/QuizGame.Service"
RUN dotnet build "QuizGame.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizGame.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizGame.Service.dll"]