nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
cd C:\projects\swagattack-gui-webserver-mirror\tools
OpenCover.4.6.519\tools\OpenCover.Console.exe -target:NUnit.Runners.2.6.4\tools\nunit-console.exe -targetargs:"/nologo /noshadow ../SwagAttack_GUI_Webserver_Mirror/**/*.test/bin/Debug/netcoreapp2.0/*Tests.dll" -filter:"+[*]* -[*.Tests]*" -register:user
coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover