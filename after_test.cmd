nuget install NUnit.Console -OutputDirectory tools
nuget install OpenCover -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Version 0.412.0 -OutputDirectory tools
cd C:\projects\swagattack-gui-webserver-mirror\tools
REM nunit3-console C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\bin\Debug\netcoreapp2.0\WebserverUnitTests.dll --result=myresults.xml;format=AppVeyor
REM OpenCover.4.6.519\tools\OpenCover.Console.exe -target:"dotnet.exe" -oldStyle -register:user -targetargs:"test C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\WebserverUnitTests.csproj --test-adapter-path:. --logger:Appveyor" -filter:"+[*]* -[*.Tests]*"
dotnet test C:\projects\swagattack-gui-webserver-mirror\GUI\*.test\*.csproj --no-build --no-restore --logger:Appveyor /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
REM coveralls.net.0.412\tools\csmacnz.Coveralls.exe --opencover -i ..\GUI\GUI_Index.unit.test\coverage.xml
REM C:\projects\swagattack-gui-webserver-mirror\GUI\GUI_Index.unit.test\WebserverUnitTests.csproj