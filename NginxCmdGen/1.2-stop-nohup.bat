@echo off
title stop {srvname}

wmic process where name='{exefile}' get ParentProcessId /value|findstr "ParentProcessId" > ./WinSrvD/StopPid.tmp
set /P _string=<./WinSrvD/StopPid.tmp
if "%_string%" neq "" set _string=%_string:~16%
if "%_string%" neq "" tasklist|findstr %_string% && taskkill /f /pid %_string%

tasklist|findstr {exefile} && taskkill /f /im {exefile}
echo stop cmd send.
ping 127.0.0.1>nul