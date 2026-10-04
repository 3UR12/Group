# Mini-compilador de lenguaje para formularios

## Entrada

```text
FORMULARIO "Registro"
ETIQUETA "Nombre"
CAMPO nombre
ETIQUETA "Correo"
CAMPO correo
BOTON "Guardar"
FIN_FORMULARIO
```

## Elementos reconocidos

```text
FORMULARIO       inicio del formulario
ETIQUETA         texto visible
CAMPO            campo de entrada
BOTON            botón de acción
FIN_FORMULARIO   cierre del formulario
```

## Salida

Generamos código C# para Windows Forms con una ventana, etiquetas, campos de texto y botones definidos desde nuestro lenguaje fuente.

## Resultado

Una entrada válida produce una aplicación de escritorio compilable. Las instrucciones incompletas, desconocidas o fuera del bloque del formulario generan mensajes de error controlados.
