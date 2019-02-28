@echo off
title {srvname} reopen
cd /d "{exedir}"
{exefile} -s reopen
ping 127.0.0.1>nul