using System;

namespace Persistencia.Utils
{
    public class QueryBuilder
    {

        public static string FindById(String table, int idValue)
        {
            return String.Format("SELECT * from dbo.{0} where Id = {1}", table, idValue);
        }

        public static string DeleteById(String table, int idValue)
        {
            return String.Format("DELETE from dbo.{0} where Id = {1}", table, idValue);
        }

        public static string DeleteByAttributeInt(String table, String attributeName, int attributeValue)
        {
            return String.Format("DELETE from dbo.{0} where {1}={2}", table, attributeName, attributeValue);
        }

        public static string DeleteByCompundId(String table, String idName1, int idValue1, String idName2, int idValue2)
        {
            return String.Format("DELETE from dbo.{0} where {1} = {2} AND {3} = {4}", table, idName1, idValue1, idName2, idValue2);
        }
        public static string FindAll(String table)
        {
            return String.Format("SELECT * FROM dbo.{0}", table);
        }

        public static string FindByAttributeInt(String table, String attributeName, int attributeValue)
        {
            return String.Format("SELECT * from dbo.{0} where {1} = {2}", table, attributeName, attributeValue);
        }

        public static string FindByAttributeString(String table, String attributeName, string attributeValue)
        {
            return String.Format("SELECT * from dbo.{0} where {1} = '{2}'", table, attributeName, attributeValue);
        }

        public static string IdentityInsertWrapParameters(string table, string query)
        {
            return String.Format("SET IDENTITY_INSERT {0} ON {1} SET IDENTITY_INSERT {2} OFF", table, query, table);
        }

    }
}
