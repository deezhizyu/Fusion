partial class FileUtils
{
    public static void PopulateFileLists(ref FileInfo[] PakFiles, ref FileInfo[] PakFilesManual, ref FileInfo[] CfgFiles, ref FileInfo[] CfgFilesManual)
    {
        if (Directory.Exists(GlobalVariables.PathMod))
        {
            PakFiles = new DirectoryInfo(GlobalVariables.PathPak).GetFiles("*.pak", SearchOption.AllDirectories);
        }

        for (int i = 0; i < PakFiles.Length; i++)
        {
            if (PakFiles[i].Name == $"{GlobalVariables.FusionPatchFileName}.pak")
            {
                for (int j = i + 1; j < PakFiles.Length; j++)
                {
                    PakFiles[j - 1] = PakFiles[j];
                }

                Array.Resize(ref PakFiles, PakFiles.Length - 1);

                break;
            }
        }

        if (Directory.Exists(GlobalVariables.PathBasePak))
        {
            PakFilesManual = new DirectoryInfo(GlobalVariables.PathBasePak).GetFiles("*.pak", SearchOption.AllDirectories);
        }

        if (Directory.Exists(GlobalVariables.PathCfg))
        {
            CfgFiles = new DirectoryInfo(GlobalVariables.PathCfg).GetFiles("*DefaultInput.ini", SearchOption.AllDirectories);
        }

        if (Directory.Exists(GlobalVariables.PathBaseCfg))
        {
            CfgFilesManual = new DirectoryInfo(GlobalVariables.PathBaseCfg).GetFiles("*DefaultInput.ini", SearchOption.AllDirectories);
        }

        Utils.MergeArrays(ref PakFiles, ref PakFilesManual);
        Utils.MergeArrays(ref CfgFiles, ref CfgFilesManual);
    }
}
