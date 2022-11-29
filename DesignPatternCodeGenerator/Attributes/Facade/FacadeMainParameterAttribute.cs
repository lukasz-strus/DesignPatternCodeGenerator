using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Attributes.Facade
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class FacadeMainParameterAttribute : Attribute
    {
        public string MainParameterName { get; set; }

        public FacadeMainParameterAttribute(string mainParameterName)
        {
            MainParameterName = mainParameterName;
        }
    }
}
