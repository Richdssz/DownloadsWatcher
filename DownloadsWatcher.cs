using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

class DownloadsWatcher
{
    private static readonly string DownloadsPath = @"C:\Users\richd\Downloads";
    private static readonly string LogPath = Path.Combine(DownloadsPath, "_Historico_Organizacao.log");

    private static readonly Dictionary<string, string[]> Categories = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
    {
        { "Instaladores", new[] { ".exe", ".msi", ".bat", ".cmd", ".ps1" } },
        { "Documentos",   new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".csv", ".ppt", ".pptx", ".txt", ".md", ".epub" } },
        { "Compactados",  new[] { ".zip", ".rar", ".7z", ".tar", ".gz", ".iso" } },
        { "Imagens",      new[] { ".png", ".jpg", ".jpeg", ".gif", ".webp", ".svg", ".bmp", ".ico", ".psd" } },
        { "Videos",       new[] { ".mp4", ".mkv", ".avi", ".mov", ".wmv", ".webm" } },
        { "Audios",       new[] { ".mp3", ".wav", ".flac", ".ogg", ".m4a", ".aac" } },
        { "Web_Codigo",   new[] { ".html", ".htm", ".json", ".xml", ".js", ".css", ".ts", ".py", ".rs" } }
    };

    private static readonly HashSet<string> IgnoredExts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ".crdownload", ".tmp", ".part", ".downloading"
    };

    static void Log(string message)
    {
        try
        {
            string line = string.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}", DateTime.Now, message);
            File.AppendAllText(LogPath, line + Environment.NewLine);
        }
        catch { }
    }

    static string GetCategory(string ext)
    {
        foreach (var entry in Categories)
        {
            foreach (var item in entry.Value)
            {
                if (item.Equals(ext, StringComparison.OrdinalIgnoreCase))
                    return entry.Key;
            }
        }
        return "Outros";
    }

    static bool IsFileReady(string filename)
    {
        try
        {
            using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                return inputStream.Length >= 0;
            }
        }
        catch
        {
            return false;
        }
    }

    static void ProcessFiles()
    {
        try
        {
            string[] files = Directory.GetFiles(DownloadsPath);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                if (name.Equals("_Historico_Organizacao.log", StringComparison.OrdinalIgnoreCase))
                    continue;

                string ext = Path.GetExtension(file);
                if (IgnoredExts.Contains(ext))
                    continue;

                if (!IsFileReady(file))
                    continue;

                string cat = GetCategory(ext);
                string targetDir = Path.Combine(DownloadsPath, cat);
                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);

                string destPath = Path.Combine(targetDir, name);
                if (File.Exists(destPath))
                {
                    string nameNoExt = Path.GetFileNameWithoutExtension(name);
                    destPath = Path.Combine(targetDir, string.Format("{0}_{1:yyyyMMdd_HHmmss}{2}", nameNoExt, DateTime.Now, ext));
                }

                File.Move(file, destPath);
                Log(string.Format("Movido: '{0}' -> [{1}]", name, cat));
            }
        }
        catch (Exception ex)
        {
            Log("Erro em ProcessFiles: " + ex.Message);
        }
    }

    static void Main()
    {
        try
        {
            while (true)
            {
                ProcessFiles();
                Thread.Sleep(2000);
            }
        }
        catch (Exception ex)
        {
            Log("Erro fatal em Main: " + ex.Message);
        }
    }
}
