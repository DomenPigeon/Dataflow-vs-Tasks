using System.Diagnostics;

namespace DataFlowVsTasks;

public static class Analyze {
    public static async Task Function(string name, Action<int, bool> func, bool writeToDisk, int iterations, bool wait = true) {
        var maxDegreeOfParallelism = new[] {1, 2, Environment.ProcessorCount, -1};
        foreach (var degree in maxDegreeOfParallelism) {
            var sw = Stopwatch.StartNew();

            for (var i = 0; i < iterations; i++) {
                func(degree, writeToDisk);
            }

            sw.Stop();
            Console.Write($"\rParallel ({degree,-2}) {name + ":",-15} {sw.ElapsedMilliseconds + "ms",-8} average: {sw.ElapsedMilliseconds / iterations + "ms",-4} ({sw.ElapsedTicks / iterations})");
            Console.WriteLine();
        }

        Console.WriteLine();
        if (wait) {
            await Wait(1000);
        }
    }

    public static async Task Function(string name, Action<bool> func, bool writeToDisk, int iterations, bool wait = true) {
        var sw = Stopwatch.StartNew();

        for (var i = 0; i < iterations; i++) {
            func(writeToDisk);
        }

        sw.Stop();

        Console.WriteLine($"\rParallel ({0,-2}) {name + ":",-15} {sw.ElapsedMilliseconds + "ms",-8} average: {sw.ElapsedMilliseconds / iterations + "ms",-4} ({sw.ElapsedTicks / iterations})");
        Console.WriteLine();
        Console.WriteLine();
        if (wait) {
            await Wait(1000);
        }
    }

    private static async Task Wait(int delay) {
        Console.Write($"Waiting {delay}ms before continuing with next task");
        await Task.Delay(delay);
    }
}