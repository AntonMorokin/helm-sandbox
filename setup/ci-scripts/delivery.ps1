echo @off

$version = $args[0]

cd src

echo "Building Docker image"
docker build -f Crs.Backend\Dockerfile -t amorokin/crsbackend:$version --build-arg BUILD_VERSION=$version .

echo "Authenticating in Docker Hub"
$env:DOCKER_HUB_TOKEN | docker login -u $env:DOCKER_HUB_LOGIN --password-stdin

echo "Pushing image to Docker Hub"
docker push amorokin/crsbackend:$version

cd ..\

echo "Packaging chart"
helm package chart --version $version -d published/charts

echo "Done"
echo @on