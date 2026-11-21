using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Bible
{
    [Key, Column(Order = 0)]
    public short Book { get; set; }

    [Key, Column(Order = 1)]
    public short Chapter { get; set; }

    [Key, Column(Order = 2)]
    public short Verse { get; set; }

    [Required]
    [StringLength(600)]
    public string VerseText { get; set; }

}

public class BsiBible : Bible
{
}

public class KjvBible : Bible
{
}

public class AmpBible : Bible
{
}

