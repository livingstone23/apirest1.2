using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppiAuthors.Models
{
    public class ResourceCollection<T>: Resource where T : Resource
    {
        public List<T> _values { get; set; }

        public ResourceCollection(List<T> values)
        {
            _values = values;
        }
    }
}
