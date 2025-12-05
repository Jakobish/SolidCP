# SolidCP Blazor UI Components Tests

This directory contains tests for the SolidCP modern UI components.

## Running Tests

```bash
dotnet test
```

## Test Structure

- **Unit Tests**: Component logic and behavior
- **Integration Tests**: Component interactions and data flow
- **Visual Tests**: Component rendering and styling (future)

## Component Testing

Each component has corresponding tests that verify:

- ✅ Proper rendering with different props
- ✅ State management and event handling
- ✅ Accessibility features
- ✅ Dark mode support
- ✅ Responsive behavior
- ✅ Type safety with Blazor

## Available Components

- `UiButton` - Enhanced button with variants and loading states
- `UiCard` - Container component with header, body, and footer
- `UiTable` - Data table with sorting, filtering, and pagination
- `UiModal` - Modal dialog with proper focus management
- `UiNotification` - Toast notifications with auto-hide
- `UiInput` - Form input with validation and styling
- `ThemeToggle` - Dark/light mode toggle with localStorage

## Design System

The components follow the SolidCP design system with:

- **Color Palette**: Primary blues, semantic colors (success, warning, error)
- **Typography**: Inter font family with consistent sizing
- **Spacing**: Consistent spacing scale using Tailwind utilities
- **Accessibility**: WCAG 2.1 AA compliance with proper ARIA labels
- **Dark Mode**: Full support for dark theme with `dark:` classes
