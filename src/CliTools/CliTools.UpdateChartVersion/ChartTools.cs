using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

namespace CliTools.UpdateChartAppVersion
{
    internal static class ChartTools
    {
        private static readonly string DocumentSeparator = "..." + Environment.NewLine;

        public static void UpdateVersion(Settings settings)
        {
            CheckSettings(settings);

            var dependencies = new HashSet<string>(settings.DependenciesForUpdating, StringComparer.OrdinalIgnoreCase);
            UpdateMainChart(settings.ChartDirectory, settings.NewVersion, dependencies);
            UpdateDependencies(dependencies, settings.ChartDirectory, settings.NewVersion);
        }

        private static void CheckSettings(Settings settings)
        {
            if (!Directory.Exists(settings.ChartDirectory))
            {
                throw new InvalidOperationException($"The directory doesn't exists: {Path.GetFullPath(settings.ChartDirectory)}");
            }

            if (!Version.TryParse(settings.NewVersion, out _))
            {
                throw new InvalidOperationException($"Passed value can't be parsed as a version: {settings.NewVersion}");
            }
        }

        private static void UpdateMainChart(string mainChartDir, string newVersion, HashSet<string> dependenciesForUpdating)
        {
            var chartFilePath = Path.Combine(mainChartDir, "Chart.yaml");
            var ys = LoadYaml(chartFilePath);

            var chart = (YamlMappingNode)ys.Documents.Single().RootNode;

            UpdateStringValue(chart.Children["version"], newVersion);

            var dependencies = (YamlSequenceNode)chart.Children["dependencies"];
            foreach (var dependency in dependencies.Children.OfType<YamlMappingNode>())
            {
                var nameNode = (YamlScalarNode)dependency.Children["name"];
                if (string.IsNullOrEmpty(nameNode.Value)
                    || !dependenciesForUpdating.Contains(nameNode.Value))
                {
                    continue;
                }

                UpdateStringValue(dependency.Children["version"], newVersion);
            }

            SaveYaml(chartFilePath, ys);
        }

        private static void UpdateDependencies(HashSet<string> dependenciesForUpdating, string chartDirectory, string newVersion)
        {
            foreach (var dependency in dependenciesForUpdating)
            {
                var chartFilePath = Path.Combine(chartDirectory, "charts", dependency, "Chart.yaml");
                var ys = LoadYaml(chartFilePath);

                var chart = (YamlMappingNode)ys.Documents.Single().RootNode;

                UpdateStringValue(chart.Children["version"], newVersion);
                UpdateStringValue(chart.Children["appVersion"], newVersion);

                SaveYaml(chartFilePath, ys);
            }
        }

        private static YamlStream LoadYaml(string chartFilePath)
        {
            var chart = File.ReadAllText(chartFilePath);

            using var sr = new StringReader(chart);
            var ys = new YamlStream();
            ys.Load(sr);

            return ys;
        }

        private static void SaveYaml(string chartFilePath, YamlStream ys)
        {
            var sw = new StringWriter();
            ys.Save(sw, false);

            // Omit redundant document separator
            var sb = sw.GetStringBuilder();
            sb.Replace(DocumentSeparator, string.Empty);
            var updatedChart = sb.ToString();

            File.WriteAllText(chartFilePath, updatedChart, Encoding.UTF8);
        }

        private static void UpdateStringValue(YamlNode node, string newValue)
        {
            var scalarNode = (YamlScalarNode)node;
            scalarNode.Value = newValue;
            scalarNode.Style = YamlDotNet.Core.ScalarStyle.DoubleQuoted;
        }
    }
}
