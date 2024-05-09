# Advanced .NET Memory Debugging

## Description
This project is a simple example of how to debug memory leaks in a .NET application. 

## Example: Memory Leak
Demonstrates a scenario where a list of objects is continuously populated without being cleared, 
leading to a potential memory leak because the objects are not being garbage collected.

## Example: Thread pool starvation
Demonstrates a scenario where the threadpool continues to grow due to a long running being moved to the global queue and continuously taking in work on local queues.

## Example: Blocked finalizer queue (TBD)
Demonstrates a scenario where the finalizer queue is blocked due to a long running finalizer.

## Getting Started
To run the examples, simply clone the repository and run the projects.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
