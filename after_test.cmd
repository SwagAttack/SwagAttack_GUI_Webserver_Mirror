nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
.toolsOpenCover.4.6.519toolsOpenCover.Console.exe -target:.toolsNUnit.Runners.2.6.4toolsnunit-console.exe -targetargs:"/nologo /noshadow ./SwagAttack_GUI_Webserver_Mirror/**/*.test/bin/Debug/netcoreapp2.0/*Tests.dll" -filter:"+[*]* -[*.Tests]*" -register:user
.toolscoveralls.net.0.412toolscsmacnz.Coveralls.exe --opencover