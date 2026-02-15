using DataTypes;
using NUnit.Framework;

namespace Tests.DataTypes;

public class PriorityQueueTest
{
    public PriorityQueue<string> GetQueue => new PriorityQueue<string>();
    
    [Test]
    public void Constructor()
    {
        var queue = GetQueue;
        Assert.AreEqual(queue.HighestPriority, int.MinValue);
        Assert.Zero(queue.Items.Count);
    }
    
    [Test]
    public void Add()
    {
        var queue = GetQueue;
        queue.Add("ahoj", 5);
        queue.Add("sus", 0);
        queue.Add("amogus", 10);
        Assert.AreEqual(queue.HighestPriority, 10);
        Assert.AreEqual(queue.Items[0], "amogus");
        Assert.AreEqual(queue.Items[1], "ahoj");
        Assert.AreEqual(queue.Items[2], "sus");
    }

    [Test]
    public void Remove()
    {
        var queue = GetQueue;
        queue.Add("ahoj", 5);
        queue.Add("sus", 0);
        queue.Add("amogus", 10);
        
        queue.Remove("ahoj");
        Assert.AreEqual(queue.HighestPriority, 10);
        Assert.AreEqual(queue.Items[0], "amogus");
        Assert.AreEqual(queue.Items[1], "sus");
        
        queue.Remove("amogus");
        Assert.AreEqual(queue.HighestPriority, 0);
        Assert.AreEqual(queue.Items[0], "sus");
    }
}
