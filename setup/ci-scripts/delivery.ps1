function DeliveryArtifacts($version) {
    if (($null -eq $vesion) -or ($version -eq "")) {
        Write-Error "You must specify a version"
        return
    }

    Write-Host "Delivering artifacts"

    cd src

    Write-Host "Building Docker image"
    docker build -f Crs.Backend\Dockerfile -t amorokin/crsbackend:$version --build-arg BUILD_VERSION=$version .

    Write-Host "Authenticating in Docker Hub"
    $env:DOCKER_HUB_TOKEN | docker login -u $env:DOCKER_HUB_LOGIN --password-stdin

    Write-Host "Pushing image to Docker Hub"
    docker push amorokin/crsbackend:$version

    cd ..\

    Write-Host "Packaging chart"
    helm package chart --version $version -d published\charts

    Write-Host "Done"
}