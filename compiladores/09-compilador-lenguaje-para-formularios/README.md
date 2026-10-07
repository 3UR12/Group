# 09 - Compilador de lenguaje para formularios

## Descripción

Este mini-compilador interpreta un lenguaje sencillo para describir formularios y genera una aplicación Windows Forms en C#.

El usuario escribe instrucciones como:

```text
FORMULARIO "Registro"
ETIQUETA "Nombre"
CAMPO nombre
ETIQUETA "Correo"
CAMPO correo
BOTON "Guardar"
FIN_FORMULARIO
```

El programa realiza análisis léxico, análisis sintáctico, una validación semántica básica, genera código C#, compila ese código y permite abrir el formulario resultante.

## Objetivo

Representar de forma visible el proceso:

```text
Lenguaje fuente
↓
Análisis léxico
↓
Análisis sintáctico
↓
Validación semántica básica
↓
Generación de C#
↓
Compilación
↓
Aplicación Windows Forms
```

## Nivel

Intermedio.

## Producto esperado

Aplicación Windows Forms generada a partir del lenguaje fuente escrito por el usuario.

El formulario no está definido de forma fija: el título y los controles dependen de las instrucciones proporcionadas.

## Tecnologías

- C#.
- .NET 8.
- Windows Forms.
- `System.Diagnostics.Process` para compilar el código C# generado.
- Codificación UTF-8 para mensajes y archivos generados.

## Requisitos

- Windows 10 u 11.
- .NET 8 SDK.
- Visual Studio 2022 con la carga de trabajo de desarrollo de escritorio de .NET, o una terminal con acceso al comando `dotnet`.

## Cómo ejecutar

### Desde Visual Studio 2022

1. Abrir `CompiladoresGrupo1.sln`.
2. Seleccionar `MiniCompiladorFormularios`.
3. Establecerlo como proyecto de inicio.
4. Ejecutar con `F5` o `Ctrl + F5`.

### Desde PowerShell

Desde la raíz del repositorio:

```powershell
dotnet run --project ".\compiladores\09-compilador-lenguaje-para-formularios\MiniCompiladorFormularios.csproj"
```

## Interfaz

La ventana principal contiene cinco acciones:

| Acción | Función |
|---|---|
| `Cargar ejemplo` | Inserta un ejemplo válido en el área de código fuente. |
| `Analizar` | Ejecuta el análisis léxico y sintáctico, muestra los tokens y genera el C# si la entrada es válida. |
| `Generar formulario` | Analiza, genera el C# y compila una aplicación Windows Forms. |
| `Abrir formulario generado` | Ejecuta la aplicación Windows Forms generada previamente. |
| `Limpiar` | Borra la entrada y los resultados de la interfaz. |

La interfaz también muestra:

- código fuente;
- tabla de tokens;
- código C# generado;
- resultado del análisis o compilación;
- estado actual del proceso.

## Lenguaje fuente

La primera instrucción debe definir el formulario:

```text
FORMULARIO "Título"
```

Después pueden utilizarse etiquetas, campos y botones:

```text
ETIQUETA "Texto"
CAMPO identificador
BOTON "Texto"
```

El programa debe terminar con:

```text
FIN_FORMULARIO
```

## Palabras reservadas

| Palabra | Función |
|---|---|
| `FORMULARIO` | Inicia la definición y establece el título de la ventana. |
| `ETIQUETA` | Crea una etiqueta visible. |
| `CAMPO` | Crea un cuadro de texto identificado por un nombre. |
| `BOTON` | Crea un botón con el texto indicado. |
| `FIN_FORMULARIO` | Finaliza la definición del formulario. |

## Tokens reconocidos

El analizador utiliza estos tipos de token:

| Tipo de token | Uso |
|---|---|
| `Formulario` | Palabra reservada `FORMULARIO`. |
| `Etiqueta` | Palabra reservada `ETIQUETA`. |
| `Campo` | Palabra reservada `CAMPO`. |
| `Boton` | Palabra reservada `BOTON`. |
| `FinFormulario` | Palabra reservada `FIN_FORMULARIO`. |
| `Cadena` | Texto escrito entre comillas dobles. |
| `Identificador` | Nombre utilizado para identificar un campo. |
| `FinArchivo` | Marca interna que indica el final de la entrada. |

Para:

```text
FORMULARIO "Registro"
ETIQUETA "Nombre"
CAMPO nombre
BOTON "Guardar"
FIN_FORMULARIO
```

