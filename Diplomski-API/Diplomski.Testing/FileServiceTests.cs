using AutoMapper;
using Diplomski.Api.Controllers;
using Diplomski.Application.Dtos;
using Diplomski.Application.Exceptions;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Application.Services;
using Diplomski.Core.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace Diplomski.Testing
{
    public class FileServiceTests
    {
        private FileService _fileService;

        public FileServiceTests()
        {

        }

        [Fact]
        public async Task UploadFileReturnsFileDto()
        {
            //Arrange
            var storageService = new Mock<IStorageService>();
            var fileRepository = new Mock<IFileRepository>();
            var fileTypeRepository = new Mock<IFileTypeRepository>();
            var mapper = new Mock<IMapper>();
            var userRepository = new Mock<IUserRepository>();

            var userEntity = new User { Id = 15, FullName = "Ime" };
            userRepository.Setup(x => x.GetByIdAsync(15)).Returns(Task.FromResult(userEntity));//Ovde ne mora da se poklopi

            var fileDto = new FileDto { CreatedAt = DateTime.Now, FileName = "TestFile.txt", Path = "uploads/FileName.txt", Size = "354.354", Type = "text/" };
            //var file = new Mock<IFormFile>();
            //var content = "Ovo je fajl Slovca 354";
            //var ms = new MemoryStream();
            //var writer = new StreamWriter(ms);
            //writer.Write(content);
            //writer.Flush();
            //ms.Position = 0;
            //file.Setup(x => x.OpenReadStream()).Returns(ms);
            //file.Setup(x => x.FileName).Returns("FileName.txt");
            //file.Setup(x => x.Length).Returns(354354);

            storageService.Setup(x => x.UploadAsync(null, null)).Returns(Task.FromResult(fileDto));
            storageService.Setup(x => x.DeleteAsync(fileDto.Path));
            
            
            fileTypeRepository.Setup(x => x.GetByTypeAsync(fileDto.Type)).Returns(Task.FromResult(new FileType { Type = "text/"}));

            fileRepository.Setup(x => x.AddAsync(new File { FileName = fileDto.FileName, Size = fileDto.Size, Path = fileDto.Path, Owner = userEntity, Type = fileDto.Type }));


            _fileService = new FileService(storageService.Object, fileRepository.Object, fileTypeRepository.Object, mapper.Object, userRepository.Object);

            //Act
            var fileDtoFromFunction = await _fileService.UploadFile(new CreateFileDto { }, new UserDto { Id = 15 }); 

            //Assert
            Assert.NotNull(fileDtoFromFunction);
            Assert.Equal(fileDto, fileDtoFromFunction);
        }

        [Fact]
        public async Task UploadFileThrowsApiException()
        {
            //Arrange
            var storageService = new Mock<IStorageService>();
            var fileRepository = new Mock<IFileRepository>();
            var fileTypeRepository = new Mock<IFileTypeRepository>();
            var mapper = new Mock<IMapper>();
            var userRepository = new Mock<IUserRepository>();

            var userEntity = new User { Id = 15, FullName = "Ime" };
            userRepository.Setup(x => x.GetByIdAsync(15)).Returns(Task.FromResult(userEntity));

            var fileDto = new FileDto { CreatedAt = DateTime.Now, FileName = "TestFile.txt", Path = "uploads/FileName.txt", Size = "354.354", Type = "text/" };

            storageService.Setup(x => x.UploadAsync(null, null)).Returns(Task.FromResult(fileDto));
            storageService.Setup(x => x.DeleteAsync(fileDto.Path)).Returns(Task.CompletedTask);


            fileTypeRepository.Setup(x => x.GetByTypeAsync(fileDto.Type)).Returns(Task.FromResult<FileType>(null));

            fileRepository.Setup(x => x.AddAsync(new File { FileName = fileDto.FileName, Size = fileDto.Size, Path = fileDto.Path, Owner = userEntity, Type = fileDto.Type }));


            _fileService = new FileService(storageService.Object, fileRepository.Object, fileTypeRepository.Object, mapper.Object, userRepository.Object);


            //Act and Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => _fileService.UploadFile(new CreateFileDto { }, new UserDto { }));
            Console.WriteLine(exception.Message);
            Assert.Equal("Unable to upload file", exception.Message);//Udje u api does not support exception a posle u catch granu uhvati ovaj exception.
            Assert.Equal(500, exception.Code);
        }

        [Fact]
        public async Task DeleteFileReturnDeleteSuccessfully()
        {
            //Arrange
            var storageService = new Mock<IStorageService>();
            var fileRepository = new Mock<IFileRepository>();
            var fileTypeRepository = new Mock<IFileTypeRepository>();
            var mapper = new Mock<IMapper>();
            var userRepository = new Mock<IUserRepository>();

            var userEntity = new User { Id = 3, FullName = "Name" };
            var file = new File { Id = 3, Owner = userEntity, FileName = "File354.jpg", Path = "uploads/File354.jpg" };
            storageService.Setup(x => x.DeleteAsync(file.Path)).Returns(Task.CompletedTask);

            fileRepository.Setup(x => x.GetUserFileAsync(file.Id, 3)).Returns(Task.FromResult(file));
            fileRepository.Setup(x => x.DeleteAsync(file)).Returns(Task.CompletedTask);

            _fileService = new FileService(storageService.Object, fileRepository.Object, fileTypeRepository.Object, mapper.Object, userRepository.Object);

            //Act
            var result =  await _fileService.DeleteFile(file.Id, new UserDto { Id = userEntity.Id });//Mora da se poklapa Id koji se prosledi sa Id u GetUserFileAsync i samo ako se poklope onda vraca file koji mu je naveden u returns u funkciji GetUserFileAsync 

            //Assert
            Assert.NotNull(result);
            Assert.Equal("DeletedSuccessfully", result);
        }

        [Fact]
        public async Task DeleteFileThrowsApiException()
        {
            //Arrange
            var storageService = new Mock<IStorageService>();
            var fileRepository = new Mock<IFileRepository>();
            var fileTypeRepository = new Mock<IFileTypeRepository>();
            var mapper = new Mock<IMapper>();
            var userRepository = new Mock<IUserRepository>();

            var userEntity = new User { Id = 3, FullName = "Name" };
            var file = new File { Id = 3, Owner = userEntity, FileName = "File354.jpg", Path = "uploads/File354.jpg" };
            storageService.Setup(x => x.DeleteAsync(file.Path)).Returns(Task.CompletedTask);

            fileRepository.Setup(x => x.GetUserFileAsync(file.Id, userEntity.Id)).Returns(Task.FromResult<File>(null));
            fileRepository.Setup(x => x.DeleteAsync(file)).Returns(Task.CompletedTask);

            _fileService = new FileService(storageService.Object, fileRepository.Object, fileTypeRepository.Object, mapper.Object, userRepository.Object);

            //Act and Assert
            var exception = await Assert.ThrowsAsync<ApiException>(() => _fileService.DeleteFile(file.Id, new UserDto { Id = userEntity.Id }));

            Console.WriteLine(exception.Message);
            Assert.Equal("FileDoesNotExist", exception.Message);
            Assert.Equal(500, exception.Code);
        }
    }
}
