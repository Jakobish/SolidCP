# Unified Build Commands Reference

**Project**: SolidCP.WebPortal.Blazor  
**Last Updated**: December 6, 2025  
**Standard Port**: `http://localhost:5077`

## Quick Start Commands (Run These First)

### 1. Initial Setup (One Time Only)

```bash
# Navigate to project directory
cd SolidCP.WebPortal.Blazor

# Install npm dependencies
npm install

# Build CSS for production
npm run build:css

# Restore .NET packages
dotnet restore

# Build the project
dotnet build
```

### 2. Development Workflow

```bash
# Start development server
dotnet run

# Alternative: Watch CSS for changes during development
npm run build:css:watch
# (Run in separate terminal)
```

### 3. Testing

```bash
# Run unit tests
dotnet test

# Build and run (combined)
dotnet build && dotnet run
```

## Build Command Explanation

### When to Use Each Command

| Command                   | Purpose                         | When to Use                                    |
| ------------------------- | ------------------------------- | ---------------------------------------------- |
| `npm install`             | Install JavaScript dependencies | First time setup or after package.json changes |
| `npm run build:css`       | Compile Tailwind CSS            | After CSS changes for production               |
| `npm run build:css:watch` | Watch CSS files for changes     | During CSS development                         |
| `dotnet restore`          | Restore NuGet packages          | After .csproj changes                          |
| `dotnet build`            | Compile C# code                 | After code changes or before running           |
| `dotnet run`              | Start development server        | For testing and development                    |
| `dotnet test`             | Run unit tests                  | After code changes                             |

## Standard Development Session

```bash
# Start new development session
cd SolidCP.WebPortal.Blazor
npm install  # Only if needed
npm run build:css  # Only if CSS was modified
dotnet build  # Verify project compiles
dotnet run  # Start development server
```

## Troubleshooting

### Build Fails

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### CSS Not Updating

```bash
# Rebuild CSS
npm run build:css
```

### Package Issues

```bash
# Clean node modules and reinstall
rm -rf node_modules
npm install
```

## URLs

- **Development**: `http://localhost:5077`
- **User Account Details**: `http://localhost:5077/user/account/details`
- **Counter Page**: `http://localhost:5077/counter`

---

**Remember**: Always test at `http://localhost:5077` (port 5077)
