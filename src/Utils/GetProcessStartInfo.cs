using System.Diagnostics;

partial class Utils
{
    public static ProcessStartInfo GetProcessStartInfo()
    {
        //Later add -i=../main/enums
        //"-i=\"VotV/Content/main/enums\" " + //Later add -i=../main/interfaces
        ProcessStartInfo UPakProgramInfo = new ProcessStartInfo(
              GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\repak",
              "unpack " +
              "-f " +
              "-i=\"VotV/Content/main/datatables\" " +
              "-i=\"VotV/Config\" " +
              $"-o=\"Extracted\\{GlobalVariables.FusionPatchFileName}\" \"" +
              GlobalVariables.FileMainPak + "\""
        );

        UPakProgramInfo.WorkingDirectory = GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\";
        UPakProgramInfo.UseShellExecute = false;
        UPakProgramInfo.CreateNoWindow = true;

        Process.Start(UPakProgramInfo)?.WaitForExit();

        return UPakProgramInfo;
    }
}
