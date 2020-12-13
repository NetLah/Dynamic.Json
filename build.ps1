$ErrorActionPreference = "Stop";

$packOutput = './artifacts'
$packOutputCopy = './nuget'

New-Item -ItemType Directory -Force -Path $packOutputCopy

dotnet tool restore

dotnet build -c Release --nologo

dotnet test -c Release --no-build

if(Test-Path -Path $packOutput -PathType Container) {
  Remove-Item -Path $packOutput -Recurse -Force -Confirm:$false
}

dotnet pack ./Testing.Dynamic.Json/Testing.Dynamic.Json.csproj -c Release -o $packOutput --no-build --nologo

Copy-Item -Path $packOutput -Destination $packOutputCopy -Recurse -Container:$false