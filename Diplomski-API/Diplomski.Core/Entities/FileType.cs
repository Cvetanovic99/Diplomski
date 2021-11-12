using System;
using System.Collections.Generic;
using System.Text;

namespace Diplomski.Core.Entities
{
    public class FileType : BaseEntity
    {
        public string Type { get; set; }
        public int Count { get; set; }
        public User  BelongsTo { get; set; }
    }
}
