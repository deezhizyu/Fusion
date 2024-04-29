public static class GlobalVariables
{
    public static string FusionFolderName = "NynrahGhost-Fusion";
    public static string FusionPatchFileName = "FusionPatch_P";

    public static string PathExeNoShipping { get; set; } = "";
    public static string PathExe { get; set; } = "";
    public static string PathMod { get; set; } = "";
    public static string PathPak { get; set; } = "";
    public static string PathCfg { get; set; } = "";

    public static string PathBaseCfg { get; set; } = "";
    public static string PathBasePak { get; set; } = ""; //LogicMods folder if R2Modman exists
    public static string FileMainPak { get; set; } = "";

    public static string PathExtracted { get; set; } = "";
    public static string FileFusionCfg { get; set; } = "";
    public static string FileLastWrite { get; set; } = "";
    public static string FilePass { get; set; } = "";
    public static string FileThunderstoreModsYml { get; set; } = "";
    public static string FileHash { get; set; } = "";
    public static string FilePatchPak { get; set; } = "";
    public static string FileExtractedDefaultInput { get; set; } = "";
    public static string PathAppData { get; set; } = "";
    public static string FileInput { get; set; } = "";

    public static bool bManualOnly { get; set; } = false;
    public static bool bDebug { get; set; } = false;

    public static byte[] ResultHash { get; set; } = Array.Empty<byte>();
}
