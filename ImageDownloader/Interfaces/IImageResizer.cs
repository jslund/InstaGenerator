using System.IO;

namespace InstaGenerator.ImageFilter.Interfaces
{
    public interface IImageResizer
    {
        void ResizeAndSave(Stream inputStream, FileStream fileStream);
    }
}