echo publish...
set web=\\bmshserver01\www\bmshpms
set webconfig=\\bmshserver01\www\bmshpms\web.config

robocopy %web%\ %web%_old\ /Mir /NP /TEE /R:0

echo .. >> %webconfig%
ping 127.0.0.1 -n 5 > Nul

rem del /S /Q %web%\*

robocopy .\publish %web%\ /Mir /TEE /R:0 /NP

copy appsettings.json \\bmshserver01\www\bmshpms\appsettings.json /y

pause