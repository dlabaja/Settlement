using System.IO;
using System.Threading.Tasks;

namespace Utils;

public static class IOUtils
{
    public static void SaveFile(string path, string content)
    {
        using StreamWriter sw = File.CreateText(path);
        sw.WriteLine(content);
    }

    public async static Task<string[]> GetFileLines(string path)
    {
        return await File.ReadAllLinesAsync(path);
    }
        
    public async static Task<string> GetFileText(string path)
    {
        return await File.ReadAllTextAsync(path);
    }

    public static FileStream GetFileStream(string path, FileMode mode)
    {
        return File.Open(path, mode);
    }
}