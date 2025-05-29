using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LombardSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LombardSystem.Database
{
    internal static class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            // Гарантируем, что база данных создана
            context.Database.EnsureCreated();

            // Проверяем, есть ли уже данные
            if (context.Users.Any())
            {
                return; // База уже инициализирована
            }

            // Добавляем начальных пользователей
            var users = new User[]
            {
                new User { Username = "employee", Password = "123", Role = "Employee" },
                new User { Username = "owner", Password = "123", Role = "Owner" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Можно добавить тестовых клиентов, предметы и т.д.
            var clients = new Client[]
            {
                new Client { FullName = "Иванов Иван Иванович", ContactInfo = "+79123456789", PassportData = "1234 567890" }
            };

            context.Clients.AddRange(clients);
            context.SaveChanges();
        }
    }
}
