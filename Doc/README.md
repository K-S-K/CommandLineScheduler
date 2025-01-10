# CommandLineScheduler

Easy to use Scheduler for command line tasks

## How does it works

### Time signaling subsystem

The proactive part of the system is Time Controller which provides events to execute tasks.

**ITimeToExecuteTask** interface has two events:

- **HeartBeat** event to ensure that the controller is alive;

- **TimeToExecuteTask** event to notify that it is proper time to start a task.

Time Controller contains the eternal loop with checking the current time at the every iteration. It uses the **ICurrentTimeProvider** service to get the time.

We have **two implementation of the ICurrentTimeProvider** interface:

- **Working implementation** just returns a local system time;

- **Test implementation** returns an intentionally configured time for the test purposes.

## Command line developer instructions

- ```dotnet build Src --no-incremental```
