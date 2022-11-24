$workdir=$args[0]
dotnet build $workdir\GTAVLife.csproj
Copy-Item -Force $workdir\bin\Debug\net48\GTAVLife.* 'F:\SteamLibrary\steamapps\common\Grand Theft Auto V\scripts'
Copy-Item -Force $workdir\bin\Debug\net48\*.dll 'F:\SteamLibrary\steamapps\common\Grand Theft Auto V\scripts'
