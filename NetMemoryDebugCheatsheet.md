# .NET memory debugging cheat sheet
This cheat sheet is designed to help you identify the root cause of memory issues in .NET applications using the dotnet-dump tool.
It can be used with the examples in the [DotnetMemoryDebugging](https://github.com/Radioh/DotnetMemoryDebugging) repository to learn how to identify memory issues in .NET applications.

## Prerequisites
- Install the dotnet-dump tool: https://learn.microsoft.com/en-us/dotnet/core/diagnostics/dotnet-dump

## Quick start guide
| Command                                 | Description       |
|-----------------------------------------|-------------------|
| dotnet-dump analyze {your_dmp_file}.dmp | Open the DMP file |
| eeversion                               |List the version of the runtime|
| loadsymbols                             |Load the symbols for the dump|
| verifyheap                              |Verify the heap integrity|
| gcheapstat                              |Display the statistics for the GC generations|
| dumpheap -stat                          |Display the statistics for the heap. Look for the largest objects which will be shown at the bottom of the list. Look for objects that are recognizable from the code.|
| dumpheap -mt {address}                  |Dump the heap on a specific address. Use the address from the dumpheap -stat command.|
| do {address}                            |Display the object at the address. Use the address from the dumpheap -mt command. Note if a thread is holding the object. Is useful when you want to inspect the CLR stack of the thread.|
| gcroot {address}                        |Display the root of the object which is holding the object.|
| clrthreads                              |Display the CLR threads. If you see a lot of threads, it might be a sign of thread pool starvation.|
| setthread {thread_id}                   |Set the thread to be the thread specified. Use the thread id from the clrthreads command.|
| clrstack                                |Display the CLR stack of the current thread.|
| clrstack -all                           |Display the CLR stack of all threads. Make sure to check the finalizer thread is not blocked.|
| dumpexceptions                          |Displays all exceptions on the heap.|

## Note
There are many more commands available in the dotnet-dump tool. You can find the full list of commands by running the help command.
This quick start is designed to help you get started with the most common commands to identify memory issues in .NET applications.
If you don't find the issue after you have followed the commands, then you should have a pretty good idea what state the system is in and what symptoms the issue is causing.