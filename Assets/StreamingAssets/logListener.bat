@echo off
setlocal enabledelayedexpansion

set app_name=%1
set PID=%2
set src_dir=%3
set src_file=%4
set ext=%5
set dst_dir=%6

echo Log Listener started for [93;1m%app_name%[0m
echo Don't close this window; it'll make a copy
echo of the most recent log and will automatically
echo terminate when [93;1m%app_name%[0m is no longer running.

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