# Mini-compiladores — Grupo 1

Universidad Interamericana de Panamá  
Curso: Compiladores · 2026

## Integrantes

- Daniela Insturaín
- Aaron Fechrenback
- Euris J. Rodríguez V.

## Entrega

Este repositorio contiene únicamente los tres mini-compiladores asignados al grupo y los archivos necesarios para abrirlos, compilarlos y probarlos.

| # | Mini-compilador | Carpeta | Producto esperado | Nivel |
|---|---|---|---|---|
| 01 | Compilador “Hola Mundo” | `compiladores/01-compilador-hola-mundo/` | Archivo `.cs` y posteriormente `.exe` | Básico |
| 06 | Compilador de estructuras IF | `compiladores/06-compilador-estructuras-if/` | Código C# con `if` | Intermedio |
| 09 | Compilador de lenguaje para formularios | `compiladores/09-compilador-lenguaje-para-formularios/` | Aplicación Windows Forms | Intermedio |

Cada carpeta incluye su propio `README.md` con la sintaxis aceptada, tokens, análisis, casos de prueba, errores esperados y forma de ejecución.

## Requisitos

- Windows 10 u 11.
- .NET 8 SDK.
- Visual Studio 2022 con desarrollo de escritorio de .NET, o una terminal con `dotnet`.

## Abrir los tres proyectos

Con Visual Studio 2022, abrir:

```text
CompiladoresGrupo1.sln
```

Desde terminal, cada proyecto también puede ejecutarse con `dotnet run --project` usando la ruta indicada en su README.

## Estructura

```text
CompiladoresGrupo1.sln
README.md
.gitignore
compiladores/
├── 01-compilador-hola-mundo/
│   ├── MiniCompiladorHolaMundo.csproj
│   ├── Program.cs
│   └── README.md
├── 06-compilador-estructuras-if/
│   ├── MiniCompiladorEstructurasIf.csproj
│   ├── Program.cs
│   └── README.md
└── 09-compilador-lenguaje-para-formularios/
    ├── MiniCompiladorFormularios.csproj
    ├── Program.cs
    └── README.md
```

Las carpetas `bin/`, `obj/`, `salida/` y otros artefactos locales se excluyen del repositorio mediante `.gitignore`.
