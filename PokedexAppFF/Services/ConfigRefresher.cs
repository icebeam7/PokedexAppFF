using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace PokedexAppFF.Services
{
    public class ConfigRefresherService : IConfigRefresherService
    {
        private readonly IEnumerable<IConfigurationRefresher> _refreshers = null;

        public ConfigRefresherService(
            IConfigurationRefresherProvider refresherProvider)
        {
            _refreshers = refresherProvider.Refreshers;
        }

        public async Task RefreshConfiguration()
        {
            foreach (var refresher in _refreshers)
            {
                _ = refresher.TryRefreshAsync();
            }
        }
    }
}
