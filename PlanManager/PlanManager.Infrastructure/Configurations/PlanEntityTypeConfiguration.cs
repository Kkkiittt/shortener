using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PlanManager.Domain.Entities;

using Shortener.Shared.Enums;

namespace PlanManager.Infrastructure.Configurations;

public class PlanEntityTypeConfiguration : IEntityTypeConfiguration<Plan>
{
	public void Configure(EntityTypeBuilder<Plan> builder)
	{
		builder.HasKey(p => p.Id);
		List<ClientAction> baseActions = new List<ClientAction>()
		{
			ClientAction.LinkCreate,
			ClientAction.LinkDelete,
			ClientAction.LinkInfo
		};
		Plan basePlan = new Plan("Free", 7, 3, baseActions, "Free tier", 0, TimeSpan.MaxValue)
		{
			Id = 1,
			Created = DateTime.Parse("2022-05-12 12:12:12.123").ToUniversalTime(),
			Updated = DateTime.Parse("2022-05-12 12:12:12.123").ToUniversalTime()
		};
		List<ClientAction> allActions = new List<ClientAction>()
		{
			ClientAction.LinkCreate,
			ClientAction.LinkDelete,
			ClientAction.LinkInfo,
			ClientAction.LinkUpdate
		};
		Plan adminPlan = new Plan("Admin", int.MaxValue, int.MaxValue, allActions, "Admin tier", double.MaxValue, TimeSpan.MaxValue)
		{
			Id = 2,
			Created = DateTime.Parse("2022-05-12 12:12:12.123").ToUniversalTime(),
			Updated = DateTime.Parse("2022-05-12 12:12:12.123").ToUniversalTime()
		};
		builder.HasData(basePlan);
		builder.HasData(adminPlan);
	}
}
