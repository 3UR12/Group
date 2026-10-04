using System.Text;
using Grupo1.HolaMundo;

Console.OutputEncoding = Encoding.UTF8;

if (args.Contains("--pruebas", StringComparer.OrdinalIgnoreCase))
{
    Environment.ExitCode = PruebasHolaMundo.Ejecutar();
    return;
}

Console.WriteLine("Mini-compilador Hola Mundo");
Console.WriteLine("Entrada válida:");
Console.WriteLine("  IMPRIMIR \"Hola Mundo\"");
Console.WriteLine("o");
Console.WriteLine("  INICIO");
Console.WriteLine("  IMPRIMIR \"Hola Mundo\"");
Console.WriteLine("  FIN");
Console.WriteLine();
Console.WriteLine("Escriba la entrada. Finalice con una línea vacía o con FIN:");

var lineas = new List<string>();

while (true)
{
    string? linea = Console.ReadLine();

    if (linea is null || string.IsNullOrWhiteSpace(linea))
    {
        break;
    }

    lineas.Add(linea);

    if (linea.Trim() == "FIN")
    {
        break;
    }
}

string fuente = string.Join(Environment.NewLine, lineas);

try
{
    var compilador = new CompiladorHolaMundo();
    ResultadoCompilacion resultado = compilador.Compilar(fuente);

    Console.WriteLine();
    Console.WriteLine("Tokens:");

    foreach (Token token in resultado.Tokens.Where(t => t.Tipo != TipoToken.FinArchivo))
    {
        Console.WriteLine($"  {token.Tipo,-12} {token.Lexema}");
    }

    Console.WriteLine();
    Console.WriteLine("Código C# generado:");
    Console.WriteLine();
    Console.WriteLine(resultado.CodigoCSharp);

    var compiladorDotNet = new CompiladorDotNet();
    ResultadoEjecucion ejecucion = compiladorDotNet.CompilarYEjecutar(resultado.CodigoCSharp);

    Console.WriteLine();
    Console.WriteLine($"Archivo C#: {ejecucion.RutaCodigo}");

    if (ejecucion.RutaEjecutable is not null)
    {
        Console.WriteLine($"Ejecutable: {ejecucion.RutaEjecutable}");
    }

    Console.WriteLine();
    Console.WriteLine("Resultado de ejecución:");
    Console.WriteLine(ejecucion.Salida);
}
catch (ErrorCompilacion error)
{
    Console.WriteLine();
    Console.WriteLine(error.Message);
    Environment.ExitCode = 1;
}
