@echo off

IF [%2] == [] GOTO usage
IF [%1] == [Release] GOTO run
IF [%1] == [Debug] GOTO run
GOTO usage

:run

msbuild /p:Configuration=%1
nuget pack Ds3.csproj -Prop Configuration=%1 -OutputDirectory %2
GOTO eof

:usage

echo Usage: %0 "Release|Debug" "output directory" 1>&2
GOTO eof

:eof