se reconocen unidades equivalentes a:

| Entrada | Tipo |
|---|---|
| `FORMULARIO` | `Formulario` |
| `Registro` | `Cadena` |
| `ETIQUETA` | `Etiqueta` |
| `Nombre` | `Cadena` |
| `CAMPO` | `Campo` |
| `nombre` | `Identificador` |
| `BOTON` | `Boton` |
| `Guardar` | `Cadena` |
| `FIN_FORMULARIO` | `FinFormulario` |

## Análisis sintáctico

El parser exige esta estructura:

1. `FORMULARIO` debe aparecer primero.
2. `FORMULARIO` debe ir seguido de un título entre comillas.
3. `ETIQUETA` debe ir seguida de una cadena.
4. `CAMPO` debe ir seguido de un identificador.
5. `BOTON` debe ir seguido de una cadena.
6. La definición debe terminar con `FIN_FORMULARIO`.
7. No se acepta contenido adicional después de `FIN_FORMULARIO`.

## Validación semántica básica

Los identificadores de los campos no pueden repetirse dentro del mismo formulario.

Ejemplo inválido:

```text
FORMULARIO "Registro"
CAMPO nombre
CAMPO nombre
FIN_FORMULARIO
```

El programa informa que el campo `nombre` está repetido.

## Generación de código C#

Cada instrucción se transforma en un control real de Windows Forms:

| Lenguaje fuente | C# generado |
|---|---|
| `FORMULARIO "Registro"` | `new Form` con `Text = "Registro"` |
| `ETIQUETA "Nombre"` | `new Label` |
| `CAMPO nombre` | `new TextBox` con `Name = "nombre"` |
| `BOTON "Guardar"` | `new Button` |

El código generado se presenta con saltos de línea e indentación para que pueda revisarse en la interfaz.

## Caso válido

Entrada:

```text
FORMULARIO "Registro"
ETIQUETA "Nombre"
CAMPO nombre
ETIQUETA "Correo"
CAMPO correo
BOTON "Guardar"
FIN_FORMULARIO
```

Proceso esperado:

1. `Analizar` muestra los tokens y el mensaje `El formulario es válido.`.
2. En `Código C# generado` aparece un programa Windows Forms legible.
3. `Generar formulario` compila el programa.
4. El resultado muestra:

```text
Compilación correcta.
0 advertencias
0 errores
```

5. `Abrir formulario generado` abre una ventana titulada `Registro` con etiquetas, campos y el botón `Guardar`.

## Caso inválido: CAMPO sin identificador

Entrada:

```text
FORMULARIO "Registro"
CAMPO
FIN_FORMULARIO
```

Mensaje esperado:

```text
Error sintáctico: falta el identificador del campo.
```

## Caso inválido: falta FIN_FORMULARIO

Entrada:

```text
FORMULARIO "Registro"
ETIQUETA "Nombre"
CAMPO nombre
```

Mensaje esperado:

```text
Error sintáctico: falta FIN_FORMULARIO.
```

## Caso inválido: cadena sin cerrar

Entrada:

```text
FORMULARIO "Registro
FIN_FORMULARIO
```

El analizador léxico informa que la cadena no tiene comillas de cierre e indica la línea donde se encontró el problema.

## Compilación del formulario generado

Cuando se selecciona `Generar formulario`:

1. el código C# generado se escribe en una carpeta temporal;
2. se crea un proyecto temporal de Windows Forms para .NET 8;
3. se ejecuta `dotnet build`;
4. si no existen errores, se conserva la ruta del ejecutable temporal;
5. el botón `Abrir formulario generado` ejecuta ese archivo.

Los archivos temporales generados por este proceso no se incluyen en el repositorio.

## Archivos del proyecto

| Archivo | Función |
|---|---|
| `Program.cs` | Contiene lexer, parser, validación semántica, generador de C#, compilación y la interfaz Windows Forms del mini-compilador. |
| `MiniCompiladorFormularios.csproj` | Define el proyecto Windows Forms en .NET 8. |
| `README.md` | Documenta sintaxis, tokens, pruebas, errores y uso de la aplicación. |

## Alcance

El lenguaje implementado crea controles básicos y está diseñado como ejemplo académico. No incluye persistencia de datos, eventos personalizados del botón, conexión a bases de datos ni diseño avanzado de interfaces.

## Integrantes

- Daniela Insturaín
- Aaron Fechrenback
- Euris J. Rodríguez V.
