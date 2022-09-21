﻿using CommandLine;

namespace CliTools.CreateNextVersion
{
    internal sealed class Settings
    {
        public const string NewVersionEnvironmentVariableName = "CMS_NEW_VERSION";

        [Option('d', "repo-dir", Required = true, HelpText = "Set directory with your repository")]
        public string WorkingDirectory { get; set; }

        [Option('j', "major", Required = false, Default = false, HelpText = "Should major version be incremented or not")]
        public bool IncrementMajor { get; set; }

        [Option('i', "minor", Required = false, Default = false, HelpText = "Should minor version be incremented or not")]
        public bool IncrementMinor { get; set; }

        [Option('b', "build", Required = false, Default = true, HelpText = "Should build version be incremented or not")]
        public bool IncrementBuild { get; set; }

        [Option('s', "save", Required = false, Default = false, HelpText = "Should new version be saved in environment variable")]
        public bool NeedSaveNewVersionInEnvVariable { get; set; }

        [Option('e', "env-var", Required = false, Default = NewVersionEnvironmentVariableName, HelpText = "Name of environment variable to save new version value")]
        public string NewVersionEnvVariable { get; set; }

    }
}