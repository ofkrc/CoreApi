using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmaAPI.Core
{
    public class EntityBase 
    {
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string? Code {  get; set; }
        public bool? Deleted {  get; set; }
        public bool? Status {  get; set; }
        public int UserId {  get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }

    }
}
