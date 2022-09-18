using AthenicConsulting.Office.Office.Data;
using Microsoft.AspNetCore.Http;

namespace AthenicConsulting.Office.Office.Interfaces
{
    public interface IFileHelper
    {
        public FileResult UploadFile(string pathToUpload, IFormFile formFile, bool deleteExisting = false);
        public IEnumerable<FileResult> UploadFiles(string pathToUpload, IEnumerable<IFormFile> formFiles);
        public FileResult CreateBrandFolder(string brandName, bool deleteExisting = false);
        public FileResult CreateCampaignFolder(string brandName, string campaignName, bool deleteExisting = false);
        public void UpdateFolderName(string oldName, string newName);
        public void UpdateFileName(string oldName, string newName);   
        string ReadToEnd(string path);
        bool WriteToEnd(string path, string values);
    }
}
