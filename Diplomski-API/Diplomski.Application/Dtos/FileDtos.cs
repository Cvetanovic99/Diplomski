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
}
