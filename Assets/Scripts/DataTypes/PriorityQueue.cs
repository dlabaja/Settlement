using System.Collections.Generic;

namespace DataTypes;

public class PriorityQueue<T>
{
    private List<int> _priorities = new List<int>();
    public List<T> Items { get; } = new List<T>();
    public int Length { get; private set; }
    public bool IsEmpty => Length == 0;

    public void Add(T item, int priority)
    {
        Length++;
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

        RemoveAt(index);
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
            RemoveAt(0);
            return true;
        }
        return false;
    }

    public void Promote(T item, int newPrioriry)
    {
        Remove(item);
        Add(item, newPrioriry);
    }
    
    public int HighestPriority
    {
        get => Items.Count > 0 ? _priorities[0] : int.MinValue;
    }

    public bool TryGetPriority(T item, out int priority)
    {
        priority = 0;
        var index = Items.IndexOf(item);
        if (index == -1)
        {
            return false;
        }

        priority = _priorities[priority];
        return true;
    }

    public bool Contains(T item)
    {
        if (Items.Contains(item))
        {
            return true;
        }

        return false;
    }

    private void RemoveAt(int index)
    {
        if (index >= Length)
        {
            return;
        }

        Length--;
        Items.RemoveAt(0);
        _priorities.RemoveAt(0);
    }
}
