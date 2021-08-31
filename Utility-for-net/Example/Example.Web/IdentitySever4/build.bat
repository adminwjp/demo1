@echo off

echo "Linux Docker build"

cd E:\work\csharp\src\IdentityServer\Identity.Server

dotnet publish -c Release -o E:\work\csharp\src\IdentityServer\Identity.Server\bin\Release\netcoreapp3.1\Publish

cd E:\work\csharp\src\IdentityServer\Identity.Server\bin\Release\netcoreapp3.1\Publish

echo "publish SocialContact.Api success"

docker build -t aspnetcoredocker .