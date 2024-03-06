using System.ComponentModel.DataAnnotations;

namespace TheSpine.Core.Common
{
    public class IdentifiableEntity
    {
        [Key]
        public int Id { get; set; }
        //public int CreatedBy { get; set; }
        //public int UpdatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime UpdatedOn { get; set; }
    }
}
