#Persistent Logs For Unity
Persistent Logs for Unity is short script that runs a secondary process alongside with the Unity app. This process waits until the application is no longer running (stopped either by a normal application quit or a crash). When the application stops, a timestamped copy of the Player.log file is created at the root folder of the executable, under the "Logs" folder. At this point, the process listener also terminates in a few seconds.

##How to use
### Windows
- Make sure that logListener.bat file is in the StreamingAssets folder
- Anywhere in your project, make a call to the method LogListener.Start()
- That's it. Those are the only two steps.

__If you want to disable the log listener, simply rename or remove the bat file under the streaming assets folder. If the bat file is not found, the method call will be ignored.__

### Other Platforms
Currently this only works on Windows.