# ScoutMonitor - Cross-Platform System Resource Monitor

## Overview

A .NET 8 console application that monitors system resources with plugin support.

## Features

- Real-time monitoring of:
  - CPU usage (%)
  - RAM used (MB)
  - Disk used (MB)
- Configurable interval (via `appsettings.json`)
- Plugin architecture
  - File logging plugin
  - API POST plugin (URL configurable)
- Clean architecture and dependency injection
- Console output support

## How to Build and Run
1. **Prerequisites**:
   .NET 8.0 SDK
   Visual Studio 2022 (recommended)
2. **Steps**:
   ```bash
   git clone https://github.com/biradarniriksha/ScoutMonitor.git
3. Open `ScoutMonitor.sln` in Visual Studio 2022
4. Set `ScoutMonitor` as the startup project
5. Make sure `appsettings.json` has the correct values
6. run:
   In VS: Press ,
   CLI: dotnet run --project ScoutMonitor
