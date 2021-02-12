using System;
using System.Linq;
using Google.Cloud.Vision.V1;

namespace GoogleCloudClients
{
    public class GoogleVisionClient
    {

        private ImageAnnotatorClient _client;

        private Image _image;

        private string[] _labels;
        

        public void FetchTags()
        {
            var response = _client.DetectLabels(_image);
            Console.WriteLine("Retrieved labels");
            _labels = response.Where(x=> x.Score >= 0.5).Select(x => x.Description).ToArray();

        }

        public bool Validate(string primaryTag)
        {
            if (_labels.Contains(primaryTag))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public GoogleVisionClient(string uri)
        {
            _client = ImageAnnotatorClient.Create();
            _image = Image.FromUri(uri);

        }
        
    }
}