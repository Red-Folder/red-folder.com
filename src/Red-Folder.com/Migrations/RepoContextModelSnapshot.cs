using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using RedFolder.Data;

namespace RedFolder.com.Migrations
{
    [DbContext(typeof(RepoContext))]
    partial class RepoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
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
                });

            modelBuilder.Entity("RedFolder.Data.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("RepoId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("RedFolder.Data.Tag", b =>
                {
                    b.HasOne("RedFolder.Data.Repo")
                        .WithMany()
                        .HasForeignKey("RepoId");
                });
        }
    }
}
