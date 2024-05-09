# Advanced .NET Memory Debugging

## Description
This project contains examples of common memory issues in .NET applications to be used when training debugging.

## Example: [Memory Leak](Source/MemoryLeak/Program.cs)
Demonstrates a scenario where a list of objects is continuously populated without being cleared, 
leading to a potential memory leak because the objects are not being garbage collected.

## Example: [Threadpool starvation](Source/ThreadPoolStarvation/Program.cs)
Demonstrates a scenario where the threadpool continues to grow due to a long running being moved to the global queue and continuously taking in work on local queues.

## Example: Blocked finalizer queue (TBD)
Demonstrates a scenario where the finalizer queue is blocked due to a long running finalizer.

## Getting Started with the Examples
To run the examples, simply clone the repository and run the projects.

## How to debug the applications using dotnet-dump
1. Install the dotnet-dump tool by running the following command:
```bash
dotnet tool install -g dotnet-dump
```
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


## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
