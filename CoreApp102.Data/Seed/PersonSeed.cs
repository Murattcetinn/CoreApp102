using CoreApp102.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp102.Data.Seed
{
    public class PersonSeed : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(

                new Person { Id = 1, Name = "Semih ", Surname = "Semih " },
                new Person { Id = 2, Name = "Ali ", Surname = "Osman " }
                );
        }
    }
}
