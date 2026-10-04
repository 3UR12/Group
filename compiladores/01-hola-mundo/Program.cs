using System.Text;
using Grupo1.HolaMundo;

Console.OutputEncoding = Encoding.UTF8;

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

    string carpetaSalida = Path.Combine(
        Directory.GetCurrentDirectory(),
        "salida",
        "hola-mundo");

    Directory.CreateDirectory(carpetaSalida);

    string rutaCodigo = Path.Combine(carpetaSalida, "ProgramaGenerado.cs");
    File.WriteAllText(rutaCodigo, resultado.CodigoCSharp, Encoding.UTF8);

    Console.WriteLine();
    Console.WriteLine($"Archivo generado: {rutaCodigo}");
}
catch (ErrorCompilacion error)
{
    Console.WriteLine();
    Console.WriteLine(error.Message);
    Environment.ExitCode = 1;
}
