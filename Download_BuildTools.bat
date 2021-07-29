@echo off
Mkdir buildtools
Cd buildtools
Mkdir py2exe
Cd py2exe
Echo Downloading BuildTools from Github repo...


Powershell -Command "Invoke-WebRequest https://github.com/py2exe/py2exe/releases/download/v0.10.4.0/py2exe-0.10.4.0.tar.gz -Outfile py2exe.tar.gz"
Echo Expanding archive...

Wsl gzip -d py2exe.tar.gz
