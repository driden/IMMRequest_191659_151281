# IMMRequest_191659_151281


[![Build status](https://dev.azure.com/IMMRequest/IMMRequest/_apis/build/status/IMMRequest-ASP.NET%20Core-CI)](https://dev.azure.com/IMMRequest/IMMRequest/_build/latest?definitionId=3)

## TODO
### Backend
* [x] Crear un endpoint para poder llamar a la dll que importa los archivos, pasar un objeto `{content: string|byte[], format: string}`

### Frontend
* [x] Crear listado de requests
* [x] Un usuario debería pode consultar sus requests por email
* [x] Crear pagina para agregar más admins
* [ ] Crear pagina para eliminar admins
* [x] Hacer que el listado de request rediriga a la request seleccionada
* [x] Componente de reportes A (elegir mejor nombre)
* [x] Componente de reportes B (elegir mejor nombre)
* [x] Listar todos los tipos
* [x] Eliminar un tipo
* [ ] Adaptar los compoonentes para que muestren su contenido según usuario logueado o no
* [ ] Authorization guards para que no se pueda navegar directo a un componente
* [ ] Una guarda para que solo el usuario que hizo la solicitud pueda verla
* [ ] _bonus_ hacer otro router solo para reportes


# Docker command
`docker-compse up -d`
