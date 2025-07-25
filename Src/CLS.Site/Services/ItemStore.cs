using CLS.Common.DTO;

namespace CLS.Site.Services;

/// <summary>
/// The store for command items.
/// </summary>
public class ItemStore
{
    #region -> Events
    /// <summary>
    /// Event that is triggered when the items in the store are updated.
    /// </summary>
    public event Action? OnUpdated;
    #endregion


    #region -> Private Fields
    private readonly Dictionary<Guid, CommandTaskDto> _items = [];
    #endregion


    #region -> Public Properties
    /// <summary>
    /// Gets the collection of command items.
    /// </summary>
    public IReadOnlyCollection<CommandTaskDto> Items => _items.Values;
    #endregion


    #region -> Methods
    /// <summary>
    /// Update the contents of the store with new items.
    /// </summary>
    /// <param name="item">The item to add or update</param>
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
    #endregion
}
