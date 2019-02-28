@echo off
title {srvname} state
tasklist|findstr {exefile}
ping 127.0.0.1>nul