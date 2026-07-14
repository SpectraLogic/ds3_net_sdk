DOTNET ?= dotnet

test: unit integration longIntegration

longIntegration:
	$(DOTNET) test LongRunningIntegrationTestDs3/LongRunningIntegrationTestDs3.csproj -c Release
integration:
	$(DOTNET) test IntegrationTestDS3/IntegrationTestDs3.csproj -c Release
unit:
	$(DOTNET) test TestDs3/TestDs3.csproj -c Release
build:
	$(DOTNET) build ds3_net_sdk.sln -c Release
pack:
	$(DOTNET) pack Ds3/Ds3.csproj -c Release -o nupkgs
clean:
	$(DOTNET) clean ds3_net_sdk.sln
