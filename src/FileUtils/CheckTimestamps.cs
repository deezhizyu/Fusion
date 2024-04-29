partial class FileUtils
{
    public static bool CheckTimestamps(FileInfo[] PakFiles, FileInfo[] CfgFiles)
    {
        string[] dateStamps;

        if (File.Exists(GlobalVariables.FileLastWrite))
        {
            dateStamps = File.ReadAllLines(GlobalVariables.FileLastWrite);
        }
        else
        {
            return true;
        }

        if (dateStamps.Length != (PakFiles.Length + CfgFiles.Length))
        {
            return true;
        }

        int index = 0;
        bool bHasChanges = CheckFilesTimestamps(PakFiles, dateStamps, ref index) || CheckFilesTimestamps(CfgFiles, dateStamps, ref index);

        if (index > 0 && dateStamps.Length == index) {
            Logger.Log("----------------------------------");
        }

        return bHasChanges;
    }

    private static bool CheckFilesTimestamps(FileInfo[] files, string[] dateStamps, ref int index)
    {
        for (int i = 0; i < files.Length; i++)
        {
            DateTime savedDate = DateTime.Parse(dateStamps[index]);
            DateTime currentDate = File.GetLastWriteTime(files[i].FullName);

            bool bIsEqual = Utils.IsDatesEqual(currentDate, savedDate);

            Logger.Log("----------------------------------");
            Logger.Log($"[DEBUG] File: {files[i].FullName}");
            Logger.Log($"[DEBUG] Saved datestamp: {savedDate}");
            Logger.Log($"[DEBUG] Current datestamp: {currentDate}");
            Logger.Log($"[DEBUG] Is equal: {bIsEqual}");

            if (bIsEqual)
            {
                return true;
            }

            index++;
        }

        return false;
    }
}
