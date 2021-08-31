@echo off

echo Linux Docker build

cd ../SocialContact.Api

dotnet publish -c Release -o ../../../docker/SocialContact.Api/publish

cd ../../../docker/SocialContact.Api/publish

echo publish SocialContact.Api success

docker build -t aspnetcoredocker .