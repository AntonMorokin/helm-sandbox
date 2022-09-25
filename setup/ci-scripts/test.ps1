function RunTests($version) {
    if (($null -eq $vesion) -or ($version -eq "")) {
        Write-Error "You must specify a version"
        return
    }

    Write-Host "Running tests"

    Write-Host "Cleaning up working dir"
    
    if (Test-Path published\tests) {
        Remove-Item published\tests -Recurse
    }

    if (Test-Path published\test-results) {
        Remove-Item published\test-results -Recurse
    }
    
    Write-Host "Building Crs.Backend.Tests project"
    dotnet restore src
    dotnet build src\Crs.Backend.Tests -c Release --no-restore --no-self-contained /p:Version=$version
    
    Write-Host "Publishing Crs.Backend.Tests project"
    dotnet publish src\Crs.Backend.Tests -c Release -o published\tests --no-self-contained --no-build

    Write-Host "Running Crs.Backend.Tests"
    dotnet test published\tests\Crs.Backend.Tests.dll -- NUnit.TestOutputXml=..\test-results

    Write-Host "Done"
}