using Diplomski.Application.Dtos;
using Diplomski.Application.Interfaces;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Diplomski.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    [Authorize]
    public class FileController : Controller
    {
        private readonly IStorageService _storageService;
        private readonly IAccountService _accountService;
        private readonly IFileService _fileService;
        public FileController(IStorageService storageService, IAccountService accountService, IFileService fileService)
        {
            this._storageService = storageService;
            this._accountService = accountService;
            this._fileService = fileService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] CreateFileDto createFileDto)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            var user = await _accountService.GetAuthenticatedUserAsync(token);

            var file = await _fileService.UploadFile(createFileDto, user);
            //var path = await _storageService.UploadAsync(createFileDto.File);


            return Ok(file);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFiles([FromQuery] PaginationParameters paginationParameters)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            var user = await _accountService.GetAuthenticatedUserAsync(token);

            var userFiles = await _fileService.GetUserFilesAsync(user, paginationParameters);
            return Ok(userFiles);
        }

        [HttpGet]
        [Route("allowedFileTypes")]
        public async Task<IActionResult> GetAllowedFileTypes()
        {
            var fileTypes = await _fileService.GetAllowedFileTypesAsync();
            return Ok(fileTypes);
        }

        [HttpDelete]
        [Route("deleteFile/{Id}")]
        public async Task<IActionResult> DeleteFile([FromQuery] int Id)
        {
            HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            var user = await _accountService.GetAuthenticatedUserAsync(token);
            
            var response = await _fileService.DeleteFile(Id, user);
            return Ok(response);
        }
    }
}
