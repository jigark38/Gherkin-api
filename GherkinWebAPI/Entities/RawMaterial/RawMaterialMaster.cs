using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GherkinWebAPI.Entities
{
    public class RawMaterialMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public string Raw_Material_Group_Code { get; set; }

        public string Material_Purchases { get; set; }

        public string Raw_Material_Group { get; set; }

     }
}