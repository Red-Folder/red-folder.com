namespace Red_Folder.Tests.Acceptance.Models
{
    public class StartResponse
    {
        public string Id { get; set; }
        public string StatusQueryGetUri { get; set; }
        public string SendEventPostUri { get; set; }
        public string TerminatePostUri { get; set; }
        public string PurgeHistoryDeleteUri { get; set; }
        public string RestartPostUri { get; set; }
    }
}
