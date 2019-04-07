﻿// <auto-generated />
using System;
using CoreFunctions.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreFunctions.Data.Migrations
{
    [DbContext(typeof(CoreFunctionsDbContext))]
    partial class CoreFunctionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreFunctions.Data.Data.FunctionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Imports");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<string>("References");

                    b.Property<string>("Script");

                    b.Property<string>("ShouldExecuteDelegate");

                    b.HasKey("Id");

                    b.ToTable("Functions");
                });
#pragma warning restore 612, 618
        }
    }
}
