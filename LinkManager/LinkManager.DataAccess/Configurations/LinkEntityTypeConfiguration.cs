using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinkManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkManager.DataAccess.Configurations;

public class LinkEntityTypeConfiguration : IEntityTypeConfiguration<Link>
{
	public void Configure(EntityTypeBuilder<Link> builder)
	{
		builder.HasKey(x => x.Id);
		builder.HasIndex(x => x.UserId).IsUnique(false);
	}
}
