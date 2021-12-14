#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-amd64  AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["NeopixelsBackend.csproj", "NeopixelsBackend/"]
RUN dotnet restore "NeopixelsBackend/NeopixelsBackend.csproj"
COPY . .
WORKDIR "/src/NeopixelsBackend"
COPY . .
RUN dotnet build "NeopixelsBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NeopixelsBackend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-arm32v7 AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt-get -y update
RUN apt-get install -y scons git gcc
RUN git clone https://github.com/jgarff/rpi_ws281x.git
RUN cd rpi_ws281x && scons && gcc -shared -o ws2811.so *.o && cp ws2811.so /usr/lib
ENTRYPOINT ["dotnet", "NeopixelsBackend.dll"]