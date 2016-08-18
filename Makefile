test: unit integration longIntegration

longIntegration: build
	mono ./packages/NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe ./LongRunningIntegrationTestDs3/bin/Release/LongRunningIntegrationTestDs3.dll
integration: build
	mono ./packages/NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe ./IntegrationTestDS3/bin/Release/IntegrationTestDS3.dll
unit: build
	mono ./packages/NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe ./TestDs3/bin/Release/TestDs3.dll
build:
	xbuild /p:Configuration=Release ds3_net_sdk.sln
clean:
	rm -rf ./TestDs3/bin/Release ./IntegrationTestDS3/bin/Release ./LongRunningIntegrationTestDs3/bin/Release ./Ds3/bin/Release
