namespace Grupo1.HolaMundo;

public sealed class AnalizadorSintactico
{
    private readonly IReadOnlyList<Token> _tokens;
    private int _posicion;

    public AnalizadorSintactico(IReadOnlyList<Token> tokens)
    {
        _tokens = tokens;
    }

    public ProgramaHolaMundo Analizar()
    {
        bool programaCompleto = Verificar(TipoToken.Inicio);

        if (programaCompleto)
        {
            Consumir(TipoToken.Inicio, "Error sintáctico: se esperaba INICIO.");
        }

        Consumir(
            TipoToken.Imprimir,
            "Error sintáctico: se esperaba la palabra reservada IMPRIMIR.");

        Token cadena = Consumir(
            TipoToken.Cadena,
            "Error sintáctico: se esperaba una cadena después de IMPRIMIR.");

        if (programaCompleto)
        {
            Consumir(
                TipoToken.Fin,
                "Error sintáctico: se esperaba FIN para cerrar el programa.");
        }

        Consumir(
            TipoToken.FinArchivo,
            "Error sintáctico: se encontraron elementos después de la instrucción válida.");

        return new ProgramaHolaMundo(cadena.Valor);
    }

    private bool Verificar(TipoToken tipo)
    {
        return _tokens[_posicion].Tipo == tipo;
    }

    private Token Consumir(TipoToken tipo, string mensaje)
    {
        if (!Verificar(tipo))
        {
            throw new ErrorCompilacion(mensaje);
        }

        return _tokens[_posicion++];
    }
}
