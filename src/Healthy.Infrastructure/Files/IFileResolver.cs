using System.IO;
using System.Threading.Tasks;
using Healthy.Core.Types;

namespace Healthy.Infrastructure.Files
{
    public interface IFileResolver
    {
        Maybe<File> FromBase64(string base64, string name, string contentType);
        Task<Maybe<Stream>> FromUrlAsync(string url);
    }
}