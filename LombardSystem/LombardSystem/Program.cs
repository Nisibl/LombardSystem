using LombardSystem.Database;
using LombardSystem.Forms;
using LombardSystem.Models;

namespace LombardSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ������������� ���� ������
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureCreated();

                // ���������� �������� �������������, ���� �� ���
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>
                {
                    new User { Username = "employee", Password = "123", Role = "Employee" },
                    new User { Username = "owner", Password = "123", Role = "Owner" }
                });
                    context.SaveChanges();
                }
            }

            Application.Run(new AuthForm());
        }
    }
}