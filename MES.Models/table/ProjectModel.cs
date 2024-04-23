using FreeSql.DataAnnotations;

namespace MES.Models.table
{
    [Table]
    public class ProjectModel
    {
        [Column(CanUpdate = false, IsIdentity = true, IsPrimary = true)]
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required string Owner {  get; set; }
        public required string EmployeeNo {  get; set; }
    }
}
