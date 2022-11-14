using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Factory
{
    [Factory]
    public interface ICar
    { 
        public string Name { get; set; }
        public int HorsePower { get; set; }

    }

    [FactoryChild]
    class Bmw : ICar
    {  
        public string Name { get; set; }
        public int HorsePower { get; set; }

        public Bmw(string name, int horsePower)
        {
            Name = name;
            HorsePower = horsePower;
        }
    }

    [FactoryChild]
    partial class Audi : ICar
    {
        public string Name { get; set; }
        public int HorsePower { get; set; }

        public Audi(string name, int horsePower)
        {
            Name = name;
            HorsePower = horsePower;
        }
    }


}
