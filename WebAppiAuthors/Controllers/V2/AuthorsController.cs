using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebAppiAuthors.Contexts;
using WebAppiAuthors.Entities;
using WebAppiAuthors.Helpers;

namespace WebAppiAuthors.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    //[HttpHeaderIsPresent("x-version", "2")]
    public class AutoresController : ControllerBase
    {
        private ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "ObtenerAutoresV2")]
        [ServiceFilter(typeof(HATEOASAuthorsFilterAttribute))]
        public async Task<ActionResult<IEnumerable<Author>>> Get()
        {
            var autores = await context.Authors.ToListAsync();
            return autores;
        }
    }
}

