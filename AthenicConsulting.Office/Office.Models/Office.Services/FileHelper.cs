using AthenicConsulting.Office.Office.Data;
using AthenicConsulting.Office.Office.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AthenicConsulting.Office.Office.Models.Office.Services
{
    public class FileHelper : IFileHelper
    {
        private readonly ILogger<FileHelper> _logger;
        private readonly IHostingEnvironment _hostEnvironment;

        public FileHelper(ILogger<FileHelper> logger, IHostingEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public string ReadToEnd(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            var streamReader = new StreamReader(path);
            var result = streamReader.ReadToEnd();
            streamReader.Close();
            return result;  
        }
        public bool WriteToEnd(string path, string values)
        {
            var streamWriter = new StreamWriter(path);
            streamWriter.WriteLine(values);
            return true;
        }
        public FileResult CreateBrandFolder(string brandName)
        {
            var fileResult = new FileResult { FileName = string.Empty, UploadedSuccessfully = false };
            var path = @$"{_hostEnvironment.WebRootPath}\marketdata\Brands\{brandName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                fileResult.UploadedSuccessfully = true;
                fileResult.FileName = path;
            }
            else
            {
                fileResult.UploadedSuccessfully = false;
                fileResult.FileName = path;
            }
            return fileResult;
        }

        public FileResult CreateCampaignFolder(string brandName, string campaignName)
        {
            var fileResult = new FileResult { FileName = string.Empty, UploadedSuccessfully = false };
            var path = @$"{_hostEnvironment.WebRootPath}\marketdata\Brands\{brandName}\Campaigns\{campaignName}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                fileResult.UploadedSuccessfully = true;
                fileResult.FileName = path;
            }
            else
            {
                fileResult.UploadedSuccessfully = false;
                fileResult.FileName = path;
            }
            return fileResult;
        }

        public FileResult UploadFile(string pathToUpload, IFormFile formFile)
        {
            var fileResult = new FileResult { FileName = string.Empty, UploadedSuccessfully = false };
            try
            {
                if (!Directory.Exists(pathToUpload))
                {
                    Directory.CreateDirectory(pathToUpload);
                }
                FileInfo fileInfo = new FileInfo(formFile.FileName);
                string fileNameWithPath = Path.Combine(pathToUpload, formFile.FileName);
                if (!File.Exists(fileNameWithPath))
                {
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    fileResult.UploadedSuccessfully = true;
                }
                else
                {
                    fileResult.UploadedSuccessfully = false;
                }
                fileResult.FileName = formFile.FileName;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occured while uploading the file = {ex}");

            }
            return fileResult;

        }

        public IEnumerable<FileResult> UploadFiles(string pathToUpload, IEnumerable<IFormFile> formFiles)
        {
            var fileResults = new List<FileResult>();
            foreach(var formFile in formFiles)
            {
                 var fileResult = UploadFile(pathToUpload, formFile);
                fileResults.Add(fileResult);
            } 
            return fileResults;
        }
    }
}
