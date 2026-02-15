using System.Collections.Generic;
using System.Linq;

namespace DataTypes;

public class PriorityQueue<T>
{
    private List<int> _priorities = new List<int>();
    public List<T> Items { get; } = new List<T>();

    public void Add(T item, int priority)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (_priorities[i] >= priority)
            {
                continue;
            }

            _priorities.Insert(i, priority);
            Items.Insert(i, item);
            return;
        }
        
        _priorities.Add(priority);
        Items.Add(item);
    }

    public void Remove(T item)
    {
        var index = Items.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        Items.RemoveAt(index);
        _priorities.RemoveAt(index);
    }

    public bool TryPeek(out T value)
    {
        value = default;
        if (Items.Count > 0)
        {
            value = Items[0];
            return true;
        }
        return false;
    }

    public bool TryPop(out T value)
    {
        value = default;
        if (Items.Count > 0)
        {
            value = Items[0];
            Items.RemoveAt(0);
            _priorities.RemoveAt(0);
            return true;
        }
        return false;
    }
    
    public int HighestPriority
    {
        get => Items.Count > 0 ? _priorities[0] : int.MinValue;
    }

    public List<(T item, int priority)> ItemsWithPriorities
    {
        get => Items.Zip(_priorities, (item, priority) => (item, priority)).ToList();
    }
}
