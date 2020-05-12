using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAppiAuthors.Helpers
{
    public class MiFiltroDeExcepcion : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

        }
    }
}
