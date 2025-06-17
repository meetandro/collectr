using CollectR.Infrastructure.Common;
using Microsoft.Extensions.Options;

namespace CollectR.Api.Options;

public class ImageRootSetup(IWebHostEnvironment env) : IConfigureOptions<ImageRoot>
{
    public void Configure(ImageRoot options)
    {
        options.Path = env.WebRootPath;
    }
}
