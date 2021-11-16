using Diplomski.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Infrastructure.Persistence.DataSeed
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            string[] types = { "image/", "text/", "application/" };
            List<FileType> fileTypes = new List<FileType>();

            for (int i = 0; i < types.Length; i++)
            {
                fileTypes.Add(new FileType { Id = i+1, Type = types[i] });
            }

            //modelBuilder
            modelBuilder.Entity<FileType>().HasData(fileTypes);
        }
    }
}
