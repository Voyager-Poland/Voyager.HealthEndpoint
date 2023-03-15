$version='1.1.7'
dotnet build -c Release   /property:Version=$version
dotnet pack -c Release /property:Version=$version

$ostatniPakiet = (gci .\src\Voyager.HealthEndpoint\bin\Release\*.nupkg | select -last 1).Name
$sciezka = ".\src\Voyager.HealthEndpoint\bin\Release\$ostatniPakiet"

dotnet nuget push "$sciezka" -s Voyager-Poland
