using System;
using System.Collections.Generic;
using System.Text;

namespace Diplomski.Core.Entities
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string  Size { get; set; }
        public string Path { get; set; }
        public User Owner { get; set; }
    }
}
