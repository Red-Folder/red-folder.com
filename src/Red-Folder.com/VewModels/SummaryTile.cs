namespace RedFolder.ViewModels
{
    public class SummaryTile
    {
        public string Title { get; private set; }
        public Paragraphs Description { get; private set; }
        public string Action { get; private set; }
        public string Controller { get; private set; }
        public string LinkName { get; private set; }
        public string IconClass { get; private set; }

        public SummaryTile(string title, Paragraphs description, string action, string controller, string linkName, string iconClass = null)
        {
            Title = title;
            Description = description;
            Action = action;
            Controller = controller;
            LinkName = linkName;
            IconClass = iconClass;
        }
    }
}