test: integration unit

integration: build
	mono ./packages/NUnit.Runners.2.6.3/tools/nunit-console.exe ./IntegrationTestDS3/bin/Debug/IntegrationTestDS3.dll
unit: build
	mono ./packages/NUnit.Runners.2.6.3/tools/nunit-console.exe ./TestDs3/bin/Debug/TestDs3.dll
build:
	xbuild /p:Configuration=Debug ds3_net_sdk.sln
clean:
	rm -rf ./TestDs3/bin/Debug ./IntegrationTestDS3/bin/Debug ./Ds3/bin/Debug
