namespace BlazorServerApp
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string? PlayerName {get; set; }
        public int Handicap { get; set; }
        public bool IsActive { get; set; }
    }
}
