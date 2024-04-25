using FreeSql.DataAnnotations;
using MES.Models.attribute;

namespace MES.Models.table
{
    [Table]
    public class ProjectModel
    {
        [ColumnName(name: "id")]
        [Column(CanUpdate = false, IsIdentity = true, IsPrimary = true)]
        public required string Id { get; set; }
        [ColumnName(name: "名称")]
        public required string Title { get; set; }
        [ColumnName(name: "起始时间")]
        public required DateTime StartTime { get; set; }
        [ColumnName(name: "结束时间")]
        public required DateTime EndTime { get; set; }
        [ColumnName(name: "负责人")]
        public required string Owner {  get; set; }
        [ColumnName(name: "员工编号")]
        public required string EmployeeNo {  get; set; }
    }
}
