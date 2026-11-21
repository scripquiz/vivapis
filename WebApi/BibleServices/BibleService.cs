using Microsoft.EntityFrameworkCore;

namespace ViviliamAPI.BibleServices
{
    public class BibleService : IBibleService
    {
        private readonly ViviliamDbContext _context;

        public BibleService(ViviliamDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bible>> GetVerses(string versionValue, short book, short chapter)
        {
            if (book > 73 || chapter > 150)
            {
                return new List<Bible>();
            }

            IQueryable<Bible> query = versionValue.ToUpper() switch
            {
                nameof(Version.BSI) => _context.BSI,
                nameof(Version.KJV) => _context.KJV,
                nameof(Version.AMP) => _context.AMP,
                _ => null
            };

            if (query == null)
            {
                return new List<Bible>();
            }

            try
            {
                return await query
                    .Where(v => v.Book == book && v.Chapter == chapter)
                    .Select(v => new Bible
                    {
                        Book = v.Book,
                        Chapter = v.Chapter,
                        Verse = v.Verse,
                        VerseText = v.VerseText
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, Bible[]>> GetVersesForMultipleVersions(string[] versionValues, short book, short chapter)
        {
            if (book > 73 || chapter > 150)
            {
                return new Dictionary<string, Bible[]>();
            }

            var result = new Dictionary<string, Bible[]>();

            foreach (var versionValue in versionValues)
            {
                IQueryable<Bible> query = versionValue.ToUpper() switch
                {
                    nameof(Version.BSI) => _context.BSI,
                    nameof(Version.KJV) => _context.KJV,
                    nameof(Version.AMP) => _context.AMP,
                    _ => null
                };

                if (query != null)
                {
                    try
                    {
                        var verses = await query
                            .Where(v => v.Book == book && v.Chapter == chapter)
                            .Select(v => new Bible
                            {
                                Book = v.Book,
                                Chapter = v.Chapter,
                                Verse = v.Verse,
                                VerseText = v.VerseText
                            })
                            .ToArrayAsync();

                        result[versionValue] = verses;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return result;
        }

    }
}
