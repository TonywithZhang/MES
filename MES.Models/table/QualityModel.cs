

using FreeSql.DataAnnotations;

namespace MES.Models.table
{
    [Table]
    public class QualityModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string ProductNo { get; set; }
        public required string Batch {  get; set; }
        public required DateTime UpdateTime { get; set; }
        public required string BadReason { get; set; }
    }
}
