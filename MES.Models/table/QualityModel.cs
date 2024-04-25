using FreeSql.DataAnnotations;
using MES.Models.attribute;

namespace MES.Models.table
{
    [Table]
    public class QualityModel
    {
        [ColumnName(name:"id")]
        [Column(IsIdentity = true, IsPrimary = true)]
        public required string Id { get; set; }
        [ColumnName(name: "名称")]
        public required string Name { get; set; }
        [ColumnName(name: "产品编号")]
        public required string ProductNo { get; set; }
        [ColumnName(name: "产品批次")]
        public required string Batch {  get; set; }
        [ColumnName(name: "更新时间")]
        public required DateTime UpdateTime { get; set; }
        [ColumnName(name: "返厂原因")]
        public required string BadReason { get; set; }
    }
}
