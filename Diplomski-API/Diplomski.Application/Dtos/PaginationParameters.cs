using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Dtos
{
    public class PaginationParameters
    {
        public int From { get; set; } = 0;
        public int To { get; set; } = 5;
        public string FileType { get; set; }
    }
}
