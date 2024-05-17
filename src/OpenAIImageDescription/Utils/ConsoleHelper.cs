using Spectre.Console;

namespace OpenAIImageDescription.Utils;

internal class ConsoleHelper
{
    public static void CreateHeader()
    {
        AnsiConsole.Clear();

        Grid grid = new();

        grid.AddColumn();

        grid.AddRow(
            new FigletText("Image Description").Centered().Color(Color.Red));

        grid.AddRow(
            Align.Center(
                new Panel("[red]Sample by Thomas Sebastian Jensen ([link]https://www.tsjdev-apps.de[/])[/]")));

        AnsiConsole.Write(grid);
        AnsiConsole.WriteLine();
    }

    public static string SelectFromOptions(
        List<string> options, string prompt)
    {
        CreateHeader();

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(prompt)
            .AddChoices(options));
    }

    public static string GetUrlFromConsole(
        string prompt)
    {
        CreateHeader();

        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("white")
            .ValidationErrorMessage("[red]Invalid prompt[/]")
            .Validate(prompt =>
            {
                if (prompt.Length < 3)
                {
                    return ValidationResult.Error("[red]URL too short[/]");
                }

                if (prompt.Length > 250)
                {
                    return ValidationResult.Error("[red]URL too long[/]");
                }

                if (Uri.TryCreate(prompt, UriKind.Absolute, out Uri? uri)
                    && uri.Scheme == Uri.UriSchemeHttps)
                {
                    return ValidationResult.Success();
                }

                return ValidationResult.Error("[red]No valid URL[/]");
            }));
    }

    public static string GetStringFromConsole(
        string prompt)
    {
        CreateHeader();

        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("white")
            .ValidationErrorMessage("[red]Invalid prompt[/]")
            .Validate(prompt =>
            {
                if (prompt.Length < 3)
                {
                    return ValidationResult.Error("[red]Value too short[/]");
                }

                if (prompt.Length > 200)
                {
                    return ValidationResult.Error("[red]Value too long[/]");
                }

                return ValidationResult.Success();
            }));
    }

    public static void WriteErrorMessageToConsole(
        string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
    }

    public static void WriteMessageToConsole(
        string message)
    {
        AnsiConsole.MarkupLine($"[white]{message}[/]");
    }
}
