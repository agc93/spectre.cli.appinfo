using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Spectre.Cli.AppInfo
{
    [Description("Shows basic application info.")]
    public class InfoCommand : Command<InfoCommand.Settings>
    {
        private readonly AppInfoService _infoService;

        public InfoCommand(AppInfoService infoService)
        {
            _infoService = infoService;
        }

        public override int Execute(CommandContext context, Settings settings) {
            var version = _infoService.GetAppVersion();
            var path = _infoService.GetAppPath();
            var table = new Table();
            table.AddColumn("Version");
            table.AddColumn("Runtime");
            table.AddColumn("OS");
            table.AddRow(
                version,
                _infoService.GetRuntimeName(),
                _infoService.OperatingSystemName
            );
            AnsiConsole.Render(table);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"Currently running [bold grey]{_infoService.GetExecutableName()}[/] from [bold grey]{path}[/].");
            return 0;
        }

        public class Settings : CommandSettings {
        }
    }
}