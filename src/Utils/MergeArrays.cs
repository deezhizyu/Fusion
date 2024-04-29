partial class Utils
{
    public static void MergeArrays(ref FileInfo[] Array1, ref FileInfo[] Array2)
    {
        Array.Resize(ref Array1, Array1.Length + Array2.Length);
        Array.Copy(Array2, 0, Array1, Array1.Length - Array2.Length, Array2.Length);
    }
}
