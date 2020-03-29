# IMMRequest_191659_151281

Preguntas:
- Los campos adicionales son todos obligatorios?












# Docker command
docker run \
--name sqlserver \
-e 'ACCEPT_EULA=Y' \
-e 'SA_PASSWORD=Pass1234' \
-e 'MSSQL_PID=Express' \
-p 1401:1433 \
-d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
