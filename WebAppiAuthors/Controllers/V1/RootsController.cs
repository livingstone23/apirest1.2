using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppiAuthors.Models;

namespace WebAppiAuthors.Controllers.v1
{
    [Route("api/v1")]
    [ApiController]
    public class RootsController : ControllerBase
    {
        //private readonly IUrlHelper _urlHelper;



        [HttpGet(Name="GetRoot")]
        public ActionResult<IEnumerable<Link>> Get()
        {
            List<Link> links = new List<Link>();

            //Aqui colocamos los links
            links.Add(new Link(href: Url.Link("GetRoot", new { }), rel: "self", method: "GET"));
            links.Add(new Link(href: Url.Link("GetAuthors", new {}), rel: "authors", method:"GET"));
            links.Add(new Link(href: Url.Link("CreateAuthor", new { }), rel: "authors", method: "POST"));
            links.Add(new Link(href: Url.Link("FirstAuthor", new { }), rel: "authors", method: "GET"));
           

            return links;
        }

    }
}