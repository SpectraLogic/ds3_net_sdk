param($targetDir)

# Delete and recreate the module directory.
$moduleDir = $targetDir + "\Modules\Ds3Client"
if(Test-Path -Path $moduleDir) {
	rm -Recurse $moduleDir
}
mkdir $moduleDir > $null

# Copy the module files.
cp ($targetDir + "*.dll") $moduleDir
cp ($targetDir + "Help\*") $moduleDir -Recurse -Force

# Create the module manifest.
New-ModuleManifest `
	($moduleDir + "\Ds3Client.psd1") `
	-ModuleToProcess "Ds3Client.dll" `
	-Author "Spectra Logic" `
	-CompanyName "Spectra Logic" `
	-Copyright 2014 `
	-NestedModules @() `
	-Description "" `
	-TypesToProcess @() `
	-FormatsToProcess @() `
	-RequiredAssemblies @() `
	-FileList @()
