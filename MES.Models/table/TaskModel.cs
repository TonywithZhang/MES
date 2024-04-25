using FreeSql.DataAnnotations;
using MES.Models.attribute;

namespace MES.Models.table
{
    [Table]
    public class TaskModel
    {
        [ColumnName(name: "id")]
        [Column(IsPrimary = true, IsIdentity = true)]
        public required string Id { get; set; }
        [ColumnName(name: "名字")]
        public required string Name { get; set; }
        [ColumnName(name: "任务名称")]
        public required string TaskName {  get; set; }
        [ColumnName(name: "员工编号")]
        public required string EmployeeNo {  get; set; }
        [ColumnName(name: "时间")]
        public required DateTime Time {  get; set; }
    }
}
