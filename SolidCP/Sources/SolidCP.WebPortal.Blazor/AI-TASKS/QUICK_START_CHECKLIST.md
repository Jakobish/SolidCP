# Quick Start Checklist for AI Agents

## 🚀 15-Minute Setup & Validation

### Phase 1: Environment Setup (5 minutes)

```bash
# 1. Navigate to project
cd SolidCP.WebPortal.Blazor

# 2. Build project
dotnet build

# 3. Start application
dotnet run
```

**Expected Result**: Application starts successfully on `http://localhost:5077`

### Phase 2: Verify Current Implementation (5 minutes)

```bash
# Open browser and navigate to:
# http://localhost:5077                (Main dashboard)
# http://localhost:5077/user/account/details  (User account page)
# http://localhost:5077/user/details/1  (User details with ID)

# Test dark mode: Click theme toggle in top-right
# Test responsive: Resize browser window
# Test navigation: Click between pages
```

**Expected Result**: All pages load, dark mode works, responsive design functions

### Phase 3: Review Reference Implementation (5 minutes)

```bash
# Review these key files:
# 1. Pages/UserAccount/UserAccountDetails.razor
# 2. Data/DTOs/UserDetailsDto.cs  
# 3. Data/Services/UserService.cs
# 4. Components/ directory (8 components)
```

**Expected Result**: Understand the migration pattern

## ✅ Success Criteria - All Must Pass

- [ ] Project builds without errors (`dotnet build`)
- [ ] Application starts (`dotnet run`)
- [ ] Dark mode toggle works (top-right corner)
- [ ] User Account Details page loads and functions
- [ ] Responsive design works (resize browser)
- [ ] Can navigate between pages
- [ ] Reference implementation understood

## 🚨 If Any Check Fails

1. **Build fails**: Check `dotnet clean && dotnet restore && dotnet build`
2. **Application won't start**: Verify .NET 7.0 SDK is installed
3. **Pages don't load**: Check browser console for errors
4. **Dark mode doesn't work**: Verify CSS compilation
5. **Can't navigate**: Check routing in `App.razor`

---

## 🎯 Next Task for AI Agents

**IMMEDIATE**: Migrate SpaceDetails page following the exact pattern from UserAccountDetails

1. Read legacy `SolidCP.WebPortal/DesktopModules/SolidCP/SpaceDetails.ascx`
2. Create `SpaceDetailsDto` in `Data/DTOs/`
3. Create `SpaceDetails.razor` in `Pages/Spaces/`
4. Test functionality
5. Build and verify success

**Time Estimate**: 2-3 hours
**Reference Pattern**: `UserAccountDetails.razor`
