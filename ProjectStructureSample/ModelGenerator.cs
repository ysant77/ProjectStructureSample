using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStructureSample
{
    public static class ModelGenerator
    {
        public static object GetClass(string className, Dictionary<string, Type> properties)
        {
            var classBuilder = new ClassBuilder(className);
            return classBuilder.CreateObject(properties.Keys.ToArray(), properties.Values.ToArray());
        }
        
    }
}
