using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Dtos
{
    public class CreateFileDto
    {
        public IFormFile File { get; set; }
    }

    public class FileDto
    {
        public string FileName  { get; set; }
        public string Size { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
