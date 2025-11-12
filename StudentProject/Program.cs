class Program
{
    static void Main()
    {
        var manager = new StudentManager();
        manager.Load();   
        manager.Run();   
        manager.Save();   
    }
}
