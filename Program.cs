using DataFlowVsTasks;

const int iterations = 10;

////////////////////////////////////////////////////////////////////////////////
// Writing to disk
////////////////////////////////////////////////////////////////////////////////
Console.WriteLine("Executing functions which write to disk");
Console.WriteLine("----------------------------------------------------");
Data.Generate(5, 80, 50);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, true, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, true, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, true, iterations);

Data.Generate(20, 80, 50);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, true, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, true, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, true, iterations);

Data.Generate(100, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, true, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, true, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, true, iterations);

Data.Generate(200, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, true, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, true, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, true, iterations);

Data.Generate(2000, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, true, 2);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, true, 2);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, true, 2);

Data.Generate(8000, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, true, 2);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, true, 2);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, true, 2, false);

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// NOT Writing to disk
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Executing functions which don't write to disk");
Console.WriteLine("----------------------------------------------------");
Data.Generate(5, 80, 50);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, false, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, false, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, false, iterations);

Data.Generate(20, 80, 50);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, false, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, false, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, false, iterations);

Data.Generate(100, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, false, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, false, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, false, iterations);

Data.Generate(200, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, false, iterations);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, false, iterations);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, false, iterations);

Data.Generate(2000, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, false, 2);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, false, 2);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, false, 2);

Data.Generate(8000, 800, 100);
await Analyze.Function(nameof(DataFlow.PostSync), DataFlow.PostSync, false, 2);
await Analyze.Function(nameof(DataFlow.PostAsync), DataFlow.PostAsync, false, 2);
await Analyze.Function(nameof(Tasks.OnlyTasks), Tasks.OnlyTasks, false, 2, false);

Data.DeleteAll();
Console.ReadLine();