nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
cd C:\projects\swagattack-gui-webserver-mirror\tools
OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"dotnet.exe" -register:user -targetargs:"vstest C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\bin\Debug\netcoreapp2.0\WebserverUnitTests.dll /framework:".NETCoreApp,Version=v2.0"" -filter:"+[*]* -[*.Tests]*"
::coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover