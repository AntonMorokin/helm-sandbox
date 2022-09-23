using CommandLine;
using System;
using System.Collections.Generic;

namespace CliTools.UpdateChartAppVersion
{
    internal sealed class Settings
    {
        [Option('f', "chart-dir", Required = true, HelpText = "Path to Chart directory")]
        public string ChartDirectory { get; set; }

        [Option('v', "new-version", Required = true, HelpText = "New version for the chart")]
        public string NewVersion { get; set; }

        [Option('d', "dependencies", Required = false, Separator = ',', HelpText = "Dependent charts need to be updated")]
        public IEnumerable<string> DependenciesForUpdating { get; set; } = Array.Empty<string>();
    }
}
