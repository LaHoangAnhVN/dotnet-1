﻿using System.Diagnostics.CodeAnalysis;
using Lab1.Mode;
using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Lab1.Commands
{
    public class AddFigureCommand : Command<AddFigureCommand.AddFigureSetting>
    {
        public class AddFigureSetting : CommandSettings
        {

        }
        private IFigureRepository _figureRepository;

        public AddFigureCommand(IFigureRepository figures)
        {
            _figureRepository = figures;
        }
        public override int Execute([NotNull] CommandContext context, [NotNull] AddFigureSetting settings)
        {
            var FigureType = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Выберите тип фигуры: ").AddChoices("Прямоугольник", "Треугольник", "Круг"));
            Figure figure = FigureType switch
            {
                "Прямоугольник" => new Rectangle(new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Первая точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Первая точка Y = [/]"))),
                                                    new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Вторая точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Вторая точка Y = [/]"))),
                                                    new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Третья точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Третья точка Y = [/]"))),
                                                    new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Четвертая точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Четвертая точка Y = [/]")))),
                "Треугольник" => new Tritangle(
                                                    new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Первая точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Первая точка Y = [/]"))),
                                                    new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Вторая точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Вторая точка Y = [/]"))),
                                                    new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Третья точка X = [/]")),
                                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Третья точка Y = [/]")))),
                "Круг" => new Circle(new Point(AnsiConsole.Prompt(new TextPrompt<double>("[red]Центр X = [/]")),
                                                AnsiConsole.Prompt(new TextPrompt<double>("[red]Центр Y = [/]"))),
                                        AnsiConsole.Prompt(new TextPrompt<double>("[red]Радиус = [/]"))),
                _ => null
            };
            if (figure == null)
            {
                AnsiConsole.MarkupLine($"[red]Неизвестный тип фигуры: {FigureType}[/]");
                return -1;
            }
            else
            {
                _figureRepository.Add(figure);
            }
            return 0;
        }


    }
}
