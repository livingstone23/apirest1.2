using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAppiAuthors.Helpers;

namespace WebAppiAuthors.Entities
{
    public class Author//: IValidatableObject
    {
        public static object Links { get; internal set; }

        //public int Id { get; set; }

        //[PrimeraLetraMayuscula]
        //[Required]
        //[StringLength(20,MinimumLength = 12, ErrorMessage = "The name at least must have {1} characteres")]
        //public string Name { get; set; }

        //[Range(18,120)]
        //public int Age { get; set; }

        //[CreditCard]
        //public string CreditCard { get; set; }

        //[Url]
        //public string Url { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        var primeraLetra = Name[0].ToString();

        //        if (primeraLetra != primeraLetra.ToUpper())
        //        {
        //            yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Name) });
        //        }
        //    }
        //}


        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public DateTime Birthday { get; set; }
        public List<Book> Books { get; set; }
    }
}
