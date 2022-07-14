namespace AirlineManagement.Utility
{
    public interface IApplicationContext
    {
        public int UserId { get; }
        public string Token { get; }
    }
}
