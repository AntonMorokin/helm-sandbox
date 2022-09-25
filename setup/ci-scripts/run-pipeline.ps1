Import-Module $PSScriptRoot\new-release.ps1 -Force
Import-Module $PSScriptRoot\build.ps1 -Force
Import-Module $PSScriptRoot\test.ps1 -Force
Import-Module $PSScriptRoot\delivery.ps1 -Force
Import-Module $PSScriptRoot\deploy.ps1 -Force

$mode = $args[0]

if ($mode -eq "merge-request") {
    BuildServices "1.0.0"
    RunTests "1.0.0"
}
elseif ($mode -eq "push-to-master") {
    $newVersion = ReleaseNewVersion -needToPushChanges "false"
    BuildServices $newVersion
    RunTests $newVersion
    DeliveryArtifacts $newVersion
    DeployChart $newVersion
}
else {
    Write-Host "Unknown mode $mode"
}