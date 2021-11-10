using Diplomski.Application.Dtos;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplomski.Api.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : Controller
    {
        private readonly IStorageService _storageService;
        public FileController(IStorageService storageService)
        {
            this._storageService = storageService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] CreateFileDto createFileDto)
        {
            var path = await _storageService.UploadAsync(createFileDto.File);
            return Ok(path);
        }
    }
}
