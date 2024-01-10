
using System.Diagnostics;

namespace Poc.Sqlite.Api.BackgroundServices
{
	public class SyncDbFileService : BackgroundService
	{
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			ExecuteCommand("powershel", $"-File syncdb.ps1");

			return Task.CompletedTask;

		}

		private static void ExecuteCommand(string commandName, string command)
		{
			try
			{
				using Process process = new();

				process.StartInfo.FileName = commandName; // Use "powershell" for Windows PowerShell
				process.StartInfo.Arguments = command; //$"-File syncdb.ps1";
		
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;

				process.Start();

				// You can read the output if needed
				string output = process.StandardOutput.ReadToEnd();

				process.WaitForExit();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error sync DB with GIT - {ex}");
			}
		}
	}
}
