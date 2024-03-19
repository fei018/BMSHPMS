echo publish...
set web=\\192.168.0.201\www\bmshpms
set webconfig=\\192.168.0.201\www\bmshpms\web.config

rem robocopy %web%\ %web%_old\ /Mir /NP /TEE /R:0

echo .. >> %webconfig%
ping 127.0.0.1 -n 5 > Nul

rem del /S /Q %web%\*

robocopy .\publish %web%\ /Mir /TEE /R:0 /NP

copy appsettings.json \\192.168.0.201\www\bmshpms\appsettings.json /y

pause