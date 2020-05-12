using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAppiAuthors.Contexts;
using WebAppiAuthors.Entities;
using WebAppiAuthors.Models;

namespace WebAppiAuthors.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUrlHelper _urlHelper;

        public AuthorsController(ApplicationDbContext context, ILogger<AuthorsController>  logger,
            IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            //_urlHelper = urlHelper;
        }


        [HttpGet(Name = "GetAuthors")]
        public async Task<IActionResult> Get(bool includeLinksHateos = false,int numberPage = 1, int numberRegister = 5 )
        {
            //Variables para el manejo de la paginacion
            var query = _context.Authors.AsQueryable();
            var totalRegister = query.Count();

            _logger.LogInformation("Obteniendo los autores");
            var captura = _configuration["UserAdmin"];
            var connectionString = _configuration["connectionStrings:DefaultConnection"];

            var authors = await query
                                .Skip(numberRegister * (numberPage -1))
                                .Take(numberRegister)
                                .ToListAsync();

            Response.Headers["Total_Registers"] = totalRegister.ToString();
            Response.Headers["Number_Page"] = ((int) Math.Ceiling((double) totalRegister / numberRegister)).ToString();

            var authorsDTO = _mapper.Map<List<AuthorDTO>>(authors);
            var result = new ResourceCollection<AuthorDTO>(authorsDTO);

            if (includeLinksHateos)
            {
                authorsDTO.ForEach(a => GenerateLinks(a));
                result.Links.Add(new Link(Url.Link("GetAuthors", new { }), rel: "self", method: "Get"));
                result.Links.Add(new Link(Url.Link("CreateAuthor", new { }), rel: "CreateAuthor", method: "PUT"));
                return Ok(result);
            }
            
            return Ok(authorsDTO);
        }

        [HttpGet("first", Name = "FirstAuthor")]
        public ActionResult<Author> GetFirstAuthor()
        {
            _logger.LogInformation("Obteniendo el primer Author");
            return _context.Authors.FirstOrDefault();
        }

        [HttpGet("{id}", Name="GetAuthor")]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            var authorDTO = _mapper.Map<AuthorDTO>(author);


            GenerateLinks(authorDTO);

            return authorDTO;
        }

        private void GenerateLinks(AuthorDTO authorDTO)
        {
            authorDTO.Links.Add(new Link(Url.Link("GetAuthor", new { id = authorDTO.Id }), rel: "self", method: "GET"));
            authorDTO.Links.Add(new Link(Url.Link("UpdateAuthor", new { id = authorDTO.Id }), rel: "update-author", method: "PUT"));
            authorDTO.Links.Add(new Link(Url.Link("DeleteAuthor", new { id = authorDTO.Id }), rel: "delete-author", method: "DELETE"));
        }


        [HttpPost(Name = "CreateAuthor")]
        public async Task<ActionResult> CreateAuthor([FromBody] AuthorCreationDTO authorCreation)
        {
            if (authorCreation == null)
            {
                return BadRequest();
            }

            var author = _mapper.Map<Author>(authorCreation);
            _context.Add(author);
            await _context.SaveChangesAsync();
            var authorDTO = _mapper.Map<AuthorDTO>(author);
            return new CreatedAtRouteResult("GetAuthor",new {id = author.Id}, authorDTO);
        }

        [HttpPut("{id}", Name = "UpdateAuthor")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] AuthorCreationDTO authorUpdate)
        {
            var author = _mapper.Map<Author>(authorUpdate);
            author.Id = id;
            _context.Entry(authorUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("{id}",Name = "PartialUpdateAuthor")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Author> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var authorDB = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (authorDB == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(authorDB);
            var isValid = TryValidateModel(patchDocument);

            if(!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteAuthor")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var authorId = await _context.Authors.Select(x=>x.Id).FirstOrDefaultAsync(x => x == id);
            if (authorId == null)
            {
                return NotFound();
            }

            _context.Remove(new Author{ Id = authorId });
            await _context.SaveChangesAsync();
            return NoContent();
        }




    }
}
