using CommandLine;
using System;

namespace CliTools.CreateNextVersion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var argsParsingResult = Parser.Default.ParseArguments<Settings>(args);

            argsParsingResult.WithNotParsed(e =>
            {
                var message = string.Join(Environment.NewLine, argsParsingResult.Errors);
                throw new ArgumentException(message);
            });

            argsParsingResult.WithParsed(s =>
            {
                var newVersion = new VersionService().GetNewVersion(s);
                Console.WriteLine($"New version will be '{newVersion}'");
            });
        }
    }
}