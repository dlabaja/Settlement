using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utils;

public static class JsonUtils
{
    public async static Task<T> JsonToModel<T>(string path)
    {
        await using var stream = IOUtils.GetFileStream(path, FileMode.Open);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

    public static string ModelToJson<T>(T model)
    {
        return JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
    }

    public static void SaveJson(string path, string json)
    {
        IOUtils.SaveFile(path, json);
    }
}