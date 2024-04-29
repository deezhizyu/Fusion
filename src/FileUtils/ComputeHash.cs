using System.Security.Cryptography;

partial class FileUtils
{
    public static void ComputeHash(ref SHA256 hashingInstance, FileInfo file, ref List<byte> output)
    {
        using FileStream stream = file.Open(FileMode.Open);

        output.AddRange(hashingInstance.ComputeHash(stream));
    }
}
