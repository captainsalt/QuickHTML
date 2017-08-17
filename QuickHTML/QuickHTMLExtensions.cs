using System.Diagnostics;

namespace QuickHTML.Extensions
{
    internal static class QuickHTMLExtensions
    {
        internal static void OpenVSCode(this QuickHTMLService service)
        {
            var cmd = new ProcessStartInfo()
            {
                WorkingDirectory = service.ProjectDirectoryRoot,
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = @"/C code .",
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(cmd);
        }
    }
}
