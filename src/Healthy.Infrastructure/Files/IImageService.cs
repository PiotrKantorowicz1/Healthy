using System.Collections.Generic;

namespace Healthy.Infrastructure.Files
{
    public interface IImageService
    {
        File ProcessImage(File file, double size);
        IDictionary<string,File> ProcessImage(File file);
    }
}