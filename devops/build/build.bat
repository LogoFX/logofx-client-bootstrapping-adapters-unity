cd ../../src
dotnet build LogoFX.Client.Bootstrapping.Adapters.Unity.sln -c Release
dotnet test LogoFX.Client.Bootstrapping.Adapters.Unity.sln -c Release
cd ../devops/publish