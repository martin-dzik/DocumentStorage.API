using AutoMapper;
using DocumentStorage.API.Contracts;
using DocumentStorage.API.Controllers;
using DocumentStorage.API.DTOs;
using DocumentStorage.API.Helpers;
using DocumentStorage.API.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DocumentStorage.API.Tests.Controllers
{
    public class DocumentsControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsControllerTests()
        {
            _documentsRepository = A.Fake<IDocumentsRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async Task DocumentsController_GetAll_ReturnsOk()
        {
            // Arrange
            var documents = A.Fake<IList<Document>>();
            var documentDtos = A.Fake<List<DocumentDto>>();
            
            A.CallTo(() => _mapper.Map<List<DocumentDto>>(documents)).Returns(documentDtos);

            var controller = new DocumentsController(_mapper, _documentsRepository);

            // Act
            var result = await controller.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));  
        }

        [Fact]
        public async Task DocumentsController_Post_ReturnsCreatedAtAction()
        {        
            // Arrange
            var createDocumentDto = A.Fake<CreateDocumentDto>();
            createDocumentDto.Tags = A.Fake<ICollection<string>>();
            var document = A.Fake<Document>();
            var documentDto = A.Fake<DocumentDto>();
            var dbTags = A.Fake<IList<Tag>>();

            A.CallTo(() => _mapper.Map<Document>(createDocumentDto)).Returns(document);
            A.CallTo(() => _documentsRepository.GetTagsByNamesAsync(createDocumentDto.Tags!.ToList())).Returns(Task.FromResult(dbTags));
            
            A.CallTo(() => _documentsRepository.AddAsync(document)).Returns(Task.FromResult(document));
            A.CallTo(() => _mapper.Map<DocumentDto>(document)).Returns(documentDto);

            var controller = new DocumentsController(_mapper, _documentsRepository);

            // Act
            var result = await controller.Post(createDocumentDto);
            var createdAtActionResult = result as CreatedAtActionResult; 

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtActionResult>();

            createdAtActionResult!.Value.Should().BeAssignableTo<DocumentDto>();
        }
    }
}
