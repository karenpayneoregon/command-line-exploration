using Spectre.Console;

namespace DirectoryCount.Classes;


public class ExceptionHelpers
{
    /// <summary>
    /// Provides a colorful exception message using Spectre.Console NuGet package
    /// </summary>
    /// <param name="exception"><see cref="Exception"/></param>
    public static void ColorStandard(Exception exception)
    {
        AnsiConsole.WriteException(exception, new ExceptionSettings
        {
            Style = new ExceptionStyle
            {
                Exception = new Style().Foreground(Color.Grey),
                Message = new Style().Foreground(Color.White),
                NonEmphasized = new Style().Foreground(Color.Cornsilk1),
                Parenthesis = new Style().Foreground(Color.GreenYellow),
                Method = new Style().Foreground(Color.DarkOrange),
                ParameterName = new Style().Foreground(Color.Cornsilk1),
                ParameterType = new Style().Foreground(Color.Aqua),
                Path = new Style().Foreground(Color.White),
                LineNumber = new Style().Foreground(Color.Cornsilk1),
            }
        });

    }
}