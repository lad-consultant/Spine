namespace TheSpine.Infrastructure.DataAccess.Graph
{
    public interface IGraphService
    {
        Task<bool> IsModerator(string email);
    }
}
