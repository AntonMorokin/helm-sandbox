echo @off

$version = $args[0]

echo "Cleaning up working dir"
rm published -r 2>$null

echo "Building solution"
dotnet restore src/
dotnet build src/ -c Release --no-restore --no-self-contained /p:Version=$version

echo "Runing tests"
dotnet publish src/Crs.Backend.Tests -c Release -o published/tests/ --no-build --no-self-contained
dotnet test published/tests/Crs.Backend.Tests.dll -- NUnit.TestOutputXml=../test-results/

echo "Publishing backend service and CLI tools"
dotnet publish src/Crs.Backend -c Release -o published/backend/ --no-self-contained --no-build
dotnet publish src/CliTools.CreateNextVersion -c Release -o published/cli-tools/ --no-self-contained --no-build
dotnet publish src/CliTools.UpdateChartVersion -c Release -o published/cli-tools/ --no-self-contained --no-build

echo "Done"
echo @on