FROM microsoft/dotnet:sdk AS build-env

RUN mkdir /Notifications
WORKDIR /Notifications

COPY . .

CMD dotnet restore

RUN dotnet build

RUN dotnet run