using FirstHackathon.Models;
using FirstHackathon.Models.Votes;
using Microsoft.EntityFrameworkCore;
using System;

namespace FirstHackathon.Context
{
    public sealed class FirstHackathonDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public FirstHackathonDbContext(DbContextOptions options) : base(options)
        {
            if (Database.EnsureCreated())
                LoadTestData();
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<CreatePersonRequest> CreatePersonRequests { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<NewsPost> News { get; set; }

        #region Votes

        public DbSet<Voting> Votings { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Vote> Votes { get; set; }

        #endregion

        private void LoadTestData()
        {
            House house = new House(Guid.NewGuid(), "г. Оренбург, ул. Чкалова, д. 32", "admin@mail.ru", "admin");

            Person person = new Person(Guid.NewGuid(), "Данил", "Кушмухаметов", "user@mail.ru", "user", house);

            CreatePersonRequest request1 = new CreatePersonRequest(Guid.NewGuid(), "Пользователь", "1", "user_1@mail.ru", "user", house);
            CreatePersonRequest request2 = new CreatePersonRequest(Guid.NewGuid(), "Пользователь", "2", "user_2@mail.ru", "user", house);

            NewsPost newsPost1 = new NewsPost(Guid.NewGuid(), "В Оренбурге завершены поиски без вести пропавшего пенсионера",
                "О том, что Андрей Боженков найден, сообщили в ПСО «ОренСпас» со ссылкой на родственников.\n" +
                "Накануне, 12 июня, мужчина сам вернулся домой. С ним все в порядке. Жизни и здоровью пенсионера ничто не угрожает." +
                " В причинах его ухода из дома разбираются сотрудники полиции. Проводится проверка.",
                null, DateTime.UtcNow, house);
            NewsPost newsPost2 = new NewsPost(Guid.NewGuid(), "В Оренбурге завершены поиски без вести пропавшего пенсионера",
                "О том, что Андрей Боженков найден, сообщили в ПСО «ОренСпас» со ссылкой на родственников.\n" +
                "Накануне, 12 июня, мужчина сам вернулся домой. С ним все в порядке. Жизни и здоровью пенсионера ничто не угрожает." +
                " В причинах его ухода из дома разбираются сотрудники полиции. Проводится проверка.",
                null, DateTime.UtcNow, house);
            NewsPost newsPost3 = new NewsPost(Guid.NewGuid(), "В Оренбурге завершены поиски без вести пропавшего пенсионера",
                "О том, что Андрей Боженков найден, сообщили в ПСО «ОренСпас» со ссылкой на родственников.\n" +
                "Накануне, 12 июня, мужчина сам вернулся домой. С ним все в порядке. Жизни и здоровью пенсионера ничто не угрожает." +
                " В причинах его ухода из дома разбираются сотрудники полиции. Проводится проверка.",
                null, DateTime.UtcNow, house);

            Meeting meeting = new Meeting(Guid.NewGuid(), "Уборка прилегающей территории", DateTime.UtcNow.AddHours(5), "Описание мероприятия", null, house);

            Voting voting = new Voting(Guid.NewGuid(), "Голосование 1", house);
            Variant variant1 = new Variant(Guid.NewGuid(), "Первый вариант", voting);
            Variant variant2 = new Variant(Guid.NewGuid(), "Второй вариант", voting);
            Variant variant3 = new Variant(Guid.NewGuid(), "Третий вариант", voting);

            Houses.Add(house);
            People.Add(person);
            CreatePersonRequests.AddRange(new CreatePersonRequest[] { request1, request2 });
            News.AddRange(new NewsPost[] { newsPost1, newsPost2, newsPost3 });
            Meetings.Add(meeting);
            Votings.Add(voting);
            Variants.AddRange(new Variant[] { variant1, variant2, variant3 });

            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<House>(builder =>
            {
                builder.ToTable("Houses");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Address)
                    .IsRequired();

                builder.Property(o => o.Login)
                    .IsRequired(true);
                builder.Property(o => o.Password)
                     .IsRequired(true);

                builder.HasMany(o => o.People)
                    .WithOne(p => p.House)
                    .IsRequired(false);

                builder.HasMany(o => o.CreatePeopleRequests)
                    .WithOne(p => p.House)
                    .IsRequired(false);

                builder.HasMany(o => o.Meetings)
                    .WithOne(p => p.House)
                    .IsRequired(false);

                builder.HasMany(o => o.Votings)
                    .WithOne(p => p.House)
                    .IsRequired(false);

                builder.HasMany(o => o.News)
                    .WithOne(p => p.House)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Person>(builder =>
            {
                builder.ToTable("People");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Name)
                    .IsRequired(true);
                builder.Property(o => o.Surname)
                     .IsRequired(true);

                builder.Property(o => o.Login)
                    .IsRequired(true);
                builder.Property(o => o.Password)
                     .IsRequired(true);

                builder.HasMany(o => o.Votes)
                    .WithOne(p => p.Person)
                    .IsRequired(false);
            });

            modelBuilder.Entity<CreatePersonRequest>(builder =>
            {
                builder.ToTable("CreatePeople");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Name)
                    .IsRequired(true);
                builder.Property(o => o.Surname)
                     .IsRequired(true);

                builder.Property(o => o.Login)
                    .IsRequired(true);
                builder.Property(o => o.Password)
                     .IsRequired(true);
            });

            modelBuilder.Entity<NewsPost>(builder =>
            {
                builder.ToTable("News");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Title)
                    .IsRequired();

                builder.Property(o => o.Description)
                    .IsRequired();

                builder.Property(o => o.Image)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Meeting>(builder =>
            {
                builder.ToTable("Meetings");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Title)
                    .IsRequired(true);

                builder.Property(o => o.MeetingDate)
                    .IsRequired(true);

                builder.Property(o => o.Description)
                    .IsRequired(false);

                builder.Property(o => o.Image)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Voting>(builder =>
            {
                builder.ToTable("Votings");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Title)
                    .IsRequired();

                builder.Property(o => o.IsClosed)
                    .HasDefaultValue(false)
                    .IsRequired();

                builder.HasMany(o => o.Variants)
                    .WithOne(p => p.Voting)
                    .IsRequired();
            });

            modelBuilder.Entity<Variant>(builder =>
            {
                builder.ToTable("Variants");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();

                builder.Property(o => o.Title)
                    .IsRequired();

                builder.HasMany(o => o.Votes)
                    .WithOne(p => p.Variant)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Vote>(builder =>
            {
                builder.ToTable("Votes");

                builder.HasKey(o => o.Id);
                builder.Property(o => o.Id)
                    .ValueGeneratedNever()
                    .IsRequired();
            });
        }
    }
}
