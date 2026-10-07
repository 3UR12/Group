using System.Text;
using System.Text.RegularExpressions;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
Console.WriteLine("Mini-compilador \"Hola Mundo\"");
Console.WriteLine("\nEscriba una instrucción con este formato:\n\nIMPRIMIR \"texto\"\n\nEjemplo:\nIMPRIMIR \"Hola Mundo\"");
Console.Write("\nCódigo > ");
var fuente = Console.ReadLine() ?? string.Empty;
try
{
    var tokens = new List<(string Texto, string Tipo)>();
    foreach (Match coincidencia in Regex.Matches(fuente, "\\\"[^\\\"]*\\\"|\\S+")) { var texto = coincidencia.Value; var tipo = texto switch { "IMPRIMIR" => "PALABRA_RESERVADA", _ when texto.StartsWith('"') && texto.EndsWith('"') => "CADENA", _ => throw new Exception($"Error léxico: '{texto}' no es una palabra reservada válida.") }; tokens.Add((texto, tipo)); }
    if (tokens.Count == 0 || tokens[0].Texto != "IMPRIMIR") throw new Exception("Error sintáctico: se esperaba IMPRIMIR.");
    if (tokens.Count < 2 || tokens[1].Tipo != "CADENA") throw new Exception("Error sintáctico: se esperaba una cadena después de IMPRIMIR.");
    if (tokens.Count > 2) throw new Exception("Error sintáctico: hay texto adicional no válido.");
    var mensaje = tokens[1].Texto.Trim('"');
    Console.WriteLine("\nCódigo fuente recibido:\n" + fuente + "\n\nTokens encontrados:"); foreach (var token in tokens) Console.WriteLine($"{token.Texto,-22}{token.Tipo}");
    Console.WriteLine("\nAnálisis sintáctico: correcto.\n\nCódigo C# generado:\nusing System;\n\nclass Program\n{\n    static void Main()\n    {\n        Console.WriteLine(\"" + mensaje + "\");\n    }\n}");
    Console.WriteLine("\nResultado:\n" + mensaje);
}
catch (Exception error) { Console.WriteLine(error.Message); }
