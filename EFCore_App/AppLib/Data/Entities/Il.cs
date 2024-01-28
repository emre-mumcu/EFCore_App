# nullable disable

using EFCore_App.AppLib.Data.Entities;

namespace EFCore_App.AppLib.Data.Entities
{
    public partial class Il
    {
        public int IlId { get; set; }
        public string IlAdi { get; set; }

        // Collection navigation properties (Manies)
        public virtual ICollection<Ilce> Ilceler { get; set; }
    }
}
