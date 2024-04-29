using System.Security.Cryptography;

partial class FileUtils
{
    public static bool ComputeFilesHashes(ref FileInfo[] PakFiles, ref FileInfo[] CfgFiles)
    {
        List<byte> tmpHash = new List<byte>(32 * (PakFiles.Length + CfgFiles.Length));

        SHA256 hashingInstance = SHA256.Create();

        foreach (FileInfo pak in PakFiles)
        {
            try
            {
                ComputeHash(ref hashingInstance, pak, ref tmpHash);
            }
            catch (IOException)
            {
                foreach (var p in WhoIsLocking(pak.FullName))
                {
                    p.Kill();
                    p.WaitForExit();
                }

                ComputeHash(ref hashingInstance, pak, ref tmpHash);
            }
        }

        foreach (FileInfo cfg in CfgFiles)
        {
            ComputeHash(ref hashingInstance, cfg, ref tmpHash);
        }

        GlobalVariables.ResultHash = hashingInstance.ComputeHash(tmpHash.ToArray());

        if (File.Exists(GlobalVariables.FileHash) && GlobalVariables.ResultHash.SequenceEqual(File.ReadAllBytes(GlobalVariables.FileHash)) && !GlobalVariables.bDebug)
        {
            Logger.Log(System.Text.Encoding.UTF8.GetString(GlobalVariables.ResultHash));
            Logger.Log(System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(GlobalVariables.FileHash)));
            return false;
        }

        if (File.Exists(GlobalVariables.FilePatchPak))  //Case R2Modman
        {
            File.Delete(GlobalVariables.FilePatchPak);
        }

        if (Directory.Exists(GlobalVariables.PathExtracted))    //Re-creating since some mods can be deleted and uassets will be left over,
        {
            Directory.Delete(GlobalVariables.PathExtracted, true);    //making it into the resulting VotV-WindowsNoEditor_p
        }

        Directory.CreateDirectory(GlobalVariables.PathExtracted);

        return true;
    }
}
