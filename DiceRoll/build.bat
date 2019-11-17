set mypath=%cd%
@echo %mypath%
"C:\Program Files\Unity\Hub\Editor\2019.2.9f1\Editor\Unity.exe" -quit -batchmode -logFile stdout.log -projectPath %mypath% -executeMethod Builder.PerformAndroidBuild
