# Arquitectura del Grupo 1

Nuestros tres mini-compiladores mantienen el mismo flujo general para facilitar las pruebas, la documentación y la integración con el portal.

```text
Entrada
  ↓
Analizador léxico
  ↓
Analizador sintáctico
  ↓
Generador de código
  ↓
Código C#
  ↓
Compilación y ejecución
  ↓
Resultado
```

## Módulos

```text
compiladores/
├── 01-hola-mundo/
├── 02-estructuras-if/
└── 03-formularios/
```

### Hola Mundo

Reconocemos una instrucción `IMPRIMIR` seguida de una cadena y generamos su equivalente en C#.

### Estructuras IF

Reconocemos declaraciones simples, condiciones y bloques `SI`, `SINO` y `FIN_SI`, y generamos estructuras `if/else` en C#.

### Lenguaje para formularios

Reconocemos instrucciones para definir un formulario, etiquetas, campos y botones, y generamos una aplicación Windows Forms.

## Separación de responsabilidades

Cada módulo mantiene separadas las tareas de lectura, análisis, validación y generación de código. Esta separación permite probar cada etapa sin mezclar la lógica del compilador con la presentación del resultado.
