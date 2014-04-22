#
#############################################################################
#  Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
#  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
#  this file except in compliance with the License. A copy of the License is located at
#
#  http://www.apache.org/licenses/LICENSE-2.0
#
#  or in the "license" file accompanying this file.
#  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
#  CONDITIONS OF ANY KIND, either express or implied. See the License for the
#  specific language governing permissions and limitations under the License.
#############################################################################
#

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
	
# Write to zip.
$zipfile = $moduleDir + ".zip"
if ((Test-Path $zipFile) -eq $true) { rm $zipFile }
[Reflection.Assembly]::LoadWithPartialName("System.IO.Compression.FileSystem") > $null
[System.IO.Compression.ZipFile]::CreateFromDirectory(
	$moduleDir,
	$zipFile,
	[System.IO.Compression.CompressionLevel]::Optimal,
	$false
)
