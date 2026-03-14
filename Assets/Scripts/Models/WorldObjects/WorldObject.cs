using Enums;
using Models.Systems.Inventory;
using Models.Villagers;
using System;
using System.Threading.Tasks;

namespace Models.WorldObjects;

public abstract class WorldObject : IDisposable
{
    public abstract WorldObjectType WorldObjectType { get; }
    public abstract Inventory Inventory { get; }
    public event Action<WorldObject> Destroy;
    
    public abstract Task VillagerTask(Villager villager);

    protected void InvokeDestroy(WorldObject worldObject)
    {
        Destroy?.Invoke(worldObject);
    }
    
    public virtual void Dispose() {}
}
