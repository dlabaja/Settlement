using Enums;
using Models.WorldObjects;
using UnityEngine;

namespace Factories;

public class InteractionPointFactory
{
    public InteractionPoint Create(Vector3 position, WorldObjectType worldObjectType)
    {
        return new InteractionPoint(position, InteractionPoint.GetMaxOccupantCount(worldObjectType));
    }
}
