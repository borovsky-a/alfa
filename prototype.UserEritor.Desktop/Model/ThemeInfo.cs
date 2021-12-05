using System;

namespace prototype.UserEritor.Desktop
{
    public class ThemeInfo
    {
        public ThemeInfo(string name, string displayName, Uri path)
        {
            Name = name;
            DisplayName = displayName;
            Path = path;
        }
        public string Name { get; }
        public string DisplayName { get; }
        public Uri Path { get; }
    }
}
