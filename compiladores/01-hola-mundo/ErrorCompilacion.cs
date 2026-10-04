namespace Grupo1.HolaMundo;

public sealed class ErrorCompilacion : Exception
{
    public ErrorCompilacion(string mensaje) : base(mensaje)
    {
    }
}
