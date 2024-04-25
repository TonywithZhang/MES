using MES.Models.table;

namespace MES.DataTransaction.database
{
    public class DeviceService
    {
        #region 字段
        private static readonly IFreeSql sql = FreeSQLHelper.FreeSQL;
        #endregion

        #region 静态方法
        public static async Task<List<DeviceModel>> GetDevices()
        {
            try
            {
                return await sql.Select<DeviceModel>().Limit(100).ToListAsync();
            }
            catch (Exception)
            {

            }
            return [];
        }

        public static async Task<bool> InsertDevice(DeviceModel device)
        {
            try
            {
                return await sql.Insert(device).ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static async Task<bool> DeviceOnline(string id)
        {
            try
            {
                return await sql.Update<DeviceModel>()
                    .Set(d => d.Online,true)
                    .Set(d => d.OnLineTime, DateTime.Now)
                    .Set(d => d.UpdateTime, DateTime.Now)
                    .Where(d => d.Id == id)
                    .ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static async Task<bool> DeviceOff(string id)
        {
            try
            {
                return await sql.Update<DeviceModel>()
                    .Set(d => d.Online, false)
                    .Set(d => d.UpdateTime, DateTime.Now)
                    .Where(d => d.Id == id)
                    .ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }
        #endregion
    }
}
