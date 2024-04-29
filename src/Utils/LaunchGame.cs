using System.Diagnostics;

partial class Utils
{
    public static void LaunchGame()
    {
        if (GlobalVariables.bManualOnly)
        {
            Process.Start(GlobalVariables.PathExeNoShipping + @"\VotV-Win64-Shipping.exe");//, args);
        }
        else
        {
            File.Create(GlobalVariables.FilePass);
            Console.WriteLine(GlobalVariables.PathExe);
            Process.Start(GlobalVariables.PathExe + @"\VotV.exe", "--mod-dir \"" + GlobalVariables.PathMod + "\" --pak-dir \"" + GlobalVariables.PathPak + "\" --cfg-dir \"" + GlobalVariables.PathCfg + "\"");
        }
        //System.Diagnostics.Process.Start(GlobalVariables.PathExe + @"\VotV.exe");
    }
}
