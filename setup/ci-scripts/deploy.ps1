function DeployChart($version) {
    if (($null -eq $vesion) -or ($version -eq "")) {
        Write-Error "You must specify a version"
        return
    }
    
    Write-Host "Upgrading chart to version $version"
    
    # --namespace car-rent-system  `
    helm upgrade car-rent-system published\charts\car-rent-system-$version.tgz `
    --kube-context crs `
    --atomic `
    --cleanup-on-fail `
    --create-namespace `
    --install `
    --dry-run `
    --debug
    
    Write-Host "Done"
}