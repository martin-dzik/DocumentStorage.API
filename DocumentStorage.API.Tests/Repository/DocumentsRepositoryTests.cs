
using DocumentStorage.API.Data;
using DocumentStorage.API.DTOs;
using DocumentStorage.API.Models;
using DocumentStorage.API.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocumentStorage.API.Tests.Repository
{
    public class DocumentsRepositoryTests
    {
        private Document testDocument = new Document
        {
            Id = 0,
            Data =
            """
            {
                "name": "myTestDoc",
                "text":"Test"
            }
            """,
        };

        private static Tag testTag = new Tag { Name = "testTag" };

        private Document testDocumentWithTags = new Document
        {
            Id = 0,
            Data =
            """
            {
                "name": "myDoc",
                "text":"Some my text"
            }
            """,
            Tags = new Collection<Tag>() { testTag }
        };

        private async Task<DocumentStorageDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DocumentStorageDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _dbContext = new DocumentStorageDbContext(options);

            await _dbContext.Database.EnsureCreatedAsync();

            if (await _dbContext.Documents.CountAsync() <= 0)
            {
                var tags = new List<Tag>
                {
                    new Tag { Id = 1, Name = "HTML" },
                    new Tag { Id = 2, Name = "CSS" },
                };

                for (int i = 1; i < 10; i++)
                {
                    var tag = new Tag { Id = i + 2, Name = $"SOME{i}" };

                    tags.Add(tag);
                };

                await _dbContext.Tags.AddRangeAsync(tags);

                for (int i = 0; i < 9; i++)
                {
                    var document = new Document
                    {
                        Id = 0,
                        Data =
                           """
                            {
                                "name": "myDoc",
                                "text":"Some my text"
                            }
                            """,
                        Tags = new Collection<Tag>
                        {
                            tags[0], tags[1], tags[i + 2]
                        }
                    };

                    _dbContext.Documents.Add(document);
                }

                await _dbContext.SaveChangesAsync();
            }

            return _dbContext;
        }

        [Fact]
        public async Task DocumentsRepository_GetAllWithTagsAsync_ReturnsAllDocumentsWithTags()
        {
            // Arrange
            var _dbContext = await GetDatabaseContext();
            var _documentsRepository = new DocumentsRepository(_dbContext);

            // Act
            var result = await _documentsRepository.GetAllAsync();

            // Assert
            result.Should().BeAssignableTo<IList<Document>>();
        }

        [Fact]
        public async Task DocumentsRepository_GetAllAsync_ReturnsAllDocumentsWithoutTags()
        {
            // Arrange
            var _dbContext = await GetDatabaseContext();
            var _documentsRepository = new DocumentsRepository(_dbContext);

            // Act
            var result = await _documentsRepository.GetAllAsync();

            // Assert
            result.Should().BeAssignableTo<IList<Document>>();

            foreach (var document in result)
            {
                document.Tags.Should().BeNull();
            }
        }

        [Fact]
        public async Task DocumentsRepository_GetWithTagsByIdAsync_ReturnsDocumentWithTags()
        {
            // Arrange
            var _dbContext = await GetDatabaseContext();
            var _documentsRepository = new DocumentsRepository(_dbContext);
            var id = 2;

            // Act
            var result = await _documentsRepository.GetWithTagsByIdAsync(id);

            // Assert
            result.Should().BeOfType<Document>();
            result.Id.Should().Be(id);
            result.Tags.Should().NotBeNull();
        }

        [Fact]
        public async Task DocumentsRepository_AddAsync_ReturnsDocumentWithoutTags()
        {
            // Arrange
            var _dbContext = await GetDatabaseContext();
            var _documentsRepository = new DocumentsRepository(_dbContext);
            var document = testDocument;

            // Act
            var result = await _documentsRepository.AddAsync(document);

            // Assert
            result.Should().BeOfType<Document>();
            result.Tags.Should().BeNull();
        }

        [Fact]
        public async Task DocumentsRepository_AddAsync_ReturnsDocumentWithTags()
        {
            // Arrange
            var _dbContext = await GetDatabaseContext();
            var _documentsRepository = new DocumentsRepository(_dbContext);
            var documentWithTags = testDocumentWithTags;

            // Act
            var result = await _documentsRepository.AddAsync(documentWithTags);

            // Assert
            result.Should().BeOfType<Document>();
            result.Tags.Should().NotBeNull();
        }

        [Fact]
        public async Task DocumentsRepository_Update_WillUpdateDocument()
        {
            // Arrange
            var _dbContext = await GetDatabaseContext();
            var _documentsRepository = new DocumentsRepository(_dbContext);
            var document = new Document
            {
                Id = 1,
                Data = 
                """
                {
                    "name": "myUpdateDoc",
                    "text":"Some updated text"
                }
                """,
                Tags = new List<Tag>()
            };

            // Act
            var doc = await _documentsRepository.GetWithTagsByIdAsync(document.Id);
            doc.Data = document.Data;
            doc.Tags = document.Tags;

            await _documentsRepository.Update(doc);
            var result = await _documentsRepository.GetWithTagsByIdAsNoTrackingAsync(document.Id);

            // Assert
            result.Should().BeOfType<Document>();
            result.Tags.Should().BeNullOrEmpty();
            result.Data.Should().Be(document.Data);

        }
    }
}
