﻿namespace OnlineSlotReports.Services.Data.GamingHallServices
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OnlineSlotReports.Common;
    using OnlineSlotReports.Data.Common.Repositories;
    using OnlineSlotReports.Data.Models;
    using OnlineSlotReports.Services.Data.UsersHallsServices;
    using OnlineSlotReports.Services.Mapping;

    public class GamingHallService : IGamingHallService
    {
        private readonly IDeletableEntityRepository<GamingHall> repository;
        private readonly IUsersHallsService usersHallsService;

        public GamingHallService(IDeletableEntityRepository<GamingHall> repository, IUsersHallsService usersHallsService)
        {
            this.repository = repository;
            this.usersHallsService = usersHallsService;
        }

        public async Task AddAsync(string hallName, string imageUrl, string description, string phoneNumber, string adress, string town, string userId)
        {
            var gamingHall = new GamingHall();

            gamingHall.HallName = hallName;

            if (imageUrl == null)
            {
                imageUrl = GlobalConstants.DefaultLogo;
            }

            gamingHall.ImageUrl = imageUrl;
            gamingHall.Description = description;
            gamingHall.PhoneNumber = phoneNumber;
            gamingHall.Adress = adress;
            gamingHall.Town = town;

            await this.repository.AddAsync(gamingHall);
            await this.repository.SaveChangesAsync();

            await this.usersHallsService.AddHallToUserAsync(userId, gamingHall.Id);
        }

        public IEnumerable<T> All<T>(int take, int skip)
        {
            var halls = this.repository.All().OrderBy(x => x.HallName).Skip(skip).Take(take);

            return halls.To<T>().ToList();
        }

        public IEnumerable<T> AllHalls<T>(string userId, int take, int skip)
        {
            IQueryable<GamingHall> halls = this.repository.All().Where(x => x.Users.All(y => y.UserId == userId)).OrderBy(x => x.CreatedOn).Skip(skip).Take(take);

            return halls.To<T>().ToList();
        }

        public IEnumerable<T> AllOfChain<T>(string hallName)
        {
            var halls = this.repository.All().Where(x => x.HallName == hallName).OrderBy(x => x.CreatedOn);

            return halls.To<T>().ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var hall = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            this.repository.Delete(hall);

            await this.repository.SaveChangesAsync();
        }

        public int GetAllHallsCount()
        {
            int count = this.repository.All().Count();

            return count;
        }

        public int GetHallsCount(string userId)
        {
            int count = this.repository.All().Where(x => x.Users.All(y => y.UserId == userId)).Count();

            return count;
        }

        public int GetSearchHallsCount(string name)
        {
            int count = this.repository.All().Where(x => x.HallName.Contains(name) || x.Town.Contains(name)).Count();

            return count;
        }

        public T GetT<T>(string id)
        {
            var hall = this.repository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return hall;
        }

        public IEnumerable<T> Search<T>(string name, int take, int skip)
        {
            IQueryable<GamingHall> halls = this.repository.All().Where(x => x.Town.Contains(name) || x.HallName.Contains(name)).OrderBy(x => x.CreatedOn).Skip(skip).Take(take);

            return halls.To<T>().ToList();
        }

        public async Task UpdateAsync(string id, string hallName, string imageUrl, string description, string phoneNumber, string adress, string town)
        {
            var gamingHall = this.repository.All().Where(x => x.Id == id).FirstOrDefault();

            gamingHall.HallName = hallName;
            gamingHall.ImageUrl = imageUrl;
            gamingHall.Description = description;
            gamingHall.PhoneNumber = phoneNumber;
            gamingHall.Adress = adress;
            gamingHall.Town = town;

            await this.repository.SaveChangesAsync();
        }
    }
}
