# IMMRequest_191659_151281


[![Build Status](https://dev.azure.com/driden87/IMMRequest/_apis/build/status/IMMRequest-ASP.NET%20Core-CI?branchName=develop)](https://dev.azure.com/driden87/IMMRequest/_build/latest?definitionId=3&branchName=develop)



# Docker command
docker run \
--name sqlserver \
-e 'ACCEPT_EULA=Y' \
-e 'SA_PASSWORD=Pass1234' \
-e 'MSSQL_PID=Express' \
-p 1401:1433 \
-d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
