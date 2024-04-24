using MES.Models.project;
using MES.Models.table;

namespace MES.DataTransaction.bean
{
    public class RandomModelAssembler
    {
        #region 私有字段
        private static Random random = new Random();
        private static string[] names = {
            "张三",
            "李四",
            "王五",
            "赵六",
            "巩宕",
            "秘雅歌",
            "聊傲旋",
            "香君丽",
            "潜晗蕾",
            "修旷",
            "力康裕",
            "夷春枫",
            "刀冰凡",
            "盖景曜",
            "宾敏丽",
            "宇刚豪",
            "苌听春",
            "门君豪",
            "葛学智",
            "贯醉冬",
            "仵曼容",
            "五书萱",
            "涂莘",
            "汗志尚",
            "侯运乾",
            "树曼文",
            "操平雅",
            "管琛丽",
            "秋斯",
            "母念柏"
        };
        private static string[] projects =
        {
            "出图纸",
            "画模型",
            "车螺纹",
            "NC加工",
            "线切割",
            "焊接",
            "铆接",
            "激光切割"
        };
        #endregion
        #region 静态方法
        public static ProjectModel randomProjectModel()
        {
            return new ProjectModel
            {
                Id = Guid.NewGuid().ToString(),
                Title = RandomTitle(),
                Owner = RandomName(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(1),
                EmployeeNo = RandomEmployeeNo()
            };
        }

        public static TaskModel RandomDispatcher()
        {
            return new TaskModel { EmployeeNo = RandomEmployeeNo(), Id = Guid.NewGuid().ToString(), Name = RandomName(), TaskName = RandomTitle(), Time = DateTime.Now };
        }

        public static MonitorModel RandomMonitorModel()
        {
            return new MonitorModel($"{RandomName()}检查了{RandomTitle()}的生产进度", DateTime.Now);
        }

        private static string RandomTitle() => $"PJ{(random.NextInt64() % 1e7)}{projects[random.Next() % projects.Length]}";
        private static string RandomName() => names[random.Next() % names.Length];
        private static string RandomEmployeeNo() => $"No {random.Next()}";
        #endregion
    }
}
