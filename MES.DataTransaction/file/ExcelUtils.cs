using MES.Models.attribute;
using System.Text;

namespace MES.DataTransaction.file
{
    public class ExcelUtils
    {
        public static async Task ExportTableData<T>(IList<T> list, string name, string path) where T : class
        {
            var now = DateTime.Now;
            var fileDate = now.ToString("yyyy-MM-dd HH-mm-ss");

            var t = typeof(T);
            var properties = t.GetProperties();
            var headers = string.Join(
                ',',
                properties
                .ToList()
                .Select(d =>
                ((ColumnNameAttribute)d.GetCustomAttributes(
                    typeof(ColumnNameAttribute),
                    false)[0])
                    .Name));

            var data = string.Join(
                "\r\n",
                list.Select(
                    d =>
                    string.Join(
                        ',',
                        properties
                        .ToList()
                        .Select(p => p.GetValue(d)!.ToString()))));

            var fileName = $"{path}{fileDate}{name}.csv";

            await File.WriteAllTextAsync(fileName, headers + "\r\n" + data,Encoding.UTF8);
        }
    }
}
