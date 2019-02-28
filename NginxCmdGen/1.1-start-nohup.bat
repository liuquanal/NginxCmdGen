@echo off
title {srvname}
cd /d "{exedir}"
:Repeat
tasklist|findstr {exefile} && taskkill /f /im {exefile}
echo {srvname} running ...
"{bindir}{exefile}" {args}
echo start cmd send.
ping 127.0.0.1>nul
echo process restart
goto Repeat