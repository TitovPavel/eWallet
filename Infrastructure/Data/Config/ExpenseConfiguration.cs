using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{

    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expense");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Sum).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.SumByn).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Comment).IsRequired(false).HasMaxLength(255);
            
            builder.HasOne(ci => ci.Currency)
               .WithMany()
               .HasForeignKey(ci => ci.CurrencyId);

            builder.HasOne(ci => ci.Category)
                .WithMany()
                .HasForeignKey(ci => ci.CategoryId);

            builder.HasOne(ci => ci.User)
               .WithMany(p => p.Expenses)
               .HasForeignKey(ci => ci.UserId);


        }
    }
}
