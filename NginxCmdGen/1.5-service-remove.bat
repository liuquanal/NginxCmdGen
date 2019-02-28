@echo off
title {srvname} remove
sc stop {srvname}>nul
sc delete {srvname}
ping 127.0.0.1>nul