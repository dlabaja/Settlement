using Models.Villager;
using System.Threading.Tasks;
using UnityEngine;

namespace Delegates;

public delegate Task TaskDelegate(Villager source, Vector3 destination);