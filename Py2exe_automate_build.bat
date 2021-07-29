@echo off
Mkdir buildtools
Mkdir output

::Set personal Path to the Apps:
set PythonEXE=C:\Python\python.exe
set SevenZipEXE=D:\Tools\7-ZIP\7z.exe
set UpxEXE=D:\Tools\upx\upx.exe


:: Compress=1 - Use CompressFiles
:: Compress=0 - Don't CompressFiles
set Compress=1


if not exist %~dpn0.py          call :FileNotFound %~dpn0.py
if not exist %PythonEXE%        call :FileNotFound %PythonEXE%
if not exist %SevenZipEXE%      call :FileNotFound %SevenZipEXE%
if not exist %UpxEXE%           call :FileNotFound %UpxEXE%


::Write the Py2EXE-Setup File
call :MakeSetupFile >"%~dpn0_EXESetup.py"


::Compile the Python-Script
%PythonEXE% "%~dpn0_EXESetup.py" py2exe
if not "%errorlevel%"=="0" (
        echo Py2EXE Error!
        pause
        goto:eof
)


:: Delete the Py2EXE-Setup File
del "%~dpn0_EXESetup.py"


:: Copy the Py2EXE Results to the SubDirectory and Clean Py2EXE-Results
rd build /s /q
xcopy dist\*.* "%~dpn0_EXE\" /d /y
:: I use xcopy dist\*.* "%~dpn0_EXE\" /s /d /y
:: This is necessary when you have subdirectories - like when you use Tkinter
rd dist /s /q


if "%Compress%"=="1" call:CompressFiles
echo.
echo.
echo Done: "%~dpn0_EXE\"
echo.
pause
goto:eof



:CompressFiles
        %SevenZipEXE% -aoa x "%~dpn0_EXE\library.zip" -o"%~dpn0_EXE\library\"
        del "%~dpn0_EXE\library.zip"

        cd %~dpn0_EXE\library\
        %SevenZipEXE% a -tzip -mx9 "..\library.zip" -r
        cd..
        rd "%~dpn0_EXE\library" /s /q

        cd %~dpn0_EXE\
        %UpxEXE% --best *.*
goto:eof



:MakeSetupFile
        echo.
        echo from distutils.core import setup
        echo import py2exe
        echo.
        echo setup (console=[r"%~dpn0.py"],
        echo    options = {"py2exe": {"packages": ["encodings"]}})
        echo.
goto:eof


:FileNotFound
        echo.
        echo Error, File not found:
        echo [%1]
        echo.
        echo Check Path in %~nx0???
        echo.
        pause
        exit
goto:eof
