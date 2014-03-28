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

# Determine the module version and the minimum CLR version.
$moduleAssembly = [Reflection.Assembly]::Loadfile($moduleDir + "\Ds3Client.dll")
$moduleVersion = $moduleAssembly.GetName().Version.ToString()
$clrVersionParts = $moduleAssembly.ImageRuntimeVersion.Substring(1).Split('.') | %{ [int32]::Parse($_) }
$clrVersion = New-Object System.Version ($clrVersionParts[0], $clrVersionParts[1], 0, 0)

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
	-FileList @() `
	-ClrVersion $clrVersion `
	-ModuleVersion $moduleVersion
