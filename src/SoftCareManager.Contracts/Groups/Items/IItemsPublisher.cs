using System.Collections.Generic;

namespace SoftCareManager.Contracts.Groups.Items
{
    public interface IItemsPublisher
    {
        IEnumerable<object> Items { get; set; }
    }
}