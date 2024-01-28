#nullable disable

using EFCore_App.AppLib.Data.Entities;

namespace EFCore_App.AppLib.Data.Entities
{
    public partial class SemtBucakBelde
    {
        public int SemtBucakBeldeId { get; set; }
        public string SemtBucakBeldeAdi { get; set; }

        // Collection navigation properties (Manies)
        public virtual ICollection<Mahalle> Mahalleler { get; set; }

        // Reference navigation properties (Ones)
        public int IlceId { get; set; }
        public Ilce Ilce { get; set; }
    }
}
