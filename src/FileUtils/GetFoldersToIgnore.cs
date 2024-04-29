partial class FileUtils
{
    public static List<string> GetFoldersToIgnore()
    {
        string fileThunderstoreModsYml = GlobalVariables.PathMod[..^@"\shimloader\mod".Length] + @"\mods.yml";
        string[] list = File.ReadAllLines(fileThunderstoreModsYml);

        List<string> modFoldersToIgnore = new List<string>();

        if (!GlobalVariables.bManualOnly)
        {
            foreach (string item in list)
            {
                if (item.StartsWith("  name: "))
                {
                    string folderName = item.Substring("  name: ".Length);

                    bool folderIgnored = ShouldIgnoreFolder(list, folderName);

                    if (folderIgnored)
                    {
                        modFoldersToIgnore.Add(folderName);
                    }
                }
            }
        }

        return modFoldersToIgnore;
    }

    private static bool ShouldIgnoreFolder(string[] list, string folderName)
    {
        for (int i = Array.IndexOf(list, folderName) + 1; i < list.Length; i++)
        {
            if (list[i].StartsWith("  name: "))
            {
                break;
            }
            else if (list[i].StartsWith("  enabled: ") && list[i].Substring("  enabled: ".Length) == "false")
            {
                return true;
            }
        }

        return false;
    }
}
