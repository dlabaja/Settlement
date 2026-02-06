using System.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Converters;

public static class Converter
{
    public async static Task<T> FromJson<T>(string path, T defaultData)
    {
        try
        {
            var data = await JsonUtils.JsonToModel<T>(path);
            return data;
        }
        catch
        {
            Debug.LogWarning($"Cannot load config for {path}, replacing with default config");
            return defaultData;
        }
    }
}
