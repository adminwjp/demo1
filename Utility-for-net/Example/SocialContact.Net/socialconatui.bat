@echo off
cd E:\work\program\SocialContact\src\SocialContact
echo "dotnet clean"
dotnet clean
echo "dotnet clean finsh"
echo "dotnet build"
dotnet build
echo "dotnet build finsh"
echo "dotnet publish"
dotnet publish -o "e:\work\iis\socialconatui"
echo "dotnet publish finsh"