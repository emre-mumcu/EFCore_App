#nullable disable

using EFCore_App.AppLib.Data.Entities;

namespace EFCore_App.AppLib.Data.Entities
{
    public partial class Ilce
    {
        public int IlceId { get; set; }
        public string IlceAdi { get; set; }

        // Collection navigation properties (Manies)
        public virtual ICollection<SemtBucakBelde> SemtBucakBeldeler { get; set; }

        // Reference navigation properties (Ones)
        public int IlId { get; set; }
        public Il Il { get; set; }
    }
}
