using AutoMapper;
using Diplomski.Application.Dtos;
using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileDto>();
            //CreateMap<ICollection<File>, ICollection<FileDto>>();
        }
    }
}
