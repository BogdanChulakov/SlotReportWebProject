﻿namespace OnlineSlotReports.Services.Data.EmployeesServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeesService
    {
        IEnumerable<T> All<T>(string id);

        Task DeleteAsync(string id);

        Task ChangeEmailAsync(string id, string email);

        Task ChangePhoneNumberAsync(string id, string phoneNumber);

        Task AddAsync(string fullName, string email, string phonenumber, DateTime startWorkDate, string gamingHallId);

        T GetById<T>(string id);

        string GetHallId(string id);
    }
}
