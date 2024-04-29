using System.Diagnostics;

partial class Fusion
{
    private static void ExtractPaksContent(ProcessStartInfo UPakProgramInfo, FileInfo[] PakFiles)
    {
        // Get folders to ignore
        List<string> modFoldersToIgnore = FileUtils.GetFoldersToIgnore();

        foreach (FileInfo file in PakFiles)
        {
            if (file.Directory != null && modFoldersToIgnore.Contains(file.Directory.Name))
            {
                continue;
            }

            UPakProgramInfo.Arguments = "unpack -f -i=\"VotV/Content/Mods/" + Path.GetFileNameWithoutExtension(file.Name) + "/_Content\" -o=\"Extracted/" + Path.GetFileNameWithoutExtension(file.Name) + "\" \"" + file.FullName + "\""; // /main/datatables
            Process.Start(UPakProgramInfo)?.WaitForExit();

            string modExtractedPath = GlobalVariables.PathExtracted +
                Path.GetFileNameWithoutExtension(file.Name) +
                @"\VotV\Content\Mods\" +
                Path.GetFileNameWithoutExtension(file.Name) +
                @"\_Content";

            if (Directory.Exists(modExtractedPath))
            {
                string[] dirsToTransfer = Directory.GetDirectories(modExtractedPath);

                foreach (var dir in dirsToTransfer)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    if (dirInfo.Name == "main")
                    {
                        continue;
                    }

                    if (Directory.Exists(GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\Extracted\{GlobalVariables.FusionPatchFileName}\VotV\Content\" + dirInfo.Name))
                    {
                        Directory.Delete(GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\Extracted\{GlobalVariables.FusionPatchFileName}\VotV\Content\" + dirInfo.Name, true);
                    }

                    Directory.Move(dir, GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\Extracted\{GlobalVariables.FusionPatchFileName}\VotV\Content\" + dirInfo.Name);
                }
            }
        }
    }
}
