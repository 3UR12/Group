# Mini-compilador Hola Mundo

## Lenguaje fuente

Aceptamos una instrucción directa:

```text
IMPRIMIR "Hola Mundo"
```

o un programa completo:

```text
INICIO
IMPRIMIR "Hola Mundo UIP"
FIN
```

## Tokens

```text
INICIO          palabra reservada
IMPRIMIR        palabra reservada
"Hola Mundo"    cadena
FIN             palabra reservada
```

## Reglas sintácticas

```text
programa    → INICIO instruccion FIN | instruccion
instruccion → IMPRIMIR CADENA
```

## Código generado

```csharp
using System;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Hola Mundo UIP");
    }
}
```

## Ejecución

Desde la raíz del repositorio:

```bash
dotnet run --project compiladores/01-hola-mundo/HolaMundo.csproj
```

El compilador genera un proyecto C# dentro de `salida/hola-mundo/ProgramaGenerado`, lo compila con .NET y muestra el resultado de la ejecución.

## Pruebas

```bash
dotnet run --project compiladores/01-hola-mundo/HolaMundo.csproj -- --pruebas
```

Las pruebas cubren entradas válidas, ausencia de cadena, falta de `FIN` y palabras no reconocidas.
