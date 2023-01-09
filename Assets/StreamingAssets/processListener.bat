@echo off
setlocal enabledelayedexpansion

SET PID=%1
set src_dir=%2
set src_file=%3
set ext=%4
set dst_dir=%5

:checkProcess
timeout 1 >nul

tasklist /fi "pid eq %PID%" 2>NUL | find /i /n "%PID%" >NUL
if "%ERRORLEVEL%"=="0" goto checkProcess

timeout 1 >nul

for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"

set "YYYY=%dt:~0,4%"
set "MM=%dt:~4,2%"
set "DD=%dt:~6,2%"
set "HH=%dt:~8,2%"
set "Min=%dt:~10,2%"
set "Sec=%dt:~12,2%"

set "timestamp=%YYYY%%MM%%DD%_%HH%%Min%%Sec%"

set "src_path=%src_dir%\%src_file%.%ext%"
set "dst_path=%dst_dir%\%src_file%.%timestamp%.%ext%"
copy "%src_path%" "%dst_path%"