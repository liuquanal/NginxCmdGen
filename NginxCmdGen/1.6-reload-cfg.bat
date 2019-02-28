@echo off
title {srvname} reload
cd /d "{exedir}"
{exefile} -s reload
ping 127.0.0.1>nul