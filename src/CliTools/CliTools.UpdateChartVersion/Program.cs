using CommandLine;
using System;

namespace CliTools.UpdateChartAppVersion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Settings>(args);

            result.WithNotParsed(e =>
            {
                var message = string.Join(Environment.NewLine, e);
                throw new InvalidOperationException(message);
            });

            result.WithParsed(s =>
            {
                ChartService.UpdateVersion(s);
            });
        }
    }
}