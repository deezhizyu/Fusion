using static GlobalVariables;

partial class Fusion
{
    public static void InitializePaths(string[] args)
    {
        if (args.Length == 0)
        {   //Manual only
            PathMod = Environment.CurrentDirectory[..^$"/{FusionFolderName}".Length];
            PathExeNoShipping = PathMod[..^"/Mods".Length];

            string tmp = PathExeNoShipping[..^"/Binaries/Win64".Length];

            PathPak = tmp + @"\Content\Paks\LogicMods";
            PathCfg = tmp + @"\Config";

            PathBaseCfg = "";
            PathBasePak = "";
            FileMainPak = PathPak[..^"/LogicMods".Length] + @"\VotV-WindowsNoEditor.pak";

            PathExe = tmp[..^"/VotV".Length];

            bManualOnly = true;
        }
        else
        {   //Manual + R2Modman
            PathExeNoShipping = args[0];
            PathExe = PathExeNoShipping.Substring(0, PathExeNoShipping.Length - @"\VotV\Binaries\Win64".Length);
            PathMod = args[1];
            PathPak = args[2];
            PathCfg = args[3];

            PathBaseCfg = PathExe + @"\Config";
            PathBasePak = PathExe + @"\Content\Paks\LogicMods";
            FileMainPak = PathExe + @"\VotV\Content\Paks\VotV-WindowsNoEditor.pak";

            bManualOnly = false;
        }

        PathExtracted = PathMod + @$"\{FusionFolderName}\Extracted\";
        FileExtractedDefaultInput = PathMod + @$"\{FusionFolderName}\Extracted\{FusionPatchFileName}\VotV\Config\DefaultInput.ini";
        FileFusionCfg = PathCfg + @"\Fusion-config.ini";
        FileLastWrite = PathMod + @$"\{FusionFolderName}\LastWrite.bin";
        FilePatchPak = PathPak + @$"\{FusionFolderName}\{FusionPatchFileName}.pak";
        FilePass = PathMod + @$"\{FusionFolderName}\pass";

        PathAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        FileThunderstoreModsYml = PathMod[..^@"\shimloader\mod".Length] + @"\mods.yml";
        FileInput = PathAppData + @"\VotV\Saved\Config\WindowsNoEditor\Input.ini";
    }
}
