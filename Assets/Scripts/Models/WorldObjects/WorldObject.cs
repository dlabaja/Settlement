using Enums;

namespace Models.WorldObjects;

public abstract class WorldObject
{
    public abstract WorldObjectType WorldObjectType { get; }
}
