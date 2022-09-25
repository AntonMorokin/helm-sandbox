function ReleaseNewVersion($needToPushChanges) {
    Write-Host "Releasing new version"
    
    Write-Host "Cleaning repo"
    
    git reset --hard

    if (Test-Path published\cli-tools) {
        Remove-Item published\cli-tools -Recurse
    }
    
    Write-Host "Building CLI tools"
    
    dotnet restore src

    dotnet build src\CliTools\CliTools.CreateNextVersion -c Release --no-restore --no-self-contained
    dotnet build src\CliTools\CliTools.UpdateChartVersion -c Release --no-restore --no-self-contained

    dotnet publish src\CliTools\CliTools.CreateNextVersion -c Release -o published\cli-tools\version --no-self-contained --no-build
    dotnet publish src\CliTools\CliTools.UpdateChartVersion -c Release -o published\cli-tools\chart --no-self-contained --no-build
    
    Write-Host "Getting release next version"
    
    $version = published\cli-tools\version\CliTools.CreateNextVersion.exe
    
    Write-Host "Next release version is $version"
    Write-Host "Updating chart versions"
    
    published\cli-tools\chart\CliTools.UpdateChartVersion.exe -f chart -v $version -d backend
    
    Write-Host "Committing changes"
    
    git add --all
    git commit -m "New version released: $version"
    git tag $version

    if ($needToPushChanges -eq "true") {
        git push --tags origin HEAD
    }

    Write-Host "Done"

    return $version
}