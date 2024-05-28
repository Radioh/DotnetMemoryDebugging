# Advanced .NET Memory Debugging

## Description
This project contains examples of code which will force the program into a state of common memory issues in .NET applications to be used when training debugging.
The examples are designed to be used with the dotnet-dump tool to analyze memory dumps and identify the root cause of the memory issues.

## Example: [Memory Leak](Source/MemoryLeak/Program.cs)
Demonstrates a scenario where a list of objects is continuously populated without being cleared, 
leading to a memory leak because the objects are not being garbage collected.
Use this simple example to learn how to identify how a large amount of objects look like by dumping statistics for the heap and inspecting objects.

## Example: [Threadpool starvation](Source/ThreadPoolStarvation/Program.cs)
Demonstrates a scenario where the thread pool continues to grow due to a long running operation being moved to the global queue and continuously taking in work on local queues.
Use this example to learn how to identify thread pool starvation by inspecting the CLR threads.

## Example: [Blocked finalizer queue](Source/BlockedFinalizerQueue/Program.cs)
Demonstrates a scenario where the finalizer queue is blocked due to a long running finalizer causing a memory leak.
Use this example to identify a blocked finalizer queue by inspecting objects on the heap, their parents and finalizer queue CLR stack.

## Example: [Entity Framework cartesian explosion](Source/CartesianExplosion/Program.cs)
Demonstrates a scenario where a simple EF query on a small amount of data is executed that returns a large number of rows, causing the application to use a lot of memory.
Use this example to learn how to identify a cartesian explosion by inspecting the EF query and the objects in memory.

## Getting Started with the Examples
To run the examples, simply clone the repository and run the projects. The examples are designed to be simply run inside a console application.
They will pause at a good point to take a memory dump and analyze.

## How to debug the applications using dotnet-dump

1. Install the dotnet-dump tool by running the following command:
```bash
dotnet tool install -g dotnet-dump
```
You can also visit the documentation from [https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-dump](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-dump)
2. Run the application and get the process id by running the following command:
```bash
dotnet-dump ps
```
3. Take a memory dump of the process by running the following command:
```bash
dotnet-dump collect -p <process id>
```
4. Analyze the memory dump using the following command:
```bash
dotnet-dump analyze <dump file>
```

## .NET memory debugging cheat sheet
Run the examples and use the commands in the [NetMemoryDebugCheatsheet.md](NetMemoryDebugCheatsheet.md) to identify the root cause of the memory issue.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
