echo @off

$version = $args[0]

echo "Upgrading chart to version $version"

# --namespace car-rent-system  `
helm upgrade car-rent-system published\charts\car-rent-system-$version.tgz `
--kube-context crs `
--atomic `
--cleanup-on-fail `
--create-namespace `
--install `
--dry-run `
--debug

echo "Done"

echo @on