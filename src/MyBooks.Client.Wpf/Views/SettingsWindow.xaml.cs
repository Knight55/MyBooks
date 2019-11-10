using System.Windows;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Extensions.Logging;
using MyBooks.Client.ViewModels;
using ReactiveUI;
using Splat;

namespace MyBooks.Client.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow, IViewFor<SettingsViewModel>
    {
        private readonly ILogger<SettingsWindow> _logger;

        public SettingsWindow()
        {
            InitializeComponent();

            _logger = Locator.Current.GetService<ILogger<SettingsWindow>>();
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(SettingsViewModel),
                typeof(SettingsWindow),
                new PropertyMetadata(null));

        /// <inheritdoc/>
        public SettingsViewModel ViewModel
        {
            get => (SettingsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SettingsViewModel)value;
        }

        private void OnThemeChangeClicked(object sender, RoutedEventArgs e)
        {
            var currentTheme = ThemeManager.DetectTheme(Application.Current);
            var selectedTheme = (MahApps.Metro.Theme) themeSelector.SelectedItem;

            if (selectedTheme == null)
            {
                _logger.LogInformation($"No theme selected, keeping the current one ({currentTheme.DisplayName})");
                return;
            }

            ThemeManager.ChangeTheme(Application.Current, selectedTheme);
            _logger.LogInformation($"Theme successfully changed to: {selectedTheme.DisplayName}");
        }
    }
}
