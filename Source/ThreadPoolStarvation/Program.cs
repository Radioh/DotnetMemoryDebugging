// Example of thread pool starvation where the number of threads used by the process increases over time while no work is being completed.
// Also notice how the CPU usage is very low

using System.Diagnostics;

Console.WriteLine($"Processor count: {Environment.ProcessorCount}");
Console.WriteLine("Starting to starve the thread pool...");

ThreadPool.SetMinThreads(8, 8);

_ = Task.Factory.StartNew(Producer, TaskCreationOptions.None);

Console.ReadLine();

return;

void Producer()
{
    while (true)
    {
        Console.WriteLine($"Threads used: {Process.GetCurrentProcess().Threads.Count}");

        _ = ProcessAsync();

        var delay = Environment.ProcessorCount > 8 ? 50 : 200;

        Thread.Sleep(delay);
    }
}

async Task ProcessAsync()
{
    // yield the task forcing it to run on the global queue instead of the local queue
    await Task.Yield();

    var completionSource = new TaskCompletionSource<bool>();

    _ = Task.Run(() =>
    {
        Thread.Sleep(1000);
        completionSource.SetResult(true);
    });

    completionSource.Task.Wait();

    Console.WriteLine("Ended - " + DateTime.Now.ToLongTimeString());
}