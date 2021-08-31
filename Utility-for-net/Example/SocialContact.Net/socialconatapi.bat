@echo off
cd E:/work/program/SocialContact/src/SocialContact.Api
echo "dotnet clean"
dotnet clean
echo "dotnet clean finsh"
echo "dotnet build"
dotnet build
echo "dotnet build finsh"
echo "dotnet publish"
dotnet publish -o "e:\work\iis\socialconatapi"
echo "dotnet publish finsh"