using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.IO;


namespace SaleSineDLl
{
    public class DALService
    {
        public T CopyRecord<T>(DataRow record, Type type)
        {
            //Get Properties
          
            var instance = Activator.CreateInstance(type);
            var properties = instance.GetType().GetProperties();
            var column = "";
            foreach (PropertyInfo property in properties)
            {
                
                //If property is generic list, continue
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) continue;

                try
                {
                   // CustomLog.MyLogger.info(property.Name);
                    //Check for dbnull, return null if true, or convert to correct type
                    var columnValue = Convert.IsDBNull(record[property.Name]) ? null : Convert.ChangeType(record[property.Name], Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

                    //Set property for newRecord
                    property.SetValue(instance, columnValue, null);
                }
                catch { }
            }
            return (T)instance; ;
        }

        public List<T> CopyRecords<T>(DataTable table, Type type)
            where T : new()
        {

            List<T> list = new List<T>();

            foreach (var record in table.Rows)
            {
                var o = CopyRecord<T>(record as DataRow, type);

                list.Add((T)o);
            }

            return list;
        }

        public StreamWriter CreateFile(string folder, string fileName)
        {
            if (!Directory.Exists(folder))
            {
                throw new Exception("Directory Not exist");
            }



            StreamWriter Writer = new StreamWriter(folder + @"\" + fileName, true);
            return Writer;
        }






    }
}