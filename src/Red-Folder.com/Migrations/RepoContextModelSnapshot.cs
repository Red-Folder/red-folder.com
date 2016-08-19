using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RedFolder.Data;

namespace RedFolder.com.Migrations
{
    [DbContext(typeof(RepoContext))]
    partial class RepoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RedFolder.Data.Repo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("OpenIssues");

                    b.Property<int>("Stars");

                    b.HasKey("Id");

                    b.ToTable("Repos");
                });

            modelBuilder.Entity("RedFolder.Data.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("RepoId");

                    b.HasKey("Id");

                    b.HasIndex("RepoId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RedFolder.Data.Tag", b =>
                {
                    b.HasOne("RedFolder.Data.Repo")
                        .WithMany("Tags")
                        .HasForeignKey("RepoId");
                });
        }
    }
}
