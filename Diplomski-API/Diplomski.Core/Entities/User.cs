using System;
using System.Collections.Generic;
using System.Text;

namespace Diplomski.Core.Entities
{
    public class User : BaseEntity
    {
        public string IdentityId { get; set; }
        public string FullName { get; set; }
        public ICollection<FileType> FileTypes { get; set; }
        public ICollection<File> Files { get; set;  }

    }
}
