using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppiAuthors.Models
{
    public class Link
    {
        public string _href { get; set; }
        public string _rel { get; set; }
        public string _method { get; set; }


        public Link(String href, string rel, string method)
        {
            _href = href;
            _rel = rel;
            _method = method;
        }


    }
}
