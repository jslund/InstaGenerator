using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SkiaSharp;
using InstaGenerator.ImageFilter.Interfaces;

namespace ImageDownloader
{
    public class RemoteImage : IRemoteImage
    {

        private readonly string _uri;

        private MemoryStream _image;

        private string _path;
        
        
        public void Download(WebClient webClient)
        {
            _image = new MemoryStream(webClient.DownloadData(_uri));
                
            Console.WriteLine($"Downloaded {_uri}");
        }

        public void Resize()
        {
            Console.WriteLine("Starting Resize");
            using (var sourceBitmap = SKBitmap.Decode(_image))
            {


                var toBitmap = new SKBitmap(width: 512, height: 512,
                    colorType: sourceBitmap.ColorType, alphaType: sourceBitmap.AlphaType);

                sourceBitmap.ScalePixels(toBitmap, SKFilterQuality.High);

                using (var fs = File.Create($"./files/{_path}.jpeg"))
                {
                    toBitmap.Encode(fs, SKEncodedImageFormat.Jpeg, 100);
                    
                }

                Console.WriteLine($"Resized {_path}");

            }
        }


        public void Dispose()
        {
            _image.Dispose();
        }

        public RemoteImage(string uri)
        {
            _uri = uri;
            _path = DateTime.Now.Ticks.ToString();

        }
    }
}