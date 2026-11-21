public interface IBibleService
{
    Task<List<Bible>> GetVerses(string versionValue, short book, short chapter);
    //added for lalitha's project
    Task<Dictionary<string, Bible[]>> GetVersesForMultipleVersions(string[] versionValues, short book, short chapter);
}
