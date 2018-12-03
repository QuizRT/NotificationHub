FROM microsoft/dotnet:sdk AS build-env

RUN mkdir /Notifications
WORKDIR /Notifications

COPY . .

RUN dotnet restore
RUN dotnet build
RUN chmod +x ./entrypoint.sh

CMD /bin/bash ./entrypoint.sh