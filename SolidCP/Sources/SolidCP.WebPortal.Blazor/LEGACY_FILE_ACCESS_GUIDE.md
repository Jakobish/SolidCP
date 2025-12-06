# Legacy File Access Guide

**Project**: SolidCP.WebPortal Blazor Migration  
**Last Updated**: December 6, 2025

## 🚨 Important: Legacy File Locations

The legacy SolidCP.WebPortal files are **NOT** in the current workspace directory. They exist in a separate location relative to the current project.

## Legacy File Structure

```text
Legacy Project Location (not in current workspace):
├── SolidCP.WebPortal/                    # Legacy WebForms project
│   └── DesktopModules/
│       └── SolidCP/                      # Module directory
│           ├── UserAccountDetails.ascx   # Legacy files
│           ├── SpaceDetails.ascx
│           ├── SpaceSettings.ascx
│           ├── SystemSettings.ascx
│           ├── UserAccountDetails.ascx.cs
│           ├── SpaceDetails.ascx.cs
│           └── ... other legacy files
```

## How to Access Legacy Files

### Option 1: Direct File System Access (If Available)

If you have access to the legacy files in your file system:

```bash
# Navigate to legacy files location
# Example path (adjust based on your setup):
cd /path/to/SolidCP/SolidCP.WebPortal/DesktopModules/SolidCP/

# List legacy files
ls -la *.ascx *.ascx.cs
```

### Option 2: Create Mock Legacy Files

If legacy files are not accessible, create them in the current workspace for migration reference:

```bash
# Create directory for legacy file references
mkdir -p LegacyFiles/DesktopModules/SolidCP/

# Create template files for migration reference
touch LegacyFiles/DesktopModules/SolidCP/SpaceDetails.ascx
touch LegacyFiles/DesktopModules/SolidCP/SpaceDetails.ascx.cs
touch LegacyFiles/DesktopModules/SolidCP/SpaceSettings.ascx
touch LegacyFiles/DesktopModules/SolidCP/SpaceSettings.ascx.cs
touch LegacyFiles/DesktopModules/SolidCP/SystemSettings.ascx
touch LegacyFiles/DesktopModules/SolidCP/SystemSettings.ascx.cs
```

## Migration Process Without Legacy Files

If legacy files are not accessible, follow this approach:

### 1. Create Migration Templates

Create template files in the current workspace that represent typical legacy structures:

```csharp
// LegacyFiles/SpaceDetails.ascx.cs - Template structure
public partial class SpaceDetails : SolidCPModule
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Load space data
        // Populate form fields
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // Save space configuration
            // Validate inputs
            // Update database
            // Show success message
        }
        catch (Exception ex)
        {
            // Show error message
        }
    }

    // Protected fields for ASPX controls
    protected TextBox txtSpaceName;
    protected TextBox txtDescription;
    protected Button btnSave;
    protected DropDownList ddlStatus;
}
```

### 2. Analyze Common Patterns

Typical legacy patterns you should expect:

```xml
<!-- LegacyFiles/SpaceDetails.ascx - Template -->
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpaceDetails.ascx.cs" Inherits="SolidCP.SpaceDetails" %>

<div class="fieldset">
    <h3>Space Details</h3>
    <table class="form">
        <tr>
            <td class="label">Space Name:</td>
            <td><asp:TextBox ID="txtSpaceName" runat="server" /></td>
        </tr>
        <tr>
            <td class="label">Description:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" /></td>
        </tr>
        <tr>
            <td class="label">Status:</td>
            <td><asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="Active">Active</asp:ListItem>
                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</div>
```

## Converting Without Legacy Files

When you cannot access legacy files, follow this conversion pattern:

### 1. Create Expected DTO

Based on common SolidCP patterns, create DTOs that represent typical hosting panel data:

```csharp
// Data/DTOs/SpaceDetailsDto.cs
public class SpaceDetailsDto
{
    public int SpaceId { get; set; }
    
    [Required]
    [StringLength(100)]
    [Display(Name = "Space Name")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }
    
    [Required]
    [Display(Name = "Status")]
    public string Status { get; set; } = "Active";
    
    [Display(Name = "Created Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
```

