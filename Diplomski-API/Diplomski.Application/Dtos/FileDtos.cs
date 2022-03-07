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
        public int Id { get; set; }
        public string FileName  { get; set; }
        public string Size { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FileExtradataDto
    {
        public string FileType { get; set; }
        public ICollection<FileDto> Files { get; set; }
        public int Count { set; get; }
    }
}
