using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppiAuthors.Models
{
    public class AuthorDTO : Resource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
