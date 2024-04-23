using MES.Models.table;

namespace MES.DataTransaction.database
{
    public class UserService
    {
        #region 私有字段
        private readonly static IFreeSql sql = FreeSQLHelper.FreeSQL;
        #endregion

        #region 静态方法
        public static async Task<UserModel?> GetUserByName(string username)
        {
            try
            {
                return await sql.Select<UserModel>().Where(u => u.Name == username).FirstAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<bool> InsertUser(UserModel model)
        {
            try
            {
                return await sql.Insert(model).ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }
        #endregion
    }
}
