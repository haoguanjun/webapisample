Readme


### Serilog

#### 1. add package

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Settings.Configuration
```

#### 2. configuration

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // <-- Add this line
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
}
```
设置输出到 Console

```csharp
Host.CreateDefaultBuilder(args)
                .UseSerilog( (context, options) => {
                    options.WriteTo.Console();
                }) 
```

使用配置文件

https://github.com/serilog/serilog-settings-configuration

```json
{
  "Serilog": {
    "Using":  [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
     "WriteTo": [
      { "Name": "Console" },
      { "Name": "File", "Args": { "path": "Logs/log.txt" } }
    ],
    "MinimumLevel": "Debug",
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
    "Destructure": [
      { "Name": "With", "Args": { "policy": "Sample.CustomPolicy, Sample" } },
      { "Name": "ToMaximumDepth", "Args": { "maximumDestructuringDepth": 4 } },
      { "Name": "ToMaximumStringLength", "Args": { "maximumStringLength": 100 } },
      { "Name": "ToMaximumCollectionCount", "Args": { "maximumCollectionCount": 10 } }
    ],
    "Properties": {
        "Application": "Sample"
    }
  }
}
```

Program.cs
```csharp
 Host.CreateDefaultBuilder(args)
                .UseSerilog( (context, loggerConfiguration) => {
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();
                })
```

