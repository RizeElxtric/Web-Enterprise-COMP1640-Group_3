namespace MarketingEvent.Database.Constants
{
    public static class ArticleConstants
    {
        public const string FileRoot = "articles";

        public static readonly List<string> ArticleExtensions = new List<string>()
        {
            ".doc",
            ".docx"
        };

        public static readonly List<string> ImageExtensions = new List<string>()
        {
            ".png",
            ".jpeg",
            ".jpg"
        };
    }
}
