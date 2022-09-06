namespace testing_app
{
    public class IndexIntegration
    {
        public double Id { get; set;}
        public string? Customer { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public int Size { get; set; }
        public string? LastSuccessfulRun { get; set; }
        public string? LastRun { get; set; }
        public int Duration { get; set; }
        public int SuccessRate { get; set; }
        public string? LastEditedBy { get; set; }
        public string? TransferType { get; set; }
        public int RowsPerMin { get {return Size/Duration;} }
        public string? Direction { get; set; }
    }
}