namespace Portfolio.Domain.Posts.Models
{
    public class ModelConstants
    {
        public class Common
        {
            public const int Zero = 0;
        }

        public class ForPost
        {
            public const int MinTitleLength = 2;
            public const int MaxTitleLength = 50;

            public const int MinContentLength = 51;
            public const int MaxContentLength = 10000;
        }

        public class ForTag
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 50;
        }
    }
}
