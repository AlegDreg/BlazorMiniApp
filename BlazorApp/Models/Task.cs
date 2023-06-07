namespace BlazorApp.Models
{
    public class Task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Status status { get; set; } = Status.Created;
        public DateTime dateCreate { get; set; }
    }

    public enum Status
    {
        Created,
        InProgress,
        Finished,
        Cancelled
    }
}