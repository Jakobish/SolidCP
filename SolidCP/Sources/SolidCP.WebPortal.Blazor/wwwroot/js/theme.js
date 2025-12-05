// Theme Manager for SolidCP Blazor Portal
window.themeManager = {
    init: function() {
        // Check for stored theme preference or default to 'light'
        let currentTheme = localStorage.getItem('theme') || 'light';
        
        // If no stored preference, check system preference
        if (currentTheme === 'light' && window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            currentTheme = 'dark';
        }
        
        this.setTheme(currentTheme);
        
        // Listen for system theme changes
        if (window.matchMedia) {
            window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
                if (!localStorage.getItem('theme')) {
                    this.setTheme(e.matches ? 'dark' : 'light');
                }
            });
        }
    },
    
    setTheme: function(theme) {
        const root = document.documentElement;
        if (theme === 'dark') {
            root.classList.add('dark');
        } else {
            root.classList.remove('dark');
        }
        localStorage.setItem('theme', theme);
    },
    
    setDark: function() {
        this.setTheme('dark');
    },
    
    setLight: function() {
        this.setTheme('light');
    },
    
    toggle: function() {
        const currentTheme = document.documentElement.classList.contains('dark') ? 'dark' : 'light';
        this.setTheme(currentTheme === 'dark' ? 'light' : 'dark');
    },
    
    getCurrentTheme: function() {
        return document.documentElement.classList.contains('dark') ? 'dark' : 'light';
    }
};

// Initialize theme on page load
document.addEventListener('DOMContentLoaded', function() {
    window.themeManager.init();
});