namespace Grupo1.HolaMundo;

public sealed record ResultadoCompilacion(
    IReadOnlyList<Token> Tokens,
    ProgramaHolaMundo Programa,
    string CodigoCSharp);
