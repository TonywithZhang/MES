using FreeSql.DataAnnotations;
using MES.Models.attribute;

namespace MES.Models.table
{
    [Table]
    public class DeviceModel
    {
        [ColumnName(name: "id")]
        [Column(IsPrimary = true, IsIdentity = true)]
        public required string Id { get; set; }
        [ColumnName(name: "名称")]
        public required string Name { get; set; }
        [ColumnName(name: "描述")]
        public required string Description { get; set; }
        [ColumnName(name: "是否在线")]
        public required bool Online { get; set; }
        [ColumnName(name: "设备位置")]
        public required string Location { get; set; }
        [ColumnName(name: "更新时间")]
        public required DateTime UpdateTime { get; set; }
        [ColumnName(name: "上线时间")]
        public required DateTime OnLineTime { get; set; }
        [ColumnName(name: "首次上线时间")]
        public required DateTime FirstTime { get; set; }
    }
}
