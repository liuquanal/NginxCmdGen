@echo off
title stop {srvname}
tasklist|findstr {exefile} && taskkill /f /im {exefile}
echo stop cmd send.
ping 127.0.0.1>nul