namespace Grupo1.HolaMundo;

public enum TipoToken
{
    Inicio,
    Imprimir,
    Fin,
    Cadena,
    FinArchivo
}

public sealed record Token(TipoToken Tipo, string Lexema, string Valor);
