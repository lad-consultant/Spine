namespace TheSpine.Application.Features.Commands
{
    public class Response
    {
        public Dictionary<string, IEnumerable<string>> Errors { get; set; } = new();
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
