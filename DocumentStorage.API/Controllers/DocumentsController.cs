using AutoMapper;
using DocumentStorage.API.Data;
using DocumentStorage.API.DTOs;
using DocumentStorage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentStorageDbContext _dbContext;
        private readonly IMapper _mapper;

        public DocumentsController(DocumentStorageDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var document = await _dbContext.Documents.SingleOrDefaultAsync(d => d.Id == id);

            if (document == null)
            {
                return BadRequest("No document found!");
            }

            var documentDto = _mapper.Map<DocumentDto>(document);

            return Ok(documentDto);
        }   

        [HttpPost]
        public async Task<IActionResult> Post(DocumentDto createDocumentDto)
        {
            var document = _mapper.Map<Document>(createDocumentDto);

            await _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();

            var documentDto = _mapper.Map<DocumentDto>(document);

            return CreatedAtAction(nameof(GetById), new { Id = document.Id }, documentDto);
        }
    }
}
