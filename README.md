# Repair Status Tracker

A client-server application for tracking vehicle repair job statuses. Built with .NET 9, ASP.NET Core Minimal API, and Windows Forms.

## Architecture

- **RepairStatusTracker.Api** - REST API backend with Swagger documentation
- **RepairStatusTracker.WinForms** - Windows desktop client
- **RepairStatusTracker.Shared** - Shared models, enums, constants, and validation

## Prerequisites

- .NET 9 SDK
- Windows OS (for WinForms client)
- Visual Studio 2022 or later (recommended)

## Setup & Running

Run both projects simultaneously from Visual Studio:
1. Right-click RepairStatusTracker.Api in Solution Explorer -> Debug -> Start Without Debugging
2. Right-click RepairStatusTracker.Winforms in Solution Explorer -> Debug -> Start Without Debugging (or Start New Instance)
3. Open swagger here: https://localhost:60254/swagger/index.html or whichever port your API is running on (see ###2, below)

### 1. Clone the Repository
```bash
git clone https://github.com/rosswerner/RepairStatusTracker.git
cd RepairStatusTracker
```

### 2. Configure API Base URL (Optional)
Edit `RepairStatusTracker.WinForms\App.config` to change the API endpoint:
```xml
<add key="ApiBaseUrl" value="https://localhost:60254/" />
```

### 3. Run the API
```bash
cd RepairStatusTracker.Api
dotnet run
```
The API will start at `https://localhost:60254` (or the configured port).  
Swagger UI is available at: `https://localhost:60254/swagger`

### 4. Run the WinForms Client
Open a new terminal:
```bash
cd RepairStatusTracker.WinForms
dotnet run
```

## Features

- View all repair jobs in a grid
- Color-coded status indicators
- Update job status through dialog
- Real-time synchronization with API
- Error handling with user-friendly messages

## API Endpoints

- `GET /api/repairjobs` - Retrieve all repair jobs
- `PATCH /api/repairjobs/{id}/status` - Update job status

## Status Values

- **Received** - Job awaiting processing
- **InProgress** - Actively being worked on
- **WaitingOnParts** - On hold for parts
- **QualityCheck** - Undergoing inspection
- **ReadyForPickup** - Complete and ready
- **Completed** - Picked up by customer

## Technologies

- .NET 9
- ASP.NET Core Minimal API
- Windows Forms
- System.Text.Json
- Swashbuckle (Swagger/OpenAPI)

## Assumptions

- Data is stored in memory (mock data + changes don't persist)
- No authentication
- Not intended for multiple users at present (concurrent users, record locking, etc not considered)

## Improvements (in no particular order)

- Create/Update/Delete existing data
- Unit Tests
- Data stored in db rather than memory
- Multiple users
- Repair history/audit trail (probably needs authentication/authorization, but that balloons this out far more than is reasonable)
- Search/filtering
