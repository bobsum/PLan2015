namespace Plan2015.Score.ScoreBoard
{
    public static class FileHelper
    {
        public static string CleanPath(string path)
        {
            // Windows:
            return path.Replace('/', '\\');
            // Mac:
            //return path.Replace('\\', '/');
        }
    }
}
