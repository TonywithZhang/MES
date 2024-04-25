namespace MES.Models.attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        private string name = string.Empty;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public ColumnNameAttribute(string name)
        {
            this.name = name;
        }
    }
}
