using System.Text;

namespace Grupo1.HolaMundo;

public sealed class AnalizadorLexico
{
    private readonly string _fuente;
    private int _posicion;

    public AnalizadorLexico(string fuente)
    {
        _fuente = fuente ?? string.Empty;
    }

    public IReadOnlyList<Token> Analizar()
    {
        var tokens = new List<Token>();

        while (!FinDeFuente())
        {
            char actual = VerActual();

            if (char.IsWhiteSpace(actual))
            {
                _posicion++;
                continue;
            }

            if (actual == '"')
            {
                tokens.Add(LeerCadena());
                continue;
            }

            if (char.IsLetter(actual) || actual == '_')
            {
                tokens.Add(LeerPalabraReservada());
                continue;
            }

            throw new ErrorCompilacion(
                $"Error léxico: el carácter '{actual}' no pertenece al lenguaje.");
        }

        tokens.Add(new Token(TipoToken.FinArchivo, string.Empty, string.Empty));
        return tokens;
    }

    private Token LeerPalabraReservada()
    {
        int inicio = _posicion;

        while (!FinDeFuente())
        {
            char actual = VerActual();
            if (!char.IsLetter(actual) && actual != '_')
            {
                break;
            }

            _posicion++;
        }

        string lexema = _fuente[inicio.._posicion];

        return lexema switch
        {
            "INICIO" => new Token(TipoToken.Inicio, lexema, lexema),
            "IMPRIMIR" => new Token(TipoToken.Imprimir, lexema, lexema),
            "FIN" => new Token(TipoToken.Fin, lexema, lexema),
            _ => throw new ErrorCompilacion(
                $"Error léxico: '{lexema}' no es una palabra reservada válida.")
        };
    }

    private Token LeerCadena()
    {
        int inicio = _posicion;
        _posicion++;

        var contenido = new StringBuilder();

        while (!FinDeFuente() && VerActual() != '"')
        {
            contenido.Append(VerActual());
            _posicion++;
        }

        if (FinDeFuente())
        {
            throw new ErrorCompilacion(
                "Error léxico: la cadena de texto no tiene comillas de cierre.");
        }

        _posicion++;
        string lexema = _fuente[inicio.._posicion];

        return new Token(TipoToken.Cadena, lexema, contenido.ToString());
    }

    private char VerActual() => _fuente[_posicion];

    private bool FinDeFuente() => _posicion >= _fuente.Length;
}