### 2. Create Expected Service

```csharp
// Data/Services/ISpaceService.cs
public interface ISpaceService
{
    Task<SpaceDetailsDto?> GetSpaceDetailsAsync(int spaceId);
    Task<bool> UpdateSpaceAsync(SpaceDetailsDto space);
    Task<List<SpaceSummaryDto>> GetSpaceListAsync();
}

// Data/Services/SpaceService.cs
public class SpaceService : ISpaceService
{
    private static readonly ConcurrentDictionary<int, SpaceDetailsDto> _mockSpaces = new()
    {
        [1] = new SpaceDetailsDto { SpaceId = 1, Name = "Default Space", Status = "Active" }
    };

    public Task<SpaceDetailsDto?> GetSpaceDetailsAsync(int spaceId)
    {
        return Task.FromResult(_mockSpaces.TryGetValue(spaceId, out var space) ? space : null);
    }

    public Task<bool> UpdateSpaceAsync(SpaceDetailsDto space)
    {
        if (space == null) return Task.FromResult(false);
        
        space.CreatedDate = DateTime.UtcNow;
        _mockSpaces[space.SpaceId] = space;
        return Task.FromResult(true);
    }
}
```

### 3. Create Blazor Page

```razor
@page "/spaces/details/{SpaceId:int?}"
@using SolidCP.WebPortal.Blazor.Data.Services
@using SolidCP.WebPortal.Blazor.Data.DTOs
@using SolidCP.WebPortal.Blazor.Components
@inject ISpaceService SpaceService

<PageTitle>Space Details</PageTitle>

<UiCard Title="Space Details" Description="View and manage space information">
    <ChildContent>
        @if (space != null)
        {
            <EditForm Model="space" OnValidSubmit="SaveSpace">
                <DataAnnotationsValidator />
                
                <UiInput Label="Space Name" @bind-Value="space.Name" Required="true" />
                <UiInput Label="Description" @bind-Value="space.Description" TextArea="true" />
                
                <div class="flex justify-end space-x-3 mt-6">
                    <UiButton Variant="secondary" OnClick="NavigateBack">Cancel</UiButton>
                    <UiButton Variant="primary" Type="submit">Save Changes</UiButton>
                </div>
            </EditForm>
        }
    </ChildContent>
</UiCard>

@code {
    [Parameter] public int? SpaceId { get; set; }
    private SpaceDetailsDto? space;

    protected override async Task OnInitializedAsync()
    {
        if (SpaceId.HasValue)
        {
            space = await SpaceService.GetSpaceDetailsAsync(SpaceId.Value);
        }
    }

    private async Task SaveSpace()
    {
        if (space != null)
        {
            await SpaceService.UpdateSpaceAsync(space);
        }
    }
}
```

## Legacy File Reference List

These are the legacy files commonly referenced in migration documentation:

### High Priority (Phase 2)
- `SpaceDetails.ascx` + `SpaceDetails.ascx.cs`
- `SpaceSettings.ascx` + `SpaceSettings.ascx.cs`
- `SystemSettings.ascx` + `SystemSettings.ascx.cs`

### Medium Priority (Phase 3)
- `WebSites.ascx` + `WebSites.ascx.cs`
- `SqlDatabases.ascx` + `SqlDatabases.ascx.cs`
- `MailAccounts.ascx` + `MailAccounts.ascx.cs`
- `FileManager.ascx` + `FileManager.ascx.cs`

### Advanced (Phase 4)
- Exchange Server management files
- SharePoint management files
- VPS/Virtualization files

## Next Steps

1. **Create mock legacy files** if actual files are not accessible
2. **Follow the conversion pattern** above for each page
3. **Use common SolidCP data patterns** to create realistic DTOs
4. **Test functionality** to ensure feature parity

---

**Remember**: The goal is feature parity, not exact legacy code reproduction. Modern Blazor components provide better functionality than legacy WebForms controls.