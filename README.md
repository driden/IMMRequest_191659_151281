# IMMRequest_191659_151281


[![Build status](https://dev.azure.com/IMMRequest/IMMRequest/_apis/build/status/IMMRequest-ASP.NET%20Core-CI)](https://dev.azure.com/IMMRequest/IMMRequest/_build/latest?definitionId=3)

## TODO
### Backend
* [ ] Crear un endpoint para poder llamar a la dll que importa los archivos, pasar un objeto `{content: string|byte[], format: string}`

### Frontend
* [ ] Crear listado de requests
* [ ] Crear pagina para agregar más admins
* [ ] Hacer que el listado de request rediriga a la request seleccionada
* [ ] Componente de reportes A (elegir mejor nombre)
* [ ] Componente de reportes B (elegir mejor nombre)
* [ ] Eliminar un tipo
* [ ] Adaptar los compoonentes para que muestren su contenido según usuario logueado o no
* [ ] Authorization guards para que no se pueda navegar directo a un componente
* [ ] _bonus_ hacer otro router solo para reportes


# Docker command
`docker-compse up -d`
