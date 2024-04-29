partial class Fusion
{
    private static void FuseInputs(FileInfo[] CfgFiles)
    {
        var defaultInputCfg = File.AppendText(GlobalVariables.FileExtractedDefaultInput);
        List<string> inputs = new List<string>();

        foreach (FileInfo file in new DirectoryInfo(GlobalVariables.PathMod + @$"\{GlobalVariables.FusionFolderName}\Extracted\").GetFiles("DefaultInput.ini"))
        {
            if (file.Directory?.Name == "VotV-WindowsNoEditor")
            {
                continue;
            }

            inputs.AddRange(File.ReadAllLines(file.FullName));

            defaultInputCfg.Write("\n#" + file.Directory?.Name + "\\" + file.Name + "\n");
            defaultInputCfg.Write(File.ReadAllText(file.FullName));
            defaultInputCfg.Write("\n");
        }

        foreach (FileInfo file in CfgFiles)
        {
            inputs.AddRange(File.ReadAllLines(file.FullName));

            defaultInputCfg.Write("\n#" + file.Directory?.Name + "\\" + file.Name + "\n");
            defaultInputCfg.Write(File.ReadAllText(file.FullName));
            defaultInputCfg.Write("\n");
        }

        defaultInputCfg.Close();

        string userInput = File.ReadAllText(GlobalVariables.FileInput);
        var fileHandle = File.AppendText(GlobalVariables.FileInput);

        foreach (var inputString in inputs)
        {
            var iStart = inputString.IndexOf("\"");
            var iEnd = inputString.IndexOf("\"", iStart + 1);

            if (iEnd == -1)
            {
                continue;
            }

            if (!userInput.Contains(inputString.Substring(iStart, iEnd - iStart + 1)))
            {
                if (inputString.StartsWith('+') | inputString.StartsWith('-'))
                {
                    fileHandle.WriteLine(inputString[1..]);
                }
                else
                {
                    fileHandle.WriteLine(inputString);
                }
            }
        }

        fileHandle.Close();
    }
}
