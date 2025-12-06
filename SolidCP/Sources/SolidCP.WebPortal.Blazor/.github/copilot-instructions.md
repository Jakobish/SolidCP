# SolidCP WebPortal Blazor - AI Coding Agent Guide

## Project Overview

This is a **Blazor Server application** modernizing SolidCP's legacy WebForms UI to a modern, responsive web interface with Tailwind CSS and a custom component library. The project is in **Phase 2** of migration, with foundational infrastructure complete and pages being progressively migrated from WebForms.

**Key Context**: This is NOT a greenfield project—it's a staged replacement strategy for an existing enterprise system with careful rollout using feature flags.

---

## Architecture & Data Flow

### High-Level Structure

```
Blazor Pages (.razor)     → Services Layer (.cs) → DTOs/Models → Mock Data (Phase 1)
    ↓                                                              ↓
 UI Components          → NotificationService             Real SolidCP API (Phase 2+)
  (Componentsold/)
```

### Critical Components

**Routing & Layout** (`App.razor`, `Shared/MainLayout.razor`)

- App.razor: Defines router with MainLayout as default
- MainLayout: 3-column layout (sidebar, header, main content) with responsive mobile menu
- NavMenu: SolidCP-specific navigation (Servers, Domains, Hosting, Users, Settings)

**Services**

- `UserService` (IUserService): Currently mock data, will connect to real SolidCP API
- `NotificationService`: Event-driven toast system (Success/Error/Warning/Info)
- `FeatureFlags`: Configuration-driven feature control per environment

**Data Layer** (`Data/DTOs/`)

- `UserDetailsDto`: User model with enums (UserRoleType, LoginStatusType, UserStatusType)
- Pattern: Use DTOs for data transfer, separate from EF entities (not yet in this codebase)

### State Management Pattern

Services are injected as **scoped** in Program.cs:

```csharp
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<NotificationService>();
```

Pages use `@inject` to consume them:

```razor
@inject IUserService UserService
@inject NotificationService NotificationService
```

---

## Component Library & UI Patterns

### Core Components (in `Componentsold/`)

| Component | Purpose | Key Props | Example |
| --- | --- | --- | --- |
| `UiButton` | Interactions | Variant (primary/secondary/danger/ghost), IsLoading, OnClick | `<UiButton Variant="primary" OnClick="@Save">Save</UiButton>` |
| `UiCard` | Content containers | Header, ChildContent, Footer (RenderFragment) | Used on every page for visual hierarchy |
| `UiInput` | Form fields | Type, Label, Value, ValidationMessage, Disabled | Validation states from data annotations |
| `UiTable` | Data display | Items, Columns, OnPageChange, Sortable | Pagination built-in; custom column templates |
| `UiModal` | Dialogs | Title, IsOpen, OnClose, ChildContent | Focus trap on open/close |
| `UiNotification` | Toasts | Type (Success/Error/Warning/Info), Auto-hide | Injected via NotificationService |
| `ThemeToggle` | Dark mode | OnThemeChanged callback | Persists to localStorage |

### Dark Mode Convention

- All components use Tailwind's `dark:` prefix (e.g., `dark:bg-gray-900`)
- Toggled via JavaScript (`wwwroot/js/theme.js`) + CSS class on `<html>`
- Enable/disable: `Features.EnableDarkMode` in config

### Example Form Pattern (from `UserAccountDetails.razor`)

```razor
<UiInput Label="First Name" @bind-Value="user.FirstName" />
<UiInput Label="Email" Type="email" @bind-Value="user.Email" ValidationMessage="@GetFieldError(nameof(user.Email))" />
<UiButton Variant="primary" OnClick="@SaveUser" IsLoading="@isSaving">Save Changes</UiButton>
```

---

## Build & Development Workflow

### Prerequisites

```bash
Node.js 18+ (for Tailwind CSS)
.NET 9.0 SDK
```

### Build CSS (Required before running)

