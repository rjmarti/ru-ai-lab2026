* EN el RF-08 definir bien el contrato de la API que consulta el SSO. Por ejemplo: qué recibe —username, emisor y aplicación—, qué responde —allowed true/false, usuario encontrado/no encontrado, permiso vencido—, qué códigos de error usa y en cuánto tiempo debe responder.

* Reformular los RF de la manera de “El sistema debe permitir…”. Por ejemplo: “El sistema debe permitir crear una credencial asociada a un usuario, validando que username + emisor sea único”.

* Reformular los criterios de aceptación para estar más cerca de Dado/Cuando/Entonces y probar casos reales. Por ejemplo: “Dada una credencial válida y una aplicación con permiso vigente, cuando el SSO consulta la API, entonces el sistema responde allowed=true”. También sumar casos para permiso vencido, usuario dado de baja, app inexistente, credencial inexistente y períodos solapados.

