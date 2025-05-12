# ScoutMonitor - Cross-Platform System Resource Monitor

## Overview

**ScoutMonitor** is a .NET 8 console application that monitors system resources (CPU, RAM, Disk usage) with support for pluggable integrations like file logging and API posting. It's designed using clean architecture and supports future expansion to other platforms via interface-driven design.

---

## Features

- Real-time monitoring of:
  - CPU usage (%)
  - RAM used (MB)
  - Disk used (MB)
- Configurable interval (via `appsettings.json`)
- Plugin architecture
  - File logging plugin
  - API POST plugin (URL configurable)
- Console output of live metrics
- Clean architecture using interfaces and DI
- Config-driven and extensible design

---

## How to Build and Run

### 1. **Prerequisites**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or later (recommended)

### 2. **Steps to Run**

#### a. Clone the repository

```bash
git clone https://github.com/biradarniriksha/ScoutMonitor.git
```

#### b. Open the solution

- Open `ScoutMonitor.sln` in Visual Studio
- Set `ScoutMonitor` as the **Startup Project**

#### c. Build & Run

**In Visual Studio**: Press `F5`  
**Using CLI**:
```bash
dotnet run --project ScoutMonitor
```

---

## Configuration (`appsettings.json`)

```json
{
  "MonitoringIntervalSeconds": 5,
  "ApiPlugin": {
    "EndpointUrl": "https://webhook.site/your-unique-id"
  }
}
```

> Replace `"your-unique-id"` with a new URL from [https://webhook.site](https://webhook.site) to test API POST functionality.

---

## How to Test API Plugin using Webhook.site

1. Go to [https://webhook.site](https://webhook.site)
2. Copy your unique URL and paste it in `appsettings.json` as `EndpointUrl`
3. Run the application
4. View incoming POST requests with JSON like:

```json
{
  "cpu": 6.52,
  "ram_used": 6878.03,
  "disk_used": 162733.62
}
```

---

## Sample `metrics_log.txt` Output

The file logger plugin generates a log like this:

```
2025-05-11 19:32: CPU = 7.34% | RAM = 4282.11 MB | Disk = 76500.44 MB
2025-05-11 19:37: CPU = 4.52% | RAM = 4291.87 MB | Disk = 76500.44 MB
```

File is saved to the application output directory.

---

## Cross-Platform Architecture

Although system monitoring currently uses Windows-only APIs (`PerformanceCounter`, WMI), the architecture is **cross-platform compatible** by design.

- All system metric collection is abstracted through `ISystemMonitor`
- Windows logic resides in `WindowsSystemMonitor`
- Linux/macOS support can be added with `LinuxSystemMonitor` or `MacSystemMonitor` implementations
- Plugin logic is decoupled and reusable across platforms

This ensures platform-specific code does not leak into core logic.

---

## Design Decisions & Challenges

- Followed Clean Architecture: Core logic and interfaces are separated from infrastructure
- Used Dependency Injection for service and plugin management
- Metrics collection is abstracted via `ISystemMonitor` to support multiple platforms
- Faced challenges in RAM usage calculation — resolved with WMI for total RAM
- Used `System.Text.Json` for lightweight, fast API payload serialization

---

## Limitations / Edge Cases

- Works only on **Windows** currently
- Disk usage is based on `C:\` drive only
- Limited exception handling in plugins (can be improved)
- No UI/UX layer (out of scope)

---

## Demo Ready

- Console shows real-time metrics
- `metrics_log.txt` is auto-generated
- JSON data is posted to the configured API every interval
- Plugins can be extended independently

---

## Author

Niriksha Biradar  
[biradarniriksha](https://github.com/biradarniriksha)
