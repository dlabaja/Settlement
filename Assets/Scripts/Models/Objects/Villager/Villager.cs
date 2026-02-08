
using Enums;

namespace Models.Objects.Villager;

public class Villager : CustomObject
{
    public string Name { get; }
    public Gender Gender { get; }
    
    public Villager(string name, Gender gender)
    {
        Name = name;
        Gender = gender;
    }
}
