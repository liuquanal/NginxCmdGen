@echo off
title {srvname}
cd /d "{exedir}"
tasklist|findstr {exefile} && taskkill /f /im {exefile}
echo {srvname} running ...
start /b "{srvname}" "{bindir}{exefile}" {args}
echo start cmd send.
ping 127.0.0.1>nul