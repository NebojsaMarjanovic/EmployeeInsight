namespace EmployeeInsight.Crawler.Models
{
    //model class used for json deserializing
    public class EmployeeJsonResponse
    {
        public string Id { get; set; }
        public string? EmployeeName { get; set; }
        public DateTimeOffset StarTimeUtc { get; set; }
        public DateTimeOffset EndTimeUtc { get; set; }
        public string? EntryNotes { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
    }
}
