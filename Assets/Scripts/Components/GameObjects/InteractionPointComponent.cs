using Models.WorldObjects;
using Reflex.Attributes;
using Services.Resources;
using UnityEngine;

namespace Components.GameObjects
{
    public class InteractionPointComponent : MonoBehaviour
    {
        [Inject] private TerrainService _terrainService;
        public InteractionPoint InteractionPoint { get; private set; }

        public void Awake()
        {
            InteractionPoint = new InteractionPoint(_terrainService.GroundVector3(transform.position));
        }
    }
}
