using System;
using System.Net;

namespace InstaGenerator.ImageFilter.Interfaces
{
    public interface IRemoteImage : IDisposable
    {
        void Download(WebClient webClient);

        void Resize();
        
    }
}