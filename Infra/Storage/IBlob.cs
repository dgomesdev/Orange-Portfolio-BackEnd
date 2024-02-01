namespace Orange_Portfolio_BackEnd.Infra.Storage
{
    public interface IBlob
    {
        Task<string> Upload(IFormFile file);
    }
}
