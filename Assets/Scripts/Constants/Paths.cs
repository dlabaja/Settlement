using System.IO;
using UnityEditor;
using UnityEngine;

namespace Constants;

public static class Paths
{
    public static readonly string Assets = Application.dataPath;
    public static readonly string Resources = Path.Combine(Assets, "Resources");
    public static readonly string Settings = Path.Combine(Resources, "Settings");
    public static readonly string SettingsJson = Path.Combine(Settings, "settings.json");
}