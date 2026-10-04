# Mini-compilador de estructuras IF

## Entrada

```text
ENTERO edad = 20
SI edad >= 18 ENTONCES
    IMPRIMIR "Mayor de edad"
SINO
    IMPRIMIR "Menor de edad"
FIN_SI
```

## Elementos reconocidos

```text
ENTERO      tipo
edad        identificador
=           asignación
20          número
SI          palabra reservada
>=          operador relacional
ENTONCES    palabra reservada
IMPRIMIR    palabra reservada
SINO        palabra reservada
FIN_SI      palabra reservada
```

## Salida C#

```csharp
int edad = 20;

if (edad >= 18)
{
    Console.WriteLine("Mayor de edad");
}
else
{
    Console.WriteLine("Menor de edad");
}
```

## Resultado

Traducimos una condición del lenguaje fuente a una estructura `if/else` válida en C# y mostramos errores controlados cuando la condición o el bloque no cumplen la sintaxis definida.
