using CLS.Common.DTO;

namespace CLS.Site.Services;

public class ItemStore
{
    private readonly Dictionary<Guid, CommandTaskDto> _items = [];
    public IReadOnlyCollection<CommandTaskDto> Items => _items.Values;
    public event Action? OnUpdated;

    public void UpdateItems(List<CommandTaskDto> newItems)
    {
        // Update existing items or add new ones
        foreach (CommandTaskDto item in newItems)
        {
            _items[item.Id] = item;
        }

        // TODO: Remove items that are no longer present
        // var idsToRemove = _items.Keys.Except(newItems.Select(i => i.Id)).ToList();
        // foreach (var id in idsToRemove)
        // {
        //     _items.Remove(id);
        // }

        // Notify subscribers that the items have been updated
        OnUpdated?.Invoke();
    }
}
