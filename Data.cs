using System.Text;

namespace DataFlowVsTasks;

public static class Data {
    public const string FilesDir = "files";
    public const string ResultsDir = "results";

    public static void DeleteAll() {
        Directory.Delete(FilesDir, true);
        Directory.Delete(ResultsDir, true);
    }

    public static void Generate(int fileCount, int lines, int lineLength) {
        Console.Write($"\rGenerating files ...{new string(' ', 200)}");
        var rand = new Random(1);
        if (Directory.Exists(FilesDir)) {
            Directory.Delete(FilesDir, true);
        }

        if (Directory.Exists(ResultsDir)) {
            Directory.Delete(ResultsDir, true);
        }

        Directory.CreateDirectory(FilesDir);
        Directory.CreateDirectory(ResultsDir);

        for (var i = 0; i < fileCount; i++) {
            var content = GetContent(rand, lines, lineLength);
            var path = Path.Combine(FilesDir, Path.GetRandomFileName());
            File.WriteAllText(path, content);
        }

        Console.Write($"\r{new string(' ', 200)}");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"Files count: {fileCount}, lines: {lines}, line length: {lineLength}");
        Console.WriteLine("===========================");
    }

    private static string GetContent(Random rand, int lines, int lineLength) {
        var sb = new StringBuilder();
        for (var i = 0; i < lines; i++) {
            var line = GetRandomLine(rand, lineLength);
            sb.AppendLine(line);
        }

        return sb.ToString();
    }

    private static string GetRandomLine(Random rand, int lineLength) {
        var sb = new StringBuilder();
        for (var i = 0; i < lineLength; i++) {
            var c = (char) rand.Next(32, 127);
            sb.Append(c);
        }

        return sb.ToString();
    }
}