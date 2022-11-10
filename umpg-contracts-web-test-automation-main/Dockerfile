FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

## copy solution
#COPY Selenoid.sln ./
## find and copy all csproj files 
#COPY **/*.csproj ./
## create folder structure using csproj file names
#RUN find . -iname '*.csproj' -exec sh -c 'mkdir /src/$(basename {} .csproj) && mv /src/$(basename {}) /src/$(basename {} .csproj)' \;
## restore solution packages to cache
#RUN dotnet restore

COPY . .

RUN dotnet restore

ENTRYPOINT ["dotnet","test"]