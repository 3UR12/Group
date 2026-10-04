using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Grupo1.HolaMundo;

public sealed class CompiladorDotNet
{
    public ResultadoEjecucion CompilarYEjecutar(string codigoCSharp)
    {
        string carpetaProyecto = Path.Combine(
            Directory.GetCurrentDirectory(),
            "salida",
            "hola-mundo",
            "ProgramaGenerado");

        Directory.CreateDirectory(carpetaProyecto);

        string rutaCodigo = Path.Combine(carpetaProyecto, "Program.cs");
        string rutaProyecto = Path.Combine(carpetaProyecto, "ProgramaGenerado.csproj");

        File.WriteAllText(rutaCodigo, codigoCSharp, new UTF8Encoding(false));
        File.WriteAllText(rutaProyecto, CrearProyecto(), new UTF8Encoding(false));

        ResultadoProceso compilacion = EjecutarProceso(
            "dotnet",
            "build",
            rutaProyecto,
            "-c",
            "Release",
            "--nologo",
            "--verbosity",
            "quiet");

        if (compilacion.CodigoSalida != 0)
        {
            string detalle = SeleccionarDetalle(compilacion);
            throw new ErrorCompilacion(
                $"Error al compilar el código generado.{Environment.NewLine}{detalle}");
        }

        string carpetaBin = Path.Combine(
            carpetaProyecto,
            "bin",
            "Release",
            "net8.0");

        string rutaDll = Path.Combine(carpetaBin, "ProgramaGenerado.dll");
        string rutaExe = Path.Combine(carpetaBin, "ProgramaGenerado.exe");

        if (!File.Exists(rutaDll))
        {
            throw new ErrorCompilacion(
                "La compilación terminó sin generar el archivo ejecutable esperado.");
        }

        ResultadoProceso ejecucion =
            OperatingSystem.IsWindows() && File.Exists(rutaExe)
                ? EjecutarProceso(rutaExe)
                : EjecutarProceso("dotnet", rutaDll);

        if (ejecucion.CodigoSalida != 0)
        {
            string detalle = SeleccionarDetalle(ejecucion);
            throw new ErrorCompilacion(
                $"El programa generado no pudo ejecutarse.{Environment.NewLine}{detalle}");
        }

        return new ResultadoEjecucion(
            rutaCodigo,
            File.Exists(rutaExe) ? rutaExe : null,
            ejecucion.Salida.Trim());
    }

    private static string CrearProyecto()
    {
        return """
        <Project Sdk="Microsoft.NET.Sdk">
          <PropertyGroup>
            <OutputType>Exe</OutputType>
            <TargetFramework>net8.0</TargetFramework>
            <ImplicitUsings>enable</ImplicitUsings>
            <Nullable>enable</Nullable>
            <AssemblyName>ProgramaGenerado</AssemblyName>
          </PropertyGroup>
        </Project>
        """;
    }

    private static ResultadoProceso EjecutarProceso(string archivo, params string[] argumentos)
    {
        var inicio = new ProcessStartInfo
        {
            FileName = archivo,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        foreach (string argumento in argumentos)
        {
            inicio.ArgumentList.Add(argumento);
        }

        try
        {
            using var proceso = new Process { StartInfo = inicio };
            proceso.Start();

            string salida = proceso.StandardOutput.ReadToEnd();
            string error = proceso.StandardError.ReadToEnd();

            proceso.WaitForExit();

            return new ResultadoProceso(proceso.ExitCode, salida, error);
        }
        catch (Win32Exception)
        {
            throw new ErrorCompilacion(
                "No se encontró el SDK de .NET necesario para compilar el código generado.");
        }
    }

    private static string SeleccionarDetalle(ResultadoProceso resultado)
    {
        if (!string.IsNullOrWhiteSpace(resultado.Error))
        {
            return resultado.Error.Trim();
        }

        if (!string.IsNullOrWhiteSpace(resultado.Salida))
        {
            return resultado.Salida.Trim();
        }

        return "No se recibió información adicional del proceso.";
    }

    private sealed record ResultadoProceso(
        int CodigoSalida,
        string Salida,
        string Error);
}
