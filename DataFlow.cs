using System.Threading.Tasks.Dataflow;

namespace DataFlowVsTasks;

public static class DataFlow {
    public static void PostSync(int parallelDegree, bool writeToDisk) {
        // Options
        var linkOptions = new DataflowLinkOptions {PropagateCompletion = true};
        var execOptions = new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = parallelDegree};

        // Blocks
        var readFiles = new TransformBlock<string, (string, string[])>(Actions.ReadFile, execOptions);
        var searchFiles = new TransformBlock<(string, string[]), (string, int)>(Actions.SearchFile, execOptions);
        var enrich = new TransformBlock<(string, int), (string, string)>(Actions.EnrichFileCount, execOptions);
        var writeResults = new TransformBlock<(string, string), string>(writeToDisk ? Actions.WriteFile : Actions.DontWriteToDisk, execOptions);

        // Pipeline
        readFiles.LinkTo(searchFiles, linkOptions);
        searchFiles.LinkTo(enrich, linkOptions);
        enrich.LinkTo(writeResults, linkOptions);
        writeResults.LinkTo(DataflowBlock.NullTarget<string>(), linkOptions);

        var files = Directory.EnumerateFiles(Data.FilesDir, "*").ToArray();
        foreach (var file in files) {
            readFiles.Post(file);
        }

        readFiles.Complete();
        writeResults.Completion.Wait();

        var results = new List<string>();
        while (writeResults.TryReceive(out var result)) {
            results.Add(result);
        }
        // Do something with the results ...
    }

    public static void PostAsync(int parallelDegree, bool writeToDisk) {
        // Options
        var linkOptions = new DataflowLinkOptions {PropagateCompletion = true};
        var execOptions = new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = parallelDegree};

        // Blocks
        var readFiles = new TransformBlock<string, (string, string[])>(Actions.ReadFile, execOptions);
        var searchFiles = new TransformBlock<(string, string[]), (string, int)>(Actions.SearchFile, execOptions);
        var enrich = new TransformBlock<(string, int), (string, string)>(Actions.EnrichFileCount, execOptions);
        var writeResults = new TransformBlock<(string, string), string>(writeToDisk ? Actions.WriteFile : Actions.DontWriteToDisk, execOptions);

        // Pipeline
        readFiles.LinkTo(searchFiles, linkOptions);
        searchFiles.LinkTo(enrich, linkOptions);
        enrich.LinkTo(writeResults, linkOptions);
        writeResults.LinkTo(DataflowBlock.NullTarget<string>(), linkOptions);

        // Input
        var files = Directory.EnumerateFiles(Data.FilesDir, "*").ToArray();
        var tasks = new Task[files.Length + 1];
        for (var i = 0; i < files.Length; i++) {
            var file = files[i];
            tasks[i] = readFiles.SendAsync(file);
        }

        readFiles.Complete();
        tasks[files.Length] = writeResults.Completion;
        Task.WaitAll(tasks);

        var results = new List<string>();
        while (writeResults.TryReceive(out var result)) {
            results.Add(result);
        }
        // Do something with the results ...
    }
}