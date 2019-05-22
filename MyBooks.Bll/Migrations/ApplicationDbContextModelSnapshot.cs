﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBooks.Bll.Context;

namespace MyBooks.Bll.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.ApplicationRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1975, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Brandon Sanderson"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1977, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Brent Weeks"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1948, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Robert Jordan"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1988, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pierce Brown"
                        });
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverImagePath");

                    b.Property<string>("Genre");

                    b.Property<string>("GoodreadsId");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoverImagePath = "thewayofkings.jpg",
                            Genre = "Fantasy",
                            GoodreadsId = "7235533-the-way-of-kings",
                            Title = "The Way of Kings"
                        },
                        new
                        {
                            Id = 2,
                            CoverImagePath = "blackprism.jpg",
                            Genre = "Fantasy",
                            GoodreadsId = "7165300-the-black-prism",
                            Title = "The Black Prism"
                        },
                        new
                        {
                            Id = 3,
                            CoverImagePath = "theeyeoftheworld.jpg",
                            Genre = "Fantasy",
                            GoodreadsId = "228665.The_Eye_of_the_World",
                            Title = "The Eye of the World"
                        },
                        new
                        {
                            Id = 4,
                            CoverImagePath = "redrising.jpg",
                            Genre = "Scifi",
                            GoodreadsId = "15839976-red-rising",
                            Title = "Red Rising"
                        },
                        new
                        {
                            Id = 5,
                            CoverImagePath = "brokeneye.jpg",
                            Genre = "Fantasy",
                            GoodreadsId = "12652457-the-broken-eye",
                            Title = "The Broken Eye"
                        });
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.BookAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<int>("BookId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            BookId = 1
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            BookId = 2
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 3,
                            BookId = 3
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 4,
                            BookId = 4
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 2,
                            BookId = 5
                        });
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.BookOwnership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<int>("UserId");

                    b.Property<long?>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId1");

                    b.ToTable("BookOwnership");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Edition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<DateTime>("DateOfPublish");

                    b.Property<string>("Format")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("Paperback");

                    b.Property<string>("IsbnNumber");

                    b.Property<int>("NumberOfPages");

                    b.Property<int>("PublisherId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Editions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            DateOfPublish = new DateTime(2010, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Format = "Hardcover",
                            IsbnNumber = "0765326353",
                            NumberOfPages = 1007,
                            PublisherId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 1,
                            DateOfPublish = new DateTime(2011, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Format = "Paperback",
                            IsbnNumber = "0765365278",
                            NumberOfPages = 1258,
                            PublisherId = 2
                        });
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Tor Books"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tom Doherty"
                        });
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.Property<long?>("UserId1");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId1");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.ReadingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EditionId");

                    b.Property<int>("NumberOfPagesRead");

                    b.Property<int>("UserId");

                    b.Property<long?>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("EditionId");

                    b.HasIndex("UserId1");

                    b.ToTable("ReadingStatuses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.BookAuthor", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Bll.Entities.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.BookOwnership", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.Book", "Book")
                        .WithMany("BookOwnerships")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Edition", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.Book", "Book")
                        .WithMany("Editions")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Bll.Entities.Publisher", "Publisher")
                        .WithMany("Editions")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.Rating", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.Book", "Book")
                        .WithMany("Ratings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("MyBooks.Bll.Entities.ReadingStatus", b =>
                {
                    b.HasOne("MyBooks.Bll.Entities.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyBooks.Bll.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });
#pragma warning restore 612, 618
        }
    }
}
