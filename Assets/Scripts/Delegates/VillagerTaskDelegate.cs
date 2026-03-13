using Models.Villagers;
using Models.WorldObjects;
using System.Threading.Tasks;

namespace Delegates;

public delegate Task VillagerTaskDelegate(Villager source, WorldObject destination);

