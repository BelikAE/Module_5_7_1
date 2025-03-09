using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_5_7_1
{
    public class ViewWrapper
    {
        public ViewWrapper(View view)
        {
            Views = view;
            Name = view.Name;
        }

        public bool IsSelected { get; set; }

        public View Views { get; }
        public string Name { get; }

    }
}
