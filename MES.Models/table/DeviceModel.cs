using FreeSql.DataAnnotations;

namespace MES.Models.table
{
    [Table]
    public class DeviceModel
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool Online { get; set; }
        public required string Location { get; set; }
        public required DateTime UpdateTime { get; set; }
        public required DateTime OnLineTime { get; set; }
        public required DateTime FirstTime { get; set; }
    }
}
