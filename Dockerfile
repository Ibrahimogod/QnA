FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src                                                                    
COPY ./src ./

# restore solution
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN dotnet restore QnA.Demo.sln

WORKDIR /src/QnA.Demo

# build project   
RUN dotnet build QnA.Demo.csproj -c Release

# publish project
WORKDIR /src/QnA.Demo  
RUN dotnet publish QnA.Demo.csproj -c Release -o /app/published


WORKDIR /app/published

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime 

# installs required packages
RUN apk add libgdiplus --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted
RUN apk add libc-dev --no-cache
RUN apk add tzdata --no-cache
RUN apk add icu-libs --no-cache

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# shoud run script for to install and trust dev cert
# COPY ./entrypoint.sh /entrypoint.sh
# RUN chmod 755 /entrypoint.sh

WORKDIR /app
COPY --from=build /app/published .

ENTRYPOINT ["dotnet", "QnA.Demo.dll"]