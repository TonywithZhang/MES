using FreeSql.DataAnnotations;

namespace MES.Models.table
{
    [Table]
    public class TaskModel
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string TaskName {  get; set; }
        public required string EmployeeNo {  get; set; }
        public required DateTime Time {  get; set; }
    }
}
