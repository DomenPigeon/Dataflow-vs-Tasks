namespace DataFlowVsTasks;

public static class Tasks {
    public static void OnlyTasks(bool writeToDisk) {
        var files = Directory.EnumerateFiles(Data.FilesDir, "*").ToArray();
        var tasks = new Task[files.Length];
        for (var i = 0; i < files.Length; i++) {
            var file = files[i];
            var task = Task.Run(() => ProcessFile(file, writeToDisk));
            tasks[i] = task;
        }

        Task.WaitAll(tasks);
    }

    private static void ProcessFile(string file, bool writeToDisk) {
        var read = Actions.ReadFile(file);
        var search = Actions.SearchFile(read);
        var enrich = Actions.EnrichFileCount(search);
        if (writeToDisk) {
            Actions.WriteFile(enrich);
        }
        else {
            Actions.DontWriteToDisk(enrich);
        }
    }
}