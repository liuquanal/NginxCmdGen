@echo off
title {srvname} install
set _SERVICE={srvname}
set _HOME=%cd%

WinSrvD\WinSrvD.exe "//IS//%_SERVICE%" ^
        --DisplayName="%_SERVICE%" ^
        --Description="%_SERVICE%" ^
        --Startup=auto --StartMode=exe ^
        --StartPath=%_HOME% ^
        --StartImage=%_HOME%\{startfile} ^
        --StopPath=%_HOME% ^
        --StopImage=%_HOME%\{stopfile} ^
        --StopMode=exe --StopTimeout=5 ^
	--LogPath=%_HOME%\logs --LogPrefix=%_SERVICE%-wrapper ^
        --PidFile=%_SERVICE%.pid --LogLevel=Info --StdOutput=auto --StdError=auto
sc query %_SERVICE% | findstr %_SERVICE% && echo service install success.
ping 127.0.0.1>nul