<div align="center">

# Compiladores · Grupo 1

Universidad Interamericana de Panamá  
Curso: Compiladores  
2026

Daniela Insturaín · Aaron Fechrenback · Euris J. Rodríguez V.

</div>

Nuestro grupo desarrolla tres mini-compiladores:

1. Hola Mundo
2. Estructuras IF
3. Lenguaje para formularios

Código fuente → Análisis léxico → Análisis sintáctico → Generación de código → Compilación → Resultado

Tecnologías: C#, .NET 8 y Visual Studio 2022. Windows Forms se utiliza solamente para el compilador de formularios.

```text
CompiladoresGrupo1.sln
compiladores/
├── 01-hola-mundo/
├── 06-estructuras-if/
└── 09-formularios/
```

Abrir `CompiladoresGrupo1.sln` con Visual Studio 2022 y seleccionar el proyecto que se desea ejecutar.

**Hola Mundo** es una aplicación de consola y acepta `IMPRIMIR "Hola Mundo"`.

**Estructuras IF** es una aplicación de consola y acepta declaraciones como `ENTERO edad = 20` y condiciones `SI edad >= 18 ENTONCES`.

**Lenguaje para formularios** acepta instrucciones como `FORMULARIO "Registro"`, `ETIQUETA "Nombre"`, `CAMPO nombre` y `BOTON "Guardar"`.
