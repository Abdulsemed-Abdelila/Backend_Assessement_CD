using CloudinaryDotNet.Actions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using SparkTank.Application.Persistence.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkTank.Infrastructure.services;

public class GoogleService : IGoogleService
{
    public async Task<string> UploadFile(IFormFile File)
    {
        GoogleCredential credential = GoogleCredential.FromJson(Environment.GetEnvironmentVariable("GOOGLE_CREDENTIAL")).CreateScoped(new[]
        {
            DriveService.ScopeConstants.DriveFile
        });
        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "SparkTank"
        });

        var FolderId = Environment.GetEnvironmentVariable("GOOGLE_FOLDER_ID");
        Google.Apis.Drive.v3.Data.File driveFile = new Google.Apis.Drive.v3.Data.File
        {
            Name = File.FileName,
            Parents = new List<string> { FolderId }
        };

        // Create upload request
        var filetype = "";
        FilesResource.CreateMediaUpload insertRequest = service.Files.Create(driveFile, File.OpenReadStream(), filetype);
        insertRequest.Fields = "id, webViewLink, webContentLink";
        var result = await insertRequest.UploadAsync();
        var baseurl = insertRequest.ResponseBody.WebContentLink;
        
        return await Task.FromResult(insertRequest.ResponseBody.WebViewLink);
        
    }

}
