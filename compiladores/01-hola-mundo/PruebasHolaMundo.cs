namespace Grupo1.HolaMundo;

public static class PruebasHolaMundo
{
    public static int Ejecutar()
    {
        var casos = new[]
        {
            new CasoPrueba(
                "Impresión directa",
                "IMPRIMIR \"Hola Mundo\"",
                true,
                "Hola Mundo"),
            new CasoPrueba(
                "Programa completo",
                "INICIO\nIMPRIMIR \"Hola Mundo UIP\"\nFIN",
                true,
                "Hola Mundo UIP"),
            new CasoPrueba(
                "Falta cadena",
                "IMPRIMIR",
                false,
                null),
            new CasoPrueba(
                "Falta FIN",
                "INICIO\nIMPRIMIR \"Hola\"",
                false,
                null),
            new CasoPrueba(
                "Palabra desconocida",
                "MOSTRAR \"Hola\"",
                false,
                null)
        };

        int correctas = 0;
        var compilador = new CompiladorHolaMundo();

        foreach (CasoPrueba caso in casos)
        {
            bool aprobada = EjecutarCaso(compilador, caso);

            Console.WriteLine(
                $"{(aprobada ? "OK" : "ERROR"),-5} {caso.Nombre}");

            if (aprobada)
            {
                correctas++;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Pruebas correctas: {correctas}/{casos.Length}");

        return correctas == casos.Length ? 0 : 1;
    }

    private static bool EjecutarCaso(
        CompiladorHolaMundo compilador,
        CasoPrueba caso)
    {
        try
        {
            ResultadoCompilacion resultado = compilador.Compilar(caso.Fuente);

            return caso.EsValida
                && resultado.Programa.Mensaje == caso.MensajeEsperado
                && resultado.CodigoCSharp.Contains("Console.WriteLine", StringComparison.Ordinal);
        }
        catch (ErrorCompilacion)
        {
            return !caso.EsValida;
        }
    }

    private sealed record CasoPrueba(
        string Nombre,
        string Fuente,
        bool EsValida,
        string? MensajeEsperado);
}
