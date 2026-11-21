using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using ViviliamApi2.Controllers;

[ApiController]
[Route("")]
public class FileController : BaseViviliamController
{
    private readonly IWebHostEnvironment _env;
    private string _imageRootPath;
    

    public FileController(IWebHostEnvironment env)
    {
        _env = env;
        _imageRootPath = Path.Combine(_env.WebRootPath, "assets", "homeimages");
        
    }

    [HttpGet("assets/voiceover/{jsonName}")]
    public IActionResult GetImage_json(string jsonName)
    {
        if (!IsValidFileName(jsonName))
        {
            return BadRequest("Invalid file name.");
        }
        var imagePath = Path.Combine(_env.WebRootPath + "/assets/voiceover/", jsonName);
        if (System.IO.File.Exists(imagePath))
        {
            return PhysicalFile(imagePath, "application/json");
        }
        else
        {
            return NotFound("File not found.");
        }
        
    }

    [HttpGet("assets/homeimages/{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        LogIncomingRequest("File/GetImage", imageName);
        try
        {
            if (!Regex.IsMatch(imageName, @"^[a-zA-Z0-9_.-]+$"))
            {
                return BadRequest("Invalid image name.");
            }
            //var imagePath = Path.Combine(_env.WebRootPath + "/assets/homeimages/", imageName);
            var imagePath = Path.Combine(_imageRootPath, imageName);

            // Check if the file exists
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("File not found.");
            }
            var contentType = "application/octet-stream"; // Default content type
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (provider.TryGetContentType(imagePath, out var mimeType))
            {
                contentType = mimeType;
            }

            return PhysicalFile(imagePath, contentType);
        }
        catch(Exception ex)
        {
            LogException("File/GetImage", ex);
            return StatusCode(500, _exceptionMessage);
        }
        //return PhysicalFile(imagePath, "image/jpeg");
    }

    [HttpGet("assets/audio/{audioName}")]
    public IActionResult GetAudio(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/audio/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }
    [HttpGet("assets/voiceover/1_WEB/{audioName}")]
    public IActionResult GetAudio_WEB(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/voiceover/1_WEB/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }
    [HttpGet("assets/voiceover/2_KJV/{audioName}")]
    public IActionResult GetAudio_2_KJV(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/voiceover/2_KJV/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }
    [HttpGet("assets/voiceover/3_BSI/{audioName}")]
    public IActionResult GetAudio_3_BSI(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/voiceover/3_BSI/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }
    [HttpGet("assets/voiceover/5_ETB/{audioName}")]
    public IActionResult GetAudio_5_ETB(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/voiceover/5_ETB/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }

    [HttpGet("assets/audio/Proverbs/{audioName}")]
    public IActionResult GetAudio_Proverbs(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/audio/Proverbs/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }
    [HttpGet("assets/audio/Psalms/{audioName}")]
    public IActionResult GetAudio_Psalms(string audioName)
    {
        var audioPath = Path.Combine(_env.WebRootPath + "/assets/audio/Psalms/", audioName);
        return PhysicalFile(audioPath, "audio/mpeg", enableRangeProcessing: true);
    }

    [HttpGet("assets/video/{videoName}")]
    public IActionResult GetVideo(string videoName)
    {
        var videoPath = Path.Combine(_env.WebRootPath + "/assets/video/", videoName);
        return PhysicalFile(videoPath, "video/mp4");
    }
}
