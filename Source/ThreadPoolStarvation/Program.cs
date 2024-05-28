// Example program that demonstrates Thread Pool Starvation where the number of threads used by the process increases over time while no work is being completed.
// Also notice how the CPU usage is very low
// Try to remove the Task.Yield() call and see how the thread pool is not starved anymore because the task has ran to completion on the local queue

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
        var threadCount = Process.GetCurrentProcess().Threads.Count;
        Console.WriteLine($"Threads used: {threadCount}");

        if (threadCount > 100)
        {
            Console.WriteLine("Take a memory dump now.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine("Bye!");
            break;
        }

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