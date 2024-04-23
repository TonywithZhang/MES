using FreeSql;

namespace MES.DataTransaction.database
{
    public static class FreeSQLHelper
    {
        public static IFreeSql FreeSQL = new FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, "data source=MES.db")
            .UseAutoSyncStructure(true)
            .Build();
    }
}
