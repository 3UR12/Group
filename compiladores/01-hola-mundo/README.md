# Mini-compilador Hola Mundo

## Entrada

```text
IMPRIMIR "Hola Mundo"
```

## Tokens principales

```text
IMPRIMIR        PALABRA_RESERVADA
"Hola Mundo"    CADENA
```

## Regla sintáctica

```text
instruccion → IMPRIMIR CADENA
```

## Salida C#

```csharp
Console.WriteLine("Hola Mundo");
```

## Resultado

Generamos código C# válido a partir de una instrucción de impresión y controlamos entradas incompletas o con estructura incorrecta.