```bash
npm run build:css           # Watch mode during development
npm run build:css:prod      # Minified production CSS
```

### Run Application

```bash
dotnet build                # Build .NET project
dotnet run                  # Start on https://localhost:5077
```

### CSS Configuration

- **Input**: `wwwroot/css/input.css` (Tailwind directives, custom utilities)
- **Output**: `wwwroot/css/site.css` (Generated, DO NOT EDIT)
- **Config**: `tailwind.config.js` (Content paths, color palette, dark mode settings)
- **PostCSS**: `postcss.config.js` (Autoprefixer integration)

### Testing

- Unit tests in `Tests/` folder
- bUnit framework for Blazor component testing
- No complex test harness yet—write simple MSTest/xUnit tests

---

## Page Migration Pattern (WebForms → Blazor)

This is the **essential workflow** for all future page migrations:

### Step 1: Create Page Component

```razor
@page "/your/route"
@page "/your/route/{Id:int?}"
@using SolidCP.WebPortal.Blazor.Data.Services
@using SolidCP.WebPortal.Blazor.Data.DTOs

@inject IYourService YourService
@inject NotificationService Notifications
```

### Step 2: Implement Load Pattern

```razor
@code {
    private YourDto? item;
    private bool isLoading = true;
    private bool hasError = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        try
        {
            item = await YourService.GetItemAsync();
            isLoading = false;
        }
        catch (Exception ex)
        {
            hasError = true;
            await Notifications.ShowError("Failed to load data");
        }
    }
}
```

### Step 3: Build UI with Components

- Wrap content in `<UiCard>` with proper headers
- Use `<UiInput>` for form fields (NOT raw `<input>`)
- Use `<UiTable>` for lists (NOT raw `<table>`)
- Use `<UiButton>` for actions with proper variants
- Notify user actions via `NotificationService` (Success/Error)

### Step 4: Wire to Services

- For mock data: Update `UserService` with new methods
- For real API: Implement HTTP calls in service
- For validation: Use `UserDetailsDto` data annotations

**Reference Implementation**: `Pages/UserAccount/UserAccountDetails.razor` (complete working example with forms, validation, save/update flow)

---

## Project-Specific Conventions

### Namespace Structure

```
SolidCP.WebPortal.Blazor.Components       # UI components
SolidCP.WebPortal.Blazor.Data.DTOs        # Data transfer objects
SolidCP.WebPortal.Blazor.Data.Services    # Business logic
SolidCP.WebPortal.Blazor.Pages.*          # Feature-specific pages
SolidCP.WebPortal.Blazor.Services         # App-level services
```

### Feature Flags

All major features are gated by config flags in `appsettings.json`:

```json
{
  "Features": {
    "EnableBlazorUIForAll": false,
    "EnableDarkMode": true,
    "EnableAdvancedUI": false
  }
}
```

Inject `FeatureFlags` to check at runtime:

```csharp
@inject FeatureFlags Flags

@if (Flags.EnableUserAccountDetails) { ... }
```

### Responsive Design

- **Mobile-first**: Utilities apply to all screens, then breakpoints override
- **Key breakpoints**: md (768px) for sidebar hide, lg (1024px) for full layouts
- **Utility examples**: `md:flex`, `hidden md:block`, `w-full md:w-1/2`
- Test on: 375px (mobile), 768px (tablet), 1920px (desktop)

### Color & Styling

- **Primary color**: Indigo (sky blue) for key interactions
- **Secondary**: Slate for backgrounds/borders
- **Utility-first**: Use Tailwind classes, avoid custom CSS (except in `input.css`)
- **Dark mode**: Tailwind handles automatically via `dark:` prefix

---

## Integration Points & External Dependencies

### Sysinfocus Library

```csharp
@using Sysinfocus.AspNetCore.Components
@inject Initialization init
```

- Provides `Initialization` service for theme/focus management
- Used in MainLayout for `themeManager.init()` JS interop
- Call `init.InitializeTheme()` on first render for theme consistency

