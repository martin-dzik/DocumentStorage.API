using AutoMapper;
using DocumentStorage.API.Contracts;
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
        private readonly IMapper _mapper;
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsController(IMapper mapper, IDocumentsRepository documentsRepository)
        {
            _mapper = mapper;
            _documentsRepository = documentsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents = await _documentsRepository.GetAllAsync();
            var documentDtos = _mapper.Map<List<DocumentDto>>(documents);

            return Ok(documentDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // var document = await _dbContext.Documents.SingleOrDefaultAsync(d => d.Id == id);
            var document = await _documentsRepository.GetAsync(id);

            if (document is null)
            {
                return BadRequest("No document found!");
            }

            var documentDto = _mapper.Map<DocumentDto>(document);

            return Ok(documentDto);
        }   

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDocumentDto createDocumentDto)
        {
            var document = _mapper.Map<Document>(createDocumentDto);

            await _documentsRepository.AddAsync(document);

            var documentDto = _mapper.Map<DocumentDto>(document);

            return CreatedAtAction(nameof(GetById), new { Id = document.Id }, documentDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] DocumentDto documentDto)
        {
            if (id != documentDto.Id)
            {
                return BadRequest("Invalid document ID");
            }

            var document = await _documentsRepository.GetAsync(id);

            if (document is null)
            {
                return NotFound();
            }

            _mapper.Map(documentDto, document);

            try
            {
                await _documentsRepository.Update(document);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict();
            }

            return NoContent();
        }
    }
}
