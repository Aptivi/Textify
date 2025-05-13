@echo off

set ROOTDIR=%~dp0\..

REM Download libicu for Windows and build
if not exist "%TEMP%\icu4c-77_1-Win64-MSVC2022.zip" powershell -Command "Invoke-WebRequest https://github.com/unicode-org/icu/releases/download/release-77-1/icu4c-77_1-Win64-MSVC2022.zip -OutFile ""%TEMP%\icu4c-77_1-Win64-MSVC2022.zip"""
if not exist "%TEMP%\icu4c-77_1-WinARM64-MSVC2022.zip" powershell -Command "Invoke-WebRequest https://github.com/unicode-org/icu/releases/download/release-77-1/icu4c-77_1-WinARM64-MSVC2022.zip -OutFile ""%TEMP%\icu4c-77_1-WinARM64-MSVC2022.zip"""

pushd "%ROOTDIR%\tools\"
"%ProgramFiles%\7-Zip\7z.exe" e "%TEMP%\icu4c-77_1-Win64-MSVC2022.zip" bin64/icuuc77.dll
"%ProgramFiles%\7-Zip\7z.exe" e "%TEMP%\icu4c-77_1-Win64-MSVC2022.zip" bin64/icudt77.dll
popd
mkdir "%ROOTDIR%\public\Textify.Data\runtimes\win-x64\native\"
move "%ROOTDIR%\tools\icuuc77.dll" "%ROOTDIR%\public\Textify.Data\runtimes\win-x64\native\icuuc77.dll"
move "%ROOTDIR%\tools\icudt77.dll" "%ROOTDIR%\public\Textify.Data\runtimes\win-x64\native\icudt77.dll"

pushd "%ROOTDIR%\tools\"
"%ProgramFiles%\7-Zip\7z.exe" e "%TEMP%\icu4c-77_1-WinARM64-MSVC2022.zip" binARM64/icuuc77.dll
"%ProgramFiles%\7-Zip\7z.exe" e "%TEMP%\icu4c-77_1-WinARM64-MSVC2022.zip" binARM64/icudt77.dll
popd
mkdir "%ROOTDIR%\public\Textify.Data\runtimes\win-arm64\native\"
move "%ROOTDIR%\tools\icuuc77.dll" "%ROOTDIR%\public\Textify.Data\runtimes\win-arm64\native\icuuc77.dll"
move "%ROOTDIR%\tools\icudt77.dll" "%ROOTDIR%\public\Textify.Data\runtimes\win-arm64\native\icudt77.dll"
