public class Logger
{
    public static void Log(string message)
    {
        TextWriter textWriter = new StreamWriter($"{GlobalVariables.PathMod + $"/{GlobalVariables.FusionFolderName}"}/log.txt", true);

        textWriter.WriteLine(DateTime.Now.ToString() + ' ' + message);

        textWriter.Close();
    }
}
