namespace CLS.Site.Services;

/// <summary>
/// The service to update command items in the background.
/// </summary>
public class ItemUpdateService : BackgroundService
{
    private readonly ICLSAPIService _itemApi;
    private readonly ItemStore _itemStore;

    public ItemUpdateService(ICLSAPIService itemApi, ItemStore itemStore)
    {
        _itemApi = itemApi;
        _itemStore = itemStore;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var items = await _itemApi.GetItemUpdatesAsync();

            if (items.Count > 0)
            {
                _itemStore.UpdateItems(items);
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), stoppingToken);
        }
    }
}
