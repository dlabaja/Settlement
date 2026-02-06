using System.IO;
using UnityEngine;

namespace Constants;

public static class Paths
{
    public static readonly string Assets = Application.dataPath;
    public static readonly string Resources = Path.Combine(Assets, "Resources");
    public static readonly string Data = Path.Combine(Resources, "Data");
    public static readonly string SettingsJson = Path.Combine(Data, "settings.json");
    public static readonly string VillagerNamesJson = Path.Combine(Data, "villager_names.json");
}