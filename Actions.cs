using System.Text;

namespace DataFlowVsTasks;

public static class Actions {
    public static (string file, string[] content) ReadFile(string file) {
        return (file, File.ReadAllLines(file));
    }

    public static (string file, int count) SearchFile((string file, string[] content) fileContent) {
        var count = 0;
        foreach (var line in fileContent.content) {
            count += line.Count(c => c == 'a');
        }

        return (fileContent.file, count);
    }

    public static (string file, string content) EnrichFileCount((string file, int count) fileCount) {
        var sb = new StringBuilder();
        var line = $"{fileCount.file} has {fileCount.count} 'a' characters";
        for (var i = 0; i < 1000; i++) {
            sb.AppendLine(line);
        }

        return (fileCount.file, sb.ToString());
    }

    public static string WriteFile((string file, string content) fileCount) {
        File.WriteAllText(Path.Combine(Data.ResultsDir, Path.GetFileName(fileCount.file)), fileCount.content);
        return $"{fileCount.file}: {fileCount.content.Length}";
    }
    
    public static string DontWriteToDisk((string file, string content) fileCount) {
        return $"{fileCount.file}: {fileCount.content.Length}";
    }
}