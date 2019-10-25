# Enviroment
<p>
Esta es la configuración inicial del proyecto, tambien se presenta la estructura para las clases y la forma de inyectar dependencia
.</p>

## Estructura del proyecto

<p>
Las solución se divide en 3 areas importantes:
</p>

| Area | Descripción  |
|--|--|
|Repositories|<p stype="text-allign=justify" >Carpeta donde se encuentra todas las clases encargadas de la traer datos de firebase, por lo tanto, es estas clases contendran todos los queries, pero no deberan tener lógica que se aplique sobre los objetos que sean retornados. Los repositorios son el ultimo nivel de abstracción, por lo que  **No dependeran tener dependencia de ningun otro repositorio**</p>|
| Services  |<p stype="text-allign=justify"> Carpeta donde se encuentra todas las clases que contienen la lógica del negocio, estas clases no deberan contener ninguna conexión hacia firebase; de esto se encargara las clases en los repositorios, Los servicios si pueden tener dependencia de muchos repositorios. Objetos más complejos pueden ser construidos dentro de los servicios, como la union de la respuesta de varios repositorios  </p>|
| Controllers | <p stype="text-allign=justify"> Contiene todos las clases que contienen los endpoints de la API. No deberan tener dependencia de repositorios, y pueden utilizar varios servicios.</p> |

Si se desea agreagar un nuevo repositorio o un nuevo servicio, **No olvidar de injectar la dependencia en el arcivo /Configuration/NinjectConfiguration/NinjectWebBindingConfigure.cs**

Estas parte es importante, ya que de no hacerlo el proyecto no compilara.
