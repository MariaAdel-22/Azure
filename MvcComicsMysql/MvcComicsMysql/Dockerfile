#LA IMAGEN A DESCARGAR Y LOS PUERTOS QUE PODEMOS UTILIZAR
#EN DICHA IMAGEN
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
#ESTO ES LA IMAGEN QUE NECESITA CON EL COMPILADOR
#DOTNET
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.csproj ./
#DESCARGAR TODOS LOS NUGET PARA EL PROYECTO
RUN dotnet restore 
COPY . .
WORKDIR "/src/MvcComicsMysql"
#COMPILA EL PROYECTO PARA SER PUBLICADO EN LA IMAGEN DE DOCKER
RUN dotnet build "/src/MvcComicsMysql.csproj" -c Release -o /app/build
 
#CREAMOS UN PUNTO PARA PUBLICAR LA APP EN OTRA CARPETA
FROM build AS publish
RUN dotnet publish "/src/MvcComicsMysql.csproj" -c Release -o /app/publish
 
#COPIA EL PROYECTO PUBLICADO DENTRO DE LA APP PARA SU EJECUCION
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcComicsMysql.dll"]