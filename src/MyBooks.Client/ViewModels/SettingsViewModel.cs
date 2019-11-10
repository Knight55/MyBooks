using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace MyBooks.Client.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        private readonly ILogger<SettingsViewModel> _logger;

        public SettingsViewModel(
            ILogger<SettingsViewModel> logger)
        {
            _logger = logger;
        }
    }
}