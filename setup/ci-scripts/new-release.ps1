echo @off

echo "Cleaning repo"
git reset --hard
rm temp/cli-tools -r 2>$null

echo "Building CLI tools"

dotnet restore src
dotnet build src -c Release --no-restore --no-self-contained
dotnet publish src/CliTools/CliTools.CreateNextVersion -c Release -o temp/cli-tools/version --no-self-contained --no-build
dotnet publish src/CliTools/CliTools.UpdateChartVersion -c Release -o temp/cli-tools/chart --no-self-contained --no-build

echo "Getting release next version"

$version = temp/cli-tools/version/CliTools.CreateNextVersion.exe

echo "Next release version is $version"
echo "Updating chart versions"

temp/cli-tools/chart/CliTools.UpdateChartVersion.exe -f chart -v $version -d backend

echo "Committing changes"

git add --all
git commit -m "New version released: $version"
git tag $version
# git push --tags origin HEAD

echo "Cleaning up temp files"
rm temp/cli-tools -r

echo "Done"
echo @on