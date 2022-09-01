using AthenicConsulting.Office.Office.Data;
using Microsoft.AspNetCore.Http;

namespace AthenicConsulting.Office.Office.Interfaces
{
    public interface IFileHelper
    {
        public FileResult UploadFile(string pathToUpload, IFormFile formFile);
        public IEnumerable<FileResult> UploadFiles(string pathToUpload, IEnumerable<IFormFile> formFiles);
        public FileResult CreateBrandFolder(string brandName);
        public FileResult CreateCampaignFolder(string brandName, string campaignName);
        string ReadToEnd(string path);
        bool WriteToEnd(string path, string values);
    }
}
