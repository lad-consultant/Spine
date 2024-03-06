using System.Reflection;

using TheSpine.Infrastructure.DataAccess.Repositories.Attributes;

namespace TheSpine.Infrastructure.DataAccess.Repositories
{
    public class TableNameResolver
    {
        private static readonly Dictionary<Type, string> _tableNames = new Dictionary<Type, string>();

        public string ResolveTableName(Type t)
        {
            if(_tableNames.TryGetValue(t, out var tableName))
            {
                return tableName;
            }

            return string.Empty;
        }

        public static void Init()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttributes(typeof(TableNameAttribute), true).Length > 0);
            foreach(var type in types)
            {
                var attribute = type.GetCustomAttribute<TableNameAttribute>();
                _tableNames.Add(type, attribute.TableName);
            }
        }
    }
}
