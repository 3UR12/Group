using System.Text;
using System.Text.RegularExpressions;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
Console.WriteLine("Mini-compilador de estructuras IF\n");
Console.WriteLine("Escriba el código fuente línea por línea.");
Console.WriteLine("Las líneas vacías se permiten.");
Console.WriteLine("Cuando termine, escriba EJECUTAR en una línea independiente.");
Console.WriteLine("\nCódigo >");
var lineas = new List<string>();
while (true) { var linea = Console.ReadLine(); if (linea is null || linea.Trim() == "EJECUTAR") break; lineas.Add(linea); }
var fuente = string.Join(Environment.NewLine, lineas);
try
{
    var tokens = Regex.Matches(fuente, "\\\"[^\\\"]*\\\"|>=|<=|==|!=|[=><]|[A-Za-z_][A-Za-z0-9_]*|\\d+(?:\\.\\d+)?").Select(x => x.Value).ToList();
    var p = 0;
    string Tomar(string esperado, string error) { if (p >= tokens.Count || tokens[p] != esperado) throw new Exception(error); return tokens[p++]; }
    if (tokens.Count < 4) throw new Exception("Error sintáctico: la declaración está incompleta.");
    var tipoFuente = tokens[p++];
    if (tipoFuente is not ("ENTERO" or "DECIMAL" or "TEXTO")) throw new Exception("Error sintáctico: se esperaba un tipo de variable.");
    if (p >= tokens.Count || !Regex.IsMatch(tokens[p], "^[A-Za-z_][A-Za-z0-9_]*$")) throw new Exception("Error sintáctico: se esperaba un identificador.");
    var nombre = tokens[p++]; Tomar("=", "Error sintáctico: se esperaba = en la declaración.");
    if (p >= tokens.Count) throw new Exception("Error sintáctico: la declaración está incompleta.");
    var valor = tokens[p++]; Tomar("SI", "Error sintáctico: se esperaba SI."); Tomar(nombre, "Error sintáctico: se esperaba la variable declarada en la condición.");
    if (p >= tokens.Count || !new[] { ">", "<", ">=", "<=", "==", "!=" }.Contains(tokens[p])) throw new Exception("Error sintáctico: condición incompleta.");
    var operador = tokens[p++]; if (p >= tokens.Count) throw new Exception("Error sintáctico: condición incompleta."); var comparacion = tokens[p++];
    Tomar("ENTONCES", "Error sintáctico: se esperaba ENTONCES después de la condición."); Tomar("IMPRIMIR", "Error sintáctico: se esperaba IMPRIMIR.");
    if (p >= tokens.Count || !tokens[p].StartsWith('"')) throw new Exception("Error sintáctico: IMPRIMIR requiere una cadena."); var verdadero = tokens[p++].Trim('"'); string? falso = null;
    if (p < tokens.Count && tokens[p] == "SINO") { p++; Tomar("IMPRIMIR", "Error sintáctico: se esperaba IMPRIMIR después de SINO."); if (p >= tokens.Count || !tokens[p].StartsWith('"')) throw new Exception("Error sintáctico: IMPRIMIR requiere una cadena."); falso = tokens[p++].Trim('"'); }
    Tomar("FIN_SI", "Error sintáctico: falta FIN_SI."); if (p != tokens.Count) throw new Exception("Error sintáctico: hay texto adicional no válido.");
    var tipoCs = tipoFuente == "ENTERO" ? "int" : tipoFuente == "DECIMAL" ? "decimal" : "string";
    var codigo = $"using System;\n\nclass Program\n{{\n    static void Main()\n    {{\n        {tipoCs} {nombre} = {valor};\n\n        if ({nombre} {operador} {comparacion})\n        {{\n            Console.WriteLine(\"{verdadero}\");\n        }}" + (falso is null ? "" : $"\n        else\n        {{\n            Console.WriteLine(\"{falso}\");\n        }}") + "\n    }\n}";
    Console.WriteLine("\nCódigo fuente:\n" + fuente + "\n\nTokens:"); foreach (var token in tokens) Console.WriteLine(token);
    Console.WriteLine("\nAnálisis sintáctico: correcto.\n\nCódigo C# generado:\n" + codigo);
    var esVerdadero = int.TryParse(valor, out var izquierda) && int.TryParse(comparacion, out var derecha) && (operador switch { ">=" => izquierda >= derecha, ">" => izquierda > derecha, "<=" => izquierda <= derecha, "<" => izquierda < derecha, "==" => izquierda == derecha, _ => izquierda != derecha });
    Console.WriteLine("\nResultado:\n" + (esVerdadero ? verdadero : falso ?? string.Empty));
}
catch (Exception error) { Console.WriteLine(error.Message); }
