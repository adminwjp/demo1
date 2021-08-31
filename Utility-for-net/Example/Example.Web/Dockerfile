#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#https://dotnet.microsoft.com/download/dotnet-core?utm_source=getdotnetcorecli&utm_medium=referral
#https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-centos

#publish 生成镜像  其他 方式 比较麻烦  路劲 要 符合规则 包也要存在 
#docker build -t oaapi:v1 .
#docker run -t -d -p 80:5000 oaapi:v1

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
#FROM mcr.microsoft.com/dotnet/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 802
#FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /app
COPY  . /app/out/oaapi 
WORKDIR /app/out/oaapi 
ENTRYPOINT ["dotnet", "OA.Api.dll"]