using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryBuilder.Models;

using Microsoft.Data.Sqlite;
using System.Reflection;

//microsoft.data.sqllite
namespace QueryBuilder
{
    internal class QueryBuilder : IDisposable
    {
        private SqliteConnection connection;


        public QueryBuilder(string databaseLocation)
        {
            connection = new SqliteConnection("Data Source=" + databaseLocation);
           
            connection.Open();
        }


       

        public List<T> ReadAll<T>() where T : IClassModel, new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            var datas = new List<T>();
            while (reader.Read())
            {
                data = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
                datas.Add(data);
            }
            return datas;
        }
        public void Create<T>(T obj)
        {

            PropertyInfo[] properties = typeof(T).GetProperties();


            List<string> values = new List<string>();
            List<string> names = new List<string>();
            PropertyInfo property;
            for (int i = 1; i < properties.Length; i++)
            {
                property = properties[i];

                if (property.PropertyType == typeof(DateTime))
                {
                    values.Add("\"" + ((DateTime)property.GetValue(obj)).Year + "-" + ((DateTime)property.GetValue(obj)).Month + "-" + ((DateTime)property.GetValue(obj)).Day + "\"");
                }

                else if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                }

                else
                {
                    values.Add(property.GetValue(obj).ToString());
                }
                names.Add(property.Name);
            }


            StringBuilder sb = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();
            for (int i = 0; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                {
                    sb.Append($"{values[i]}");
                    sbNames.Append(names[i]);
                }
                else
                {
                    sb.Append($"{values[i]}, ");
                    sbNames.Append($"{names[i]}, ");
                }

            }

            var command = connection.CreateCommand();

            command.CommandText = $"insert into {typeof(T).Name} ({sbNames}) values ({sb})";

            var reader = command.ExecuteNonQuery();
        }


        public T Read<T>(int id) where T : new()
        {
            var command = connection.CreateCommand();

            command.CommandText = $"select * from {typeof(T).Name} where Id = {id}";

            var reader = command.ExecuteReader();

            T data = new T();

            while (reader.Read())
            {
                ;
                for (int i = 0; i < reader.FieldCount; i++)
                {

                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }


                    else if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(DateTime) && reader.GetValue(i).ToString().Split('-').Length == 3)
                    {
                        string[] date = reader.GetValue(i).ToString().Split('-');
                        int[] dateNum = new int[3];
                        for (int l = 0; l < 3; l++)
                        {
                            dateNum[l] = Convert.ToInt32(date[l]);
                        }
                        var dateTime = new DateTime(dateNum[0], dateNum[1], dateNum[2]);
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, dateTime);
                    }


                    else
                    {
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                    }
                }
            }
            return data;



        }
        public void Update<T>(T obj) where T : IClassModel
        {
            
            PropertyInfo[] properties = typeof(T).GetProperties();

            List<string> values = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                
                if (property.PropertyType == typeof(DateTime))
                {
                    values.Add("\"" + ((DateTime)property.GetValue(obj)).Year + "-" + ((DateTime)property.GetValue(obj)).Month + "-" + ((DateTime)property.GetValue(obj)).Day + "\"");
                }
                
                else if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj).ToString() + "\"");
                }
                
                else
                {
                    values.Add(property.GetValue(obj).ToString());
                }
            }

            
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                {
                    sb.Append($"{properties[i].Name} = {values[i]}");
                }
                else
                {
                    sb.Append($"{properties[i].Name} = {values[i]}, ");
                }
            }

            var command = connection.CreateCommand();

            command.CommandText = $"update {typeof(T).Name} set {sb} where Id = {obj.Id}";
            var reader = command.ExecuteNonQuery();
        }


        public void Delete<T>(T obj) where T : IClassModel
        {
            var command = connection.CreateCommand();

            command.CommandText = $"delete from {typeof(T).Name} where Id = {obj.Id}";
            var reader = command.ExecuteNonQuery();
        }
        
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

