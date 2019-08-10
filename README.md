# SliderOnlyCheck
A Mapset Verifier plugin to check slider only sections on Easy and Normal diffs, written in .NET Core  
...and to give a quick overview on how to create a plugin.

## Building
These steps assume you already have [.NET Core SDK](https://dotnet.microsoft.com) installed.  
Oh right these build steps are for linux if you're using windows just substitute `mkdir` to `md`
```bash
$ mkdir MV-env
$ cd MV-env

# Clone every dependencies
$ git clone https://github.com/Naxesss/MapsetParser.git
$ git clone https://github.com/Naxesss/MapsetVerifierFramework.git

# Register dotnet app to our dependency because Naxess doesnt seem to register it lol
# and their dependency too
$ cd MapsetParser && dotnet new classlib && dotnet add package System.Numerics.Vectors
$ cd ../MapsetVerifierFramework && dotnet new classlib
$ dotnet add package ManagedBass && dotnet add package NAudio && dotnet add package TagLib.Portable

# Clone our thing
$ cd ../ & git clone https://github.com/rorre/MV-SliderOnly.git
$ cd MV-SliderOnly/SliderOnlyChecker
$ dotnet build
```

## Installing to MV
1. Go to latest [release](https://github.com/rorre/MV-SliderOnly/releases/latest)
2. Download the `.dll` file
3. Move the `.dll` file to `%APPDATA%\Mapset Verifier Externals\checks`
   
