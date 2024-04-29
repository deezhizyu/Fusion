using static GlobalVariables;

class Program
{
    static void Main(string[] args)
    {
        Fusion.InitializePaths(args);

        Logger.Log("[INFO] Initializing");

        if (File.Exists(FilePass))
        {
            Logger.Log("[INFO] Pass");

            File.Delete(FilePass);
            return;
        }

        if (File.Exists(FileFusionCfg))
        {
            string res = File.ReadAllText(FileFusionCfg);
            if (res.StartsWith("debug=") && res["debug=".Length..].StartsWith("true"))
                bDebug = true;
        }

        /*
        if (File.Exists(FileLastWrite) && !bDebug)
        {
            var lastModUpdate = File.GetLastWriteTime(fileThunderstoreModsYml);
            var lastFusion = DateTime.Parse(File.ReadAllText(FileLastWrite));
            if (lastFusion > lastModUpdate)
                return;
        }*/

        // Populate file lists
        FileInfo[] PakFiles = new FileInfo[0];
        FileInfo[] PakFilesManual = new FileInfo[0];
        FileInfo[] CfgFiles = new FileInfo[0];
        FileInfo[] CfgFilesManual = new FileInfo[0];

        FileUtils.PopulateFileLists(ref PakFiles, ref PakFilesManual, ref CfgFiles, ref CfgFilesManual);

        try
        {
            //Checking timestamps on whether anything changed
            bool bShouldRecompile = FileUtils.CheckTimestamps(PakFiles, CfgFiles);

            Logger.Log(bShouldRecompile.ToString());

            if (bShouldRecompile)
            {
                Fusion.CompilePatch(ref PakFiles, ref CfgFiles);
            }
        }
        catch (Exception e)
        {
            Logger.Log("[ERROR] Error catched!");
            Logger.Log(e.ToString());
        }
    }
}
