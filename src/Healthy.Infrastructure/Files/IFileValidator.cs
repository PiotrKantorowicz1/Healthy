namespace Healthy.Infrastructure.Files
{
    public interface IFileValidator
    {
        bool IsImage(File file);
    }
}