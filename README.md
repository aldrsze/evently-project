# Evently

Event management system with two apps sharing one MySQL database:

- **Evently.Desktop** – Windows Forms app (.NET 9) for organizers
- **Evently.Web** – ASP.NET Core website for guests and door staff (registration, QR ticket, check-in scan, ratings)

Both use the shared `Evently.Core` library.

## Requirements

Windows 64-bit (x64), plus:

| Software | Download |
|---|---|
| .NET 9 SDK | [Installer (.exe)](https://aka.ms/dotnet/9.0/dotnet-sdk-win-x64.exe) |
| MySQL Server 8.4 LTS | [Windows MSI 8.4.13](https://dev.mysql.com/downloads/file/?id=559075) |
| Git (to clone the project) | [Download page](https://git-scm.com/downloads/win) |
| Editor: VS Code or Visual Studio 2022 | [VS Code installer](https://code.visualstudio.com/sha/download?build=stable&os=win32-x64-user) |

After installing, add MySQL's `bin` folder to your PATH, then check in a new terminal:

```bash
dotnet --version
mysql --version
git --version
```

## Setup

See [setup.md](setup.md) for the database, `db.config`, and how to run both apps.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for the Git workflow.

## Never commit

- `db.config` with real credentials
- Database dumps containing real data (emails, password hashes)