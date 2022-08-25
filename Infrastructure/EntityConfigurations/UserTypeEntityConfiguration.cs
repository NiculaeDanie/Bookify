using Bookify.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class UserTypeEntityConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<Bookify.Domain.Model.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           
        }
    }
}
