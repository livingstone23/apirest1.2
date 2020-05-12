using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using WebAppiAuthors.Models;

namespace WebAppiAuthors.Helpers
{
    public class GeneradorEnlaces
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly IActionContextAccessor actionContextAccessor;

        public GeneradorEnlaces(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            this.urlHelperFactory = urlHelperFactory;
            this.actionContextAccessor = actionContextAccessor;
        }

        private IUrlHelper ConstruirURLHelper()
        {
            return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public ResourceCollection<AuthorDTO> GenerarEnlaces(List<AuthorDTO> autores)
        {
            var _urlHelper = ConstruirURLHelper();
            var resultado = new ResourceCollection<AuthorDTO>(autores);
            autores.ForEach(a => GenerarEnlaces(a));
            resultado.Links.Add(new Link(_urlHelper.Link("ObtenerAutores", new { }), rel: "self", method: "GET"));
            resultado.Links.Add(new Link(_urlHelper.Link("CrearAutor", new { }), rel: "crear-autor", method: "POST"));
            return resultado;
        }

        public void GenerarEnlaces(AuthorDTO autor)
        {
            var _urlHelper = ConstruirURLHelper();
            autor.Links.Add(new Link(_urlHelper.Link("ObtenerAutor", new { id = autor.Id }), rel: "self", method: "GET"));
            autor.Links.Add(new Link(_urlHelper.Link("ActualizarAutor", new { id = autor.Id }), rel: "actualizar-autor", method: "PUT"));
            autor.Links.Add(new Link(_urlHelper.Link("BorrarAutor", new { id = autor.Id }), rel: "borrar-autor", method: "DELETE"));
        }
    }
}
