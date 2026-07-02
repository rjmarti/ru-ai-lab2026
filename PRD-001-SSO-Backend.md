# PRD-001: Backend para un Single Sign on (SSO)

## Contexto y Problema
Nuestro organismo tiene un montón de aplicaciones y cada usuario ingresa a cada una de ellas con distintas credenciales.
La gente de Seguridad Informática (SI) se cansó de administras tantos usuarios y responder ante los olvidos de credenciales en los distintos sistemas y va a implementar un Single Sign on (SSO).
La implementación del SSO queda afuera del alcance de este proyecto.
Seguridad Informática (SI) necesita tener una base dedatos para administrar los accesos a todas las aplicaciones y ser la fuente de consulta del SSO.


### Actores:
Seguridad Informática (SI): son los que van a administrar esta aplicación.
SSO: Es un servicio que recive las credenciales de un servicio de autenticación y busca si el usuario que tiene dichas credenciales, puede acceder a la apicación o no.
Usuario Final: Es quien accede a las aplicaciones y es autenticado por un servicio externo y sus credenciales utilizadas para validar sus permisos.

## Objetivos
Que se puedan administrar Credenciales, Usuarios y Aplicaciones, teneindo toda la información centralizada.
Tambien tomar acciones como dar de baja un usuario en forma centralizada, sin tener que entrar en cada una de las aplicaciones existentes, proceso que hoy toma horas.


## Requerimientos Funcionales
- RF-01: Las credenciales deben tener un username y un emisor. La combinación (username + emisor) debe ser única en el sistema.
- RF-02: Un usuario pueden tener varias credenciales de varios emisores distintos.
- RF-03: Las aplicaciones deben tener un nombre y una url asociada.
- RF-04: Para dar permisos de acceso a una aplicación se debe crear un registro para el usuario con fecha-desde y hasta que dura el permiso. Si no se establece nada la fecha-hasta queda en nulo. Los periodos no debe solaparse
- RF-05: Se puede dar de baja el perimiso de acceso de un usuario a una aplicación, simplemente poniendo la fecha de hoy.
- RF-06: La baja de un usuario será lógica y deberá caducar todos los permisos en todas las apilaciones a las que tenga acceso.
- RF-08: Debe exponer un servicio (API-REST) para que el SSO consulte con credencial y aplicacion y devuelva si el usaurio tiene permisos o no.
- RF-07: En principio no tendrá login y todos los que accedan serán administradores de Seguridad Informática


## Requerimientos No Funcionales
- RNF-01: La consulta de dada una credencial, identificar al usuario y verificar si tiene acceso a la aplicacion debe ser < 500 ms. Tomemos que tendremos unas 100 aplicaciones definidas y 3000 usuarios.
- RNF-02: Si un usuario de da de baja, se le tienen que eliminar todos los permisos en todas las aplicaciones en < 3 s 
- RNF-03: No deben almacenarse ninguna contraseña, solo el emisor de la credencial y su username.

## Criterios de Aceptación
- AC-01 (RF-01): Comprobar que la consulta de si una credencial puede acceder a una aplicación es menor al tiempo estipulado
- AC-02 (RF-02): Si una misma credencial se quiere dar de alta a dos usuarios distintos debe dar error.
- AC-03 (RF-03): Verificar que no tengan url vacias.
- AC-04 (RF-04, RF-05): El periodo de los distintos permisos de un usuario para una misma aplicación no deben solaparse.
- AC-05 (RF-06): Verificar que un usuario dado de baja no tenga ningun permiso activo.

## Fuera de Alcance
La implementación del Login. será en otra etapa
La implementación del SSO queda fuera del alcance de este proyecto. 
El modo que el SSO consulta esta base también.

## Riesgos y Dependencias
- Riesgo: No tener login en esta etapa implica un riesgo
- Riesgo: Inconsistencias debido a credenciales repetidas.
- Riesgo: Permisos duplicados para un mismo periodo.
- Dependencia: SQLite - se utiliza para maquetar el MPV (Mínimo producto viable). Luego se utilizaran una base de datos mas robusta.
   
```