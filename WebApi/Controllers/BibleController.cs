using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViviliamAPI.BibleServices;

namespace ViviliamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibleController : ControllerBase
    {
        private readonly IBibleService _bibleService;

        public BibleController(IBibleService bibleService)
        {
            _bibleService = bibleService;
        }

        //[HttpGet("{version}/{book}/{chapter}")]
        //public async Task<ActionResult<IEnumerable<Bible>>> GetVerses(string version, short book, short chapter)
        //{
        //    if (book > 73 || chapter > 150)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        List<Bible> verses = await _bibleService.GetVerses(version, book, chapter);

        //        if (verses.Count == 0)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(verses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}


        //public async Task<IActionResult> GetVersesForMultipleVersions([FromBody] List<string> versions, short book, short chapter)
        [HttpGet("{version}/{book}/{chapter}")]
        public async Task<IActionResult> GetVerses(string version, short book, short chapter)
        {
            if (book > 73 || chapter > 150)
            {
                return NotFound();
            }

            try
            {
                string[] versions = version.Split('|');
                var verses = await _bibleService.GetVersesForMultipleVersions(versions, book, chapter);
                if (verses.Count == 0)
                {
                    return NotFound();
                }
                return Ok(verses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
