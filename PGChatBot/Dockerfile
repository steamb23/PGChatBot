FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PGChatBot/PGChatBot.csproj", "PGChatBot/"]
RUN dotnet restore "PGChatBot/PGChatBot.csproj"
COPY . .
WORKDIR "/src/PGChatBot"
RUN dotnet build "PGChatBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PGChatBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PGChatBot.dll"]
