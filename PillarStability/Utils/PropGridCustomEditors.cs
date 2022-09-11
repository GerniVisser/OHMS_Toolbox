using PillarStability.DataObjects;
using Syncfusion.Windows.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PillarStability.Utils
{
    class PropGridCustomEditors
    {
    }

    public class ComboEditor : ITypeEditor
    {
        public void Attach(PropertyViewItem property, PropertyItem info)
        {
            if (info.CanWrite)
            {
                var binding = new Binding("Value")
                {
                    Mode = BindingMode.TwoWay,
                    Source = info,
                    ValidatesOnExceptions = true,
                    ValidatesOnDataErrors = true
                };
                BindingOperations.SetBinding(comboEdit, ComboBox.TextProperty, binding);
            }
            else
            {
                var binding = new Binding("Value")
                {
                    Mode = BindingMode.OneWayToSource,
                    //Source = info, < br >
                    //ValidatesOnExceptions = true,
                    ValidatesOnDataErrors = true
                };
                BindingOperations.SetBinding(comboEdit, ComboBox.TextProperty, binding);
            }
        }
        public ComboBox comboEdit;
        public object Create(System.Reflection.PropertyInfo PropertyInfo)
        {
            comboEdit = new ComboBox();

            var items = new PillarStrengthOptions();

            foreach (var item in items.Options)
            {
                comboEdit.Items.Add(item);
            }

            comboEdit.SelectedIndex = 1;

            return comboEdit;
        }
        public void Detach(PropertyViewItem property)
        {
        }
    }
}
