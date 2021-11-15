using System;
using System.Collections.Generic;
using System.Text;

namespace Diplomski.Core.Entities.ManyToManyRelations
{
    public class UserFileType : BaseEntity
    {
        public int Count { get; set; }
        public User User { get; set; }
        public FileType FileType { get; set; }
    }
}
