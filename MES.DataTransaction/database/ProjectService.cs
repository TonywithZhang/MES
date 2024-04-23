using MES.Models.table;

namespace MES.DataTransaction.database
{
    public class ProjectService
    {
        #region 私有字段
        private static IFreeSql sql = FreeSQLHelper.FreeSQL;
        #endregion

        #region 静态方法
        public static async Task<int> InsertProject(ProjectModel project)
        {
            try
            {
                return await sql.Insert(project).ExecuteAffrowsAsync();
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public static async Task<List<ProjectModel>> GetProjects()
        {
            try
            {
                return await sql.Select<ProjectModel>().Limit(100).ToListAsync();
            }
            catch (Exception)
            {

            }
            return [];
        }

        public static async Task<bool> UpdateOwner(string id, string owner, string num)
        {
            try
            {
                return await sql.Update<ProjectModel>()
                    .Set(p => p.Owner, owner)
                    .Set(p => p.EmployeeNo, num)
                    .Where(p => p.Id == id)
                    .ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static async Task<bool> InsertTask(TaskModel task)
        {
            try
            {
                return await sql.Insert(task).ExecuteAffrowsAsync() != 0;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public static async Task<IEnumerable<TaskModel>> GetTasks()
        {
            try
            {
                return await sql.Select<TaskModel>().Limit(100).ToListAsync();
            }
            catch (Exception)
            {

            }
            return [];
        }
        #endregion
    }
}
