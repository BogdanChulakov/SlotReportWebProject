namespace OnlineSlotReports.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using OnlineSlotReports.Data;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Data.Repositories;
    using OnlineSlotReports.Services.Data.EmployeesServices;
    using OnlineSlotReports.Services.Data.GamingHallServices;
    using OnlineSlotReports.Services.Data.Tests.Factory;
    using OnlineSlotReports.Services.Mapping;
    using OnlineSlotReports.Web.ViewModels;
    using OnlineSlotReports.Web.ViewModels.EmployeesViewModel;
    using Xunit;

    public class EmployeesServiceTests
    {
        [Fact]
        public async Task AddAsyncWithCorectData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var result = await dbContext.Employees.FirstOrDefaultAsync();

            Assert.Equal("Ivan Ivanov", result.FullName);
            Assert.Equal("mail@mail.mail", result.Email);
            Assert.Equal("08888888888", result.PhoneNumber);
            Assert.Equal(date, result.StartWorkDate);
            Assert.Equal("1", result.GamingHallId);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task AllWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                "Ivan Ivanov" + i,
                "mail@mail.mail",
                "08888888888",
                date,
                "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var results = service.All<EmployeeViewModel>("1");
            int count = 0;
            foreach (var result in results)
            {
                count++;
                Assert.Equal("Ivan Ivanov" + count, result.FullName);
                Assert.Equal("mail@mail.mail", result.Email);
                Assert.Equal("08888888888", result.PhoneNumber);
                Assert.Equal(date, result.StartWorkDate);
                Assert.Equal("1", result.GamingHallId);
            }

            Assert.Equal(5, count);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task AllWithNullId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                "Ivan Ivanov" + i,
                "mail@mail.mail",
                "08888888888",
                date,
                "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var results = service.All<EmployeeViewModel>(null);
            int count = 0;
            foreach (var result in results)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Dispose();
        }

        [Fact]
        public async Task AllWithNoexistinglId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            for (int i = 1; i <= 5; i++)
            {
                await service.AddAsync(
                "Ivan Ivanov" + i,
                "mail@mail.mail",
                "08888888888",
                date,
                "1");
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var results = service.All<EmployeeViewModel>("2");
            int count = 0;
            foreach (var result in results)
            {
                count++;
            }

            Assert.Equal(0, count);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangeEmailWithCorectEmail()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.ChangeEmailAsync(employee.Id, "abv@abv.bg");

            var result = await dbContext.Employees.FirstOrDefaultAsync();
            Assert.Equal("abv@abv.bg", result.Email);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangeEmailWithIncorectEmail()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.ChangeEmailAsync(employee.Id, "abvabv.bg");

            var result = await dbContext.Employees.FirstOrDefaultAsync();
            Assert.Equal(employee.Email, result.Email);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangeEmailWithNullEmail()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.ChangeEmailAsync(employee.Id, null);

            var result = await dbContext.Employees.FirstOrDefaultAsync();
            Assert.Equal(employee.Email, result.Email);
            dbContext.Database.EnsureDeleted();

        }

        [Fact]
        public async Task ChangePhoneNumberWithCorectData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.ChangePhoneNumberAsync(employee.Id, "0111111111");

            var result = await dbContext.Employees.FirstOrDefaultAsync();
            Assert.Equal("0111111111", result.PhoneNumber);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangePhonenumberWithIncorectData()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.ChangePhoneNumberAsync(employee.Id, "abvabv.bg");

            var result = await dbContext.Employees.FirstOrDefaultAsync();
            Assert.Equal(employee.PhoneNumber, result.PhoneNumber);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task ChangePhoneNumberWithNull()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.ChangeEmailAsync(employee.Id, null);

            var result = await dbContext.Employees.FirstOrDefaultAsync();
            Assert.Equal(employee.PhoneNumber, result.PhoneNumber);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task DeleteAsyncWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            await service.DeleteAsync(employee.Id);

            var result = await dbContext.GamingHalls.Where(x => x.Id == employee.Id).FirstOrDefaultAsync();

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByIdWithVaidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.GetById<EmployeeViewModel>(employee.Id);

            Assert.Equal(employee.FullName, result.FullName);
            Assert.Equal(employee.Email, result.Email);
            Assert.Equal(employee.PhoneNumber, result.PhoneNumber);
            Assert.Equal(employee.StartWorkDate, result.StartWorkDate);
            Assert.Equal(employee.GamingHallId, result.GamingHallId);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByIdWithInvalidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.GetById<EmployeeViewModel>("1234");

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetByIdWithNullId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var result = service.GetById<EmployeeViewModel>(null);

            Assert.True(result == null);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetHallIdWithValidId()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
            var service = new EmployeesService(new EfDeletableEntityRepository<Employee>(dbContext));
            var date = DateTime.UtcNow;
            await service.AddAsync(
                "Ivan Ivanov",
                "mail@mail.mail",
                "08888888888",
                date,
                "1");
            var employee = await dbContext.Employees.FirstOrDefaultAsync();

            var result = service.GetHallId(employee.Id);

            Assert.Equal("1", result);
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
