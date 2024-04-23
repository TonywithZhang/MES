using FreeSql.DataAnnotations;

namespace MES.Models.table
{
    [Table]
    public class UserModel
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required int Right { get; set; }
    }
}
