# Component Reference - Standardized Properties

**Project**: SolidCP.WebPortal.Blazor  
**Last Updated**: December 6, 2025  
**Location**: `Componentsold/` directory

## 🔧 Component Property Standards

This document standardizes all component property names to ensure consistency across documentation and prevent compilation errors.

---

## UiCard Component

**File**: `Componentsold/UiCard.razor`  
**Usage**: Primary container component for pages and sections

### Correct Properties (Use These)

```razor
<UiCard 
    Title="Page Title"
    Description="Optional description text"
    Footer="@SomeFooterContent">
    <ChildContent>
        <!-- Your content here -->
    </ChildContent>
</UiCard>
```

### Property Reference

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Title` | `string?` | No | Card header title |
| `Description` | `RenderFragment?` | No | Rich text description (RenderFragment) |
| `DescriptionText` | `string?` | No | Simple string description |
| `ChildContent` | `RenderFragment?` | Yes | Main card content |
| `Footer` | `RenderFragment?` | No | Card footer content |
| `Header` | `RenderFragment?` | No | Custom header (advanced) |
| `Class` | `string` | No | Additional CSS classes |

### Common Usage Patterns

```razor
<!-- Basic usage -->
<UiCard Title="User Details">
    <ChildContent>
        <!-- Content -->
    </ChildContent>
</UiCard>

<!-- With description -->
<UiCard Title="Settings" Description="Configure your preferences">
    <ChildContent>
        <!-- Content -->
    </ChildContent>
</UiCard>

<!-- With footer -->
<UiCard Title="Form">
    <ChildContent>
        <!-- Form content -->
    </ChildContent>
    <Footer>
        <UiButton Variant="primary" Type="submit">Save</UiButton>
    </Footer>
</UiCard>
```

---

## UiButton Component

**File**: `Componentsold/UiButton.razor`  
**Usage**: Interactive button element

### Correct Properties (Use These)

```razor
<UiButton 
    Variant="primary"
    Type="submit"
    OnClick="HandleClick"
    IsLoading="@isLoading"
    Disabled="@isDisabled">
    Button Text
</UiButton>
```

### Property Reference

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Variant` | `string` | No | Button style: `primary`, `secondary`, `danger`, `ghost` |
| `Type` | `string` | No | HTML button type: `button`, `submit`, `reset` |
| `OnClick` | `EventCallback<MouseEventArgs>` | No | Click event handler |
| `Disabled` | `bool` | No | Disable button interaction |
| `IsLoading` | `bool` | No | Show loading spinner |
| `ChildContent` | `RenderFragment?` | Yes | Button content/text |
| `Class` | `string` | No | Additional CSS classes |

### Common Usage Patterns

```razor
<!-- Standard button -->
<UiButton Variant="primary" OnClick="SaveData">Save Changes</UiButton>

<!-- Submit button -->
<UiButton Variant="primary" Type="submit" IsLoading="@isSaving">
    @if (isSaving) { <span>Saving...</span> }
    else { <span>Save Changes</span> }
</UiButton>

<!-- Secondary button -->
<UiButton Variant="secondary" OnClick="NavigateBack">Cancel</UiButton>

<!-- Danger button -->
<UiButton Variant="danger" OnClick="DeleteItem">Delete</UiButton>

<!-- Disabled button -->
<UiButton Variant="primary" Disabled="@isDisabled">Cannot Click</UiButton>
```

---

## UiInput Component

**File**: `Componentsold/UiInput.razor`  
**Usage**: Form input fields

### Property Reference

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Label` | `string?` | No | Input label text |
| `Value` | `string?` | No | Input value (for one-way binding) |
| `Placeholder` | `string?` | No | Placeholder text |
| `ReadOnly` | `bool` | No | Make input read-only |
| `Required` | `bool` | No | Mark as required field |
| `TextArea` | `bool` | No | Use textarea instead of input |
| `HelpText` | `string?` | No | Helper text below input |
| `Class` | `string` | No | Additional CSS classes |

### Common Usage Patterns

```razor
<!-- Basic input -->
<UiInput Label="Email" @bind-Value="user.Email" Required="true" />

<!-- Textarea -->
<UiInput Label="Description" @bind-Value="user.Description" TextArea="true" />

