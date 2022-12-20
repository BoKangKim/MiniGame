namespace ExcelScriptable
{
    internal class Program
    {
        // TODO
        // GUI
        // Params -> File Name

        static void Main(string[] args)
        {
            FileManageMent fmm = new FileManageMent(args[0], args[1], args[2]);
            fmm.Run();
        }
    }
}
