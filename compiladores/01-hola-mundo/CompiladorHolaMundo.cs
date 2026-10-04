namespace Grupo1.HolaMundo;

public sealed class CompiladorHolaMundo
{
    public ResultadoCompilacion Compilar(string fuente)
    {
        var analizadorLexico = new AnalizadorLexico(fuente);
        IReadOnlyList<Token> tokens = analizadorLexico.Analizar();

        var analizadorSintactico = new AnalizadorSintactico(tokens);
        ProgramaHolaMundo programa = analizadorSintactico.Analizar();

        string codigoCSharp = GeneradorCodigo.Generar(programa);

        return new ResultadoCompilacion(tokens, programa, codigoCSharp);
    }
}
