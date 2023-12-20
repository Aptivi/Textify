@echo off

for /f "tokens=* USEBACKQ" %%f in (`type version`) do set version=%%f
set releaseconfig=%1
if "%releaseconfig%" == "" set releaseconfig=Release

:packbin
echo Packing binary...
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-bin.zip "..\Textify\bin\%releaseconfig%\netstandard2.0\*"
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-online.zip "..\Textify.Online\bin\%releaseconfig%\netstandard2.0\*"
if %errorlevel% == 0 goto :complete
echo There was an error trying to pack binary (%errorlevel%).
goto :finished

:complete
move %temp%\%version%-bin.zip
move %temp%\%version%-online.zip

echo Pack successful.
:finished