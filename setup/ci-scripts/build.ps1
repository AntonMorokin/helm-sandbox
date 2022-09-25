function BuildServices($version) {
    if (($null -eq $vesion) -or ($version -eq "")) {
        Write-Error "You must specify a version"
        return
    }

    Write-Host "Building projects"

    Write-Host "Cleaning up working dir"
    
    if (Test-Path published\backend) {
        Remove-Item published\backend -Recurse
    }
    
    Write-Host "Building Crs.Backend service"
    dotnet restore src
    dotnet build src\Crs.Backend -c Release --no-restore --no-self-contained /p:Version=$version
    
    Write-Host "Publishing Crs.Backend service"
    dotnet publish src\Crs.Backend -c Release -o published\backend --no-self-contained --no-build

    Write-Host "Done"
}