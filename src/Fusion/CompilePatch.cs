using System.Diagnostics;

partial class Fusion
{
    public static void CompilePatch(ref FileInfo[] PakFiles, ref FileInfo[] CfgFiles)
    {
        // Disabled for now

        // bool bContinue = FileUtils.ComputeFilesHashes(ref PakFiles, ref CfgFiles);

        // if (!bContinue)
        // {
        //   return;
        // }

        // Kill game processes
        foreach (FileInfo pak in PakFiles)
        {
            foreach (var p in FileUtils.WhoIsLocking(pak.FullName))
            {
                p.Kill();
                p.WaitForExit();
            }
        }

        if (File.Exists(GlobalVariables.FilePatchPak))  //Case R2Modman
        {
            File.Delete(GlobalVariables.FilePatchPak);
        }

        if (Directory.Exists(GlobalVariables.PathExtracted))    //Re-creating since some mods can be deleted and uassets will be left over,
        {
            Directory.Delete(GlobalVariables.PathExtracted, true);    //making it into the resulting patch.pak
        }

        Directory.CreateDirectory(GlobalVariables.PathExtracted);

        // Start UPak
        ProcessStartInfo UPakProgramInfo = Utils.GetProcessStartInfo();

        // Extart paks content
        ExtractPaksContent(UPakProgramInfo, PakFiles);

        //Fusing datatables
        FuseDatatables(PakFiles);

        //Fusing DefaultInput
        FuseInputs(CfgFiles);

        //Pak-ing results
        PakResults(PakFiles, CfgFiles, UPakProgramInfo);

        Utils.LaunchGame();
    }
}
