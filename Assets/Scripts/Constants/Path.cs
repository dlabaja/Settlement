using UnityEngine;
using P = System.IO.Path;

namespace Constants;

public static class Path
{
    public static readonly string Assets = Application.dataPath;
    public static readonly string Resources = P.Combine(Assets, "Resources");
    public static readonly string Data = P.Combine(Resources, "Data");
    public static readonly string Materials = P.Combine(Resources, "Materials");
    public static readonly string SettingsJson = P.Combine(Data, "settings.json");
    public static readonly string VillagerNamesJson = P.Combine(Data, "villager_names.json");
}