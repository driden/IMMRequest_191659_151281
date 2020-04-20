# IMMRequest_191659_151281


[![Build status](https://dev.azure.com/IMMRequest/IMMRequest/_apis/build/status/IMMRequest-ASP.NET%20Core-CI)](https://dev.azure.com/IMMRequest/IMMRequest/_build/latest?definitionId=3)

# REST Api documentation
## Add a new request
```
POST /api/requests
{
  "details": "some details",
  "email": "email@citizen.com",
  "name": "citizen's name",
  "phone": "5555-555-555",
  "topicId": 1
}
```
Retuns a 200 if everything was ok
### TODO
* [ ] Make it return the Id of the newly created Request for tracking
* Return 400 si
    * [ ] detalles tiene mas de 2000 caracteres
    * [ ] email no tien formate correcto
    * [ ] phone no acepta caracteres espaciales o no tiene numeros
    * [ ] nombre vacio


# Docker command
docker run \
--name sqlserver \
-e 'ACCEPT_EULA=Y' \
-e 'SA_PASSWORD=Pass1234' \
-e 'MSSQL_PID=Express' \
-p 1401:1433 \
-d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
