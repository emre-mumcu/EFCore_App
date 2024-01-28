#nullable disable

using EFCore_App.AppLib.Data.Entities;

namespace EFCore_App.AppLib.Data.Entities;

public class Mahalle
{
    public int MahalleId { get; set; }
    public string MahalleAdi { get; set; }
    public string PK { get; set; }

    // Reference navigation properties (Ones)
    public int SemtBucakBeldeId { get; set; }
    public SemtBucakBelde SemtBucakBelde { get; set; }
}