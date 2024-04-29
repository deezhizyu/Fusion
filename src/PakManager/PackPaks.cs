using System.Diagnostics;

partial class Fusion
{
    private static void PakResults(FileInfo[] PakFiles, FileInfo[] CfgFiles, ProcessStartInfo UPakProgramInfo)
    {
        UPakProgramInfo.Arguments = @$"pack -m=../../../VotV/ --version=V11 ""Extracted/{GlobalVariables.FusionPatchFileName}/VotV/"" ""Extracted/{GlobalVariables.FusionPatchFileName}.pak""";
        Process.Start(UPakProgramInfo)?.WaitForExit();

        Directory.CreateDirectory(GlobalVariables.PathPak + @$"\{GlobalVariables.FusionFolderName}");
        File.Move(GlobalVariables.PathExtracted + @$"{GlobalVariables.FusionPatchFileName}.pak", GlobalVariables.FilePatchPak);

        //File.WriteAllBytes(GlobalVariables.FileHash, GlobalVariables.ResultHash);

        //File.WriteAllText(GlobalVariables.FileLastWrite, DateTime.Now.ToString());
        //Write date stamps of changes
        List<string> lines = new List<string>();

        foreach (FileInfo pak in PakFiles)
        {
            lines.Add(File.GetLastWriteTime(pak.FullName).ToString());
        }

        foreach (FileInfo cfg in CfgFiles)
        {
            lines.Add(File.GetLastWriteTime(cfg.FullName).ToString());
        }

        File.WriteAllLines(GlobalVariables.FileLastWrite, lines);
    }
}
