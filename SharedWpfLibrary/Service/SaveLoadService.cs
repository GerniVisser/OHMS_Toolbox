using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SharedWpfLibrary.Service
{
    public static class SaveLoadService
    {
        public static void SaveListToCsv<T>(List<T> list, string filePath)
        {
            using (var writer = File.CreateText(filePath))
            {
                if (list.Count == 0)
                    return;
                // get properties of the first object
                Type type = list[0].GetType();
                PropertyInfo[] props = type.GetProperties();

                // write headers
                for (int i = 0; i < props.Length; i++)
                {
                    writer.Write(props[i].Name);
                    if (i < props.Length - 1)
                    {
                        writer.Write(",");
                    }
                }
                writer.WriteLine();

                // write data
                foreach (var item in list)
                {
                    foreach (var prop in props)
                    {
                        writer.Write(prop.GetValue(item, null).ToString().Replace(',','.'));
                        if(prop != props[props.Length - 1])
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }

        public static List<T> LoadFromCsv<T>(string filePath) where T : new()
        {
            List<T> result = new List<T>();
            using (var reader = File.OpenText(filePath))
            {
                // read headers
                var headers = reader.ReadLine().Split(',');

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var obj = new T();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        var prop = obj.GetType().GetProperty(headers[i]);
                        if (prop != null && prop.CanWrite)
                        {
                            object value;
                            if (prop.PropertyType == typeof(Single))
                            {
                                value = float.Parse(values[i], CultureInfo.InvariantCulture);
                            }
                            else if (prop.PropertyType == typeof(Brush))
                            {
                                value = BrushService.getBrushFromHex(values[i]);
                            }
                            else
                            {
                                value = Convert.ChangeType(values[i], prop.PropertyType);
                            }
                            prop.SetValue(obj, value);
                        }
                    }
                    result.Add(obj);
                }
            }
            return result;
        }

    }
}
