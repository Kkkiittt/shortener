using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PlanManager.Domain.Entities;
using PlanManager.Domain.Enums;

namespace PlanManager.DataAccess.Configurations;

public class PlanEntityTypeConfiguration : IEntityTypeConfiguration<Plan>
{
	public void Configure(EntityTypeBuilder<Plan> builder)
	{
		builder.HasKey(p => p.Id);
		List<PlanAction> baseActions = new List<PlanAction>()
		{
			PlanAction.LinkCreate,
			PlanAction.LinkDelete,
			PlanAction.LinkInfo
		};
		Plan basePlan = new Plan("Free", 7, 3, baseActions, "Free tier")
		{
			Id = 1,
			Created = DateTime.Parse("2022-05-12 12:12:12.123").ToUniversalTime(),
			Updated = DateTime.Parse("2022-05-12 12:12:12.123").ToUniversalTime()
		};
		builder.HasData(basePlan);
	}
}