### Blazor Server Framework

- **Signal R Hub**: Automatic WebSocket connection for real-time updates
- **Circuit State**: Per-connection state; no cross-browser sharing
- **Rendering Mode**: Server-side only (no WebAssembly)

### JavaScript Interop

- `theme.js`: Central dark mode management
- Called via `JSRuntime.InvokeVoidAsync("themeManager.init")`
- Reads system preference and localStorage

### Mock Backend (Phase 1)

- All services return hardcoded mock data
- Transition to real HTTP calls when connecting to SolidCP API
- Pattern: Keep service interface unchanged, swap implementation

---

## Key Files Reference

| File                                         | Purpose                                           |
| -------------------------------------------- | ------------------------------------------------- |
| `Program.cs`                                 | DI container setup, middleware config             |
| `App.razor`                                  | Router configuration                              |
| `Shared/MainLayout.razor`                    | Application shell (sidebar, header, content area) |
| `Shared/NavMenu.razor`                       | Navigation menu items                             |
| `Services/FeatureFlags.cs`                   | Feature flag definitions                          |
| `Data/Services/UserService.cs`               | Example service implementation                    |
| `Data/Services/NotificationService.cs`       | Toast/alert event system                          |
| `Pages/UserAccount/UserAccountDetails.razor` | **Complete example page** (use as template)       |
| `Componentsold/UiButton.razor`               | Example component (study for component patterns)  |
| `wwwroot/css/input.css`                      | Tailwind imports + custom utilities               |
| `tailwind.config.js`                         | Theme colors, content paths, plugins              |
| `package.json`                               | Build scripts for CSS compilation                 |

---

## Common Tasks & Tips

### Adding a New Page

1. Create `.razor` file in `Pages/` subdirectory
2. Add `@page` directives with routes
3. Inject required services (`@inject IUserService UserService`)
4. Follow load pattern from UserAccountDetails.razor
5. Use component library exclusively for UI

### Adding a New Service

1. Create interface in `Data/Services/` (e.g., `IServerService.cs`)
2. Implement in same directory (e.g., `ServerService.cs`)
3. Register in Program.cs: `builder.Services.AddScoped<IServerService, ServerService>()`
4. Inject via `@inject IServerService ServerService`

### Testing a Change

- Rebuild CSS if modifying Tailwind: `npm run build:css`
- Test dark mode: Click toggle in top-right
- Test responsive: Press F12, toggle device toolbar (375px, 768px, 1920px)
- Validate forms: Use browser DevTools or NotificationService checks

### Debugging

- Browser console for JS errors
- Visual Studio debugger for C# code (breakpoints in Program.cs, services)
- Network tab for HTTP calls (when real API is integrated)

---

## Phase 2 Priority Pages

These pages are next in the migration queue (following UserAccountDetails success):

1. **SpaceDetails** - List/manage hosting spaces
2. **SpaceSettings** - Configure space properties
3. **SystemSettings** - Admin-only settings

Each should follow the UserAccountDetails pattern: Load → Display → Form → Save → Notify.

---

## Known Limitations & Notes

- **No authentication yet**: Add identity/auth when connecting to real SolidCP API
- **Mock data only**: Services hardcoded; replace with HTTP calls to backend
- **No multi-tenancy**: Current architecture assumes single user context
- **Limited testing**: Add unit/integration tests incrementally during Phase 2
- **Accessibility**: Full ARIA support in components; ensure pages use semantic HTML

---

## Where to Start

1. **Understand existing work**: Read `/AI-TASKS/PROJECT_STATUS_SUMMARY.md` (10 min)
2. **Run the app**: `dotnet run` and verify landing page loads (5 min)
3. **Explore UserAccountDetails**: Study page structure, component usage, service calls (15 min)
4. **Review component library**: Open each component in Componentsold/ and note props (10 min)
5. **Create next page**: Use template above + existing page as reference (1-2 hours)
