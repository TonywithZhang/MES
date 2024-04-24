using MES.Models.table;

namespace MES.DataTransaction.database
{
    public class QualityService
    {
        #region 私有函数
        private static IFreeSql sql = FreeSQLHelper.FreeSQL;
        #endregion

        #region 静态函数
        public static async Task<bool> InsertQualityModel(QualityModel qualityModel)
        {
            try
            {
                return await sql.Insert(qualityModel).ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static async Task<List<QualityModel>> GetQualities()
        {
            try
            {
                return await sql.Select<QualityModel>().Limit(100).ToListAsync();
            }
            catch (Exception)
            {

            }
            return [];
        }
        #endregion
    }
}