<!-- Read-only -->
<UiInput Label="ID" Value="@user.Id.ToString()" ReadOnly="true" />

<!-- With help text -->
<UiInput Label="Password" @bind-Value="user.Password" Required="true" HelpText="Minimum 8 characters" />
```

---

## UiTable Component

**File**: `Componentsold/UiTable.razor`  
**Usage**: Data tables with pagination and sorting

### Property Reference

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Items` | `IEnumerable<T>` | Yes | Data source |
| `Columns` | `RenderFragment` | Yes | Column definitions |
| `PageSize` | `int` | No | Items per page (default: 10) |
| `Class` | `string` | No | Additional CSS classes |

### Common Usage Patterns 2

```razor
<UiTable Items="@users" PageSize="5">
    <Columns>
        <TableColumn Title="Name" SortBy="@(u => u.Name)" />
        <TableColumn Title="Email" SortBy="@(u => u.Email)" />
        <TableColumn Title="Actions">
            <Template Context="user">
                <UiButton Variant="ghost" OnClick="() => Edit(user)">Edit</UiButton>
            </Template>
        </TableColumn>
    </Columns>
</UiTable>
```

---

## UiModal Component

**File**: `Componentsold/UiModal.razor`  
**Usage**: Modal dialog boxes

### Property Reference

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Show` | `bool` | Yes | Show/hide modal |
| `Title` | `string?` | No | Modal title |
| `OnClose` | `EventCallback` | No | Close event handler |
| `Size` | `string` | No | Modal size: `sm`, `md`, `lg`, `xl` |
| `Class` | `string` | No | Additional CSS classes |

### Common Usage Patterns

```razor
<UiModal Show="@showModal" Title="Confirm Action" OnClose="CloseModal">
    <div class="p-4">
        <p>Are you sure you want to delete this item?</p>
        <div class="flex justify-end space-x-3 mt-4">
            <UiButton Variant="secondary" OnClick="CloseModal">Cancel</UiButton>
            <UiButton Variant="danger" OnClick="ConfirmDelete">Delete</UiButton>
        </div>
    </div>
</UiModal>
```

---

## UiNotification Component

**File**: `Componentsold/UiNotification.razor`  
**Usage**: Toast notifications (used via NotificationService)

### Usage (via NotificationService)

```csharp
// Inject notification service
@inject NotificationService NotificationService

// Show different types
await NotificationService.ShowSuccess("Operation completed!");
await NotificationService.ShowError("Something went wrong");
await NotificationService.ShowWarning("Be careful");
await NotificationService.ShowInfo("Information message");
```

---

## ThemeToggle Component

**File**: `Componentsold/ThemeToggle.razor`  
**Usage**: Dark/light mode toggle

### Property Reference

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Class` | `string` | No | Additional CSS classes |

### Common Usage Patterns

```razor
<!-- In header/navbar -->
<ThemeToggle />
```

---

## Standardized Usage Rules

### 1. Always Use Correct Property Names

- Use `ChildContent` for UiCard and UiButton
- Use `Variant` for button styles
- Use `OnClick` for click events

### 2. Never Mix Property Names

❌ **Incorrect**:

```razor
<UiCard Content="Some content">  <!-- Wrong property name -->
```

✅ **Correct**:

```razor
<UiCard>
    <ChildContent>
        Some content
    </ChildContent>
</UiCard>
```

### 3. Follow Import Patterns

```razor
@using SolidCP.WebPortal.Blazor.Components
@inject NotificationService NotificationService
```

### 4. Use Consistent Event Patterns

```razor
<UiButton OnClick="HandleSubmit">Submit</UiButton>

@code {
    private async Task HandleSubmit()
    {
        // Handle button click
    }
}
```

---

## Migration Checklist

When migrating legacy pages, ensure all component usage follows this standard:

- [ ] UiCard uses `ChildContent` property
- [ ] UiButton uses `Variant` for styles
- [ ] All event handlers use proper Blazor syntax
- [ ] Import statements are correct
- [ ] Component names match exactly

---

**Remember**: Use these exact property names to avoid compilation errors. The component files in `Componentsold/` directory define the authoritative property names.
