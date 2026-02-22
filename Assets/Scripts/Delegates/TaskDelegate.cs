using Models.Villagers;
using Models.WorldObjects;
using System.Threading.Tasks;

namespace Delegates;

public delegate Task TaskDelegate(Villager source, WorldObject destination);