namespace Grupo1.HolaMundo;

public static class GeneradorCodigo
{
    public static string Generar(ProgramaHolaMundo programa)
    {
        string mensaje = Escapar(programa.Mensaje);

        return $$"""
        using System;

        internal static class Program
        {
            private static void Main()
            {
                Console.WriteLine("{{mensaje}}");
            }
        }
        """;
    }

    private static string Escapar(string texto)
    {
        return texto
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n");
    }
}
