using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManager.Domain.Entities;

namespace TaskManager.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("TaskItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).HasMaxLength(512);
            builder.Property(x => x.IsCompleted).IsRequired().HasDefaultValue(false);

            builder.OwnsOne(t => t.DueDateRange, dueDateRange =>
            {
                dueDateRange.Property(d => d.StartDate)
                    .HasColumnName("DueStartDate")
                    .IsRequired();

                dueDateRange.Property(d => d.EndDate)
                    .HasColumnName("DueEndDate")
                    .IsRequired();
            });

            builder.OwnsOne(t => t.TaskPriority, taskPriority =>
            {
                taskPriority.Property(tp => tp.PriorityLevel)
                    .HasColumnName("PriorityLevel")
                    .HasConversion<string>()
                    .IsRequired();
            });

            // TaskFrequency Enum
            builder.Property(t => t.Frequency)
                .HasConversion<string>()
                .HasColumnName("Frequency");

            builder.HasOne(t => t.User)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
