using System;
using System.Diagnostics;
using System.IO;

namespace CliTools.CreateNextVersion
{
    internal sealed class VersionService
    {
        public string GetNewVersion(Settings settings)
        {
            var lastTag = RunGitProcess("for-each-ref refs/tags/ --sort=\"-committerdate\" --count=1 --format=\"%(refname:short)\"", settings)
                .ReadToEnd()
                .Trim();

            Version newVersion;

            if (string.IsNullOrEmpty(lastTag))
            {
                newVersion = new Version(1, 0, 0);
            }
            else
            {
                if (Version.TryParse(lastTag, out var lastVersion))
                {
                    static int Update(bool needToIncrement, int currentValue)
                    {
                        return needToIncrement ? ++currentValue : currentValue;
                    }

                    var newMajor = Update(settings.IncrementMajor, lastVersion.Major);
                    var newMinor = Update(settings.IncrementMinor, lastVersion.Minor);
                    var newBuild = Update(settings.IncrementBuild, lastVersion.Build);

                    newVersion = new Version(newMajor, newMinor, newBuild);
                }
                else
                {
                    throw new InvalidOperationException($"Unknown format of version tag '{lastTag}'");
                }
            }

            if (settings.NeedSaveNewVersionInEnvVariable)
            {
                Environment.SetEnvironmentVariable(settings.NewVersionEnvVariable, newVersion.ToString(3), EnvironmentVariableTarget.User);
            }

            return newVersion.ToString(3);
        }

        private static StreamReader RunGitProcess(string arguments, Settings settings)
        {
            var startInfo = CreateGitProcessStartInfo(arguments, settings);

            var forEachRef = Process.Start(startInfo)
                ?? throw new InvalidOperationException($"Unable to start git process with arguments '{arguments}'.");

            forEachRef.WaitForExit();

            if (forEachRef.ExitCode != 0)
            {
                var errorMessage = forEachRef.StandardError.ReadToEnd();
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = forEachRef.StandardOutput.ReadToEnd();
                }

                throw new InvalidOperationException(errorMessage);
            }

            return forEachRef.StandardOutput;
        }

        private static ProcessStartInfo CreateGitProcessStartInfo(string arguments, Settings settings)
        {
            return new ProcessStartInfo("git", arguments)
            {
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = settings.WorkingDirectory
            };
        }
    }
}
