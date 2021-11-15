using Diplomski.Core.Entities.ManyToManyRelations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diplomski.Core.Entities
{
    public class FileType : BaseEntity
    {
        public string Type { get; set; }
        public ICollection<UserFileType>  UserFileTypes { get; set; }

        //Moglo i da ima i Kolekciju Fajlova ali u nasem slucaju ne treba tako da je bolje da ne kreiramo sto vise veza ako ih ne koristimo
    }
}
