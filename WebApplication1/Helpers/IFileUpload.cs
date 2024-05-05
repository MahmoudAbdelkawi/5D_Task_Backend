namespace WebApplication1.Helpers
{
    public interface IFileUpload
    {
        public (string, string) UploadFile(IFormFile formFilestring, UploadDirectoriesEnum FileFor);

        public string GetFilePath(UploadDirectoriesEnum FileFor, string _fileName);
    }
}
