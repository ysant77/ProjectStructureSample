namespace ProjectStructureSample
{
    public static class MainWindowSingletion
    {
        public static MainWindow Instance { get; set; }
        public static bool IsChildRemoved { get; set; } 
        public static MainWindow GetMainWindowInstance()
        {
            if (Instance == null)
                Instance = new MainWindow();
            return Instance;
        }
    }
}