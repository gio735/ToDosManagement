using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDosManagement.Domain;
using ToDosManagement.Domain.Models;

namespace ToDosManagement.Persistence.Seed
{
    public static class DataSeed
    {
        private const string SECRET_KEY = "ILHYSMBORITMHBTHA";
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<DataContext>();

            Migrate(database);
            SeedData(database);
        }

        private static void Migrate(DataContext context)
        {
            context.Database.Migrate();
        }

        private static void SeedData(DataContext context)
        {
            var usersWithData = new List<User>()
            {
                new User
                {
                    Username = "user1",
                    Password = GenerateHash("password"),
                    ToDos = new List<ToDo>()
                    {
                        new ToDo
                        {
                            Title = "user 1 ToDo 1",
                            ToDoState = ToDoState.Active,
                            Subtasks = new List<Subtask>()
                            {
                                new Subtask
                                {
                                    Title = "user 1 ToDo 1 Subtask 1",
                                    Description = "Something random 1"
                                },
                                new Subtask
                                {
                                    Title = "user 1 ToDo 1 Subtask 2",
                                    Description = "Something random 2"
                                },
                                new Subtask
                                {
                                    Title = "user 1 ToDo 1 Subtask 3",
                                    Description = "Something random 3"
                                }
                            }
                        },
                        new ToDo
                        {
                            Title = "user 1 ToDo 2",
                            ToDoState = ToDoState.Active,
                            Subtasks = new List<Subtask>()
                            {
                                new Subtask
                                {
                                    Title = "user 1 ToDo 2 Subtask 1",
                                    Description = "Something random 1"
                                },
                                new Subtask
                                {
                                    Title = "user 1 ToDo 2 Subtask 2",
                                    Description = "Something random 2"
                                },
                                new Subtask
                                {
                                    Title = "user 1 ToDo 2 Subtask 3",
                                    Description = "Something random 3"
                                }
                            }
                        },
                        new ToDo
                        {
                            Title = "user 1 ToDo 3",
                            ToDoState = ToDoState.Active,
                            Subtasks = new List<Subtask>()
                            {
                                new Subtask
                                {
                                    Title = "user 1 ToDo 3 Subtask 1",
                                    Description = "Something random 1"
                                },
                                new Subtask
                                {
                                    Title = "user 1 ToDo 3 Subtask 2",
                                    Description = "Something random 2"
                                },
                                new Subtask
                                {
                                    Title = "user 1 ToDo 3 Subtask 3",
                                    Description = "Something random 3"
                                }
                            }
                        }
                    }
                },
                new User
                {
                    Username = "user2",
                    Password = GenerateHash("password"),
                    ToDos = new List<ToDo>()
                    {
                        new ToDo
                        {
                            Title = "user 2 ToDo 1",
                            ToDoState = ToDoState.Active,
                            Subtasks = new List<Subtask>()
                            {
                                new Subtask
                                {
                                    Title = "user 2 ToDo 1 Subtask 1",
                                    Description = "Something random 1"
                                },
                                new Subtask
                                {
                                    Title = "user 2 ToDo 1 Subtask 2",
                                    Description = "Something random 2"
                                },
                                new Subtask
                                {
                                    Title = "user 2 ToDo 1 Subtask 3",
                                    Description = "Something random 3"
                                }
                            }
                        },
                        new ToDo
                        {
                            Title = "user 2 ToDo 2",
                            ToDoState = ToDoState.Active,
                            Subtasks = new List<Subtask>()
                            {
                                new Subtask
                                {
                                    Title = "user 2 ToDo 2 Subtask 1",
                                    Description = "Something random 1"
                                },
                                new Subtask
                                {
                                    Title = "user 2 ToDo 2 Subtask 2",
                                    Description = "Something random 2"
                                },
                                new Subtask
                                {
                                    Title = "user 2 ToDo 2 Subtask 3",
                                    Description = "Something random 3"
                                }
                            }
                        },
                        new ToDo
                        {
                            Title = "user 2 ToDo 3",
                            ToDoState = ToDoState.Active,
                            Subtasks = new List<Subtask>()
                            {
                                new Subtask
                                {
                                    Title = "user 2 ToDo 3 Subtask 1",
                                    Description = "Something random 1"
                                },
                                new Subtask
                                {
                                    Title = "user 2 ToDo 3 Subtask 2",
                                    Description = "Something random 2"
                                },
                                new Subtask
                                {
                                    Title = "user 2 ToDo 3 Subtask 3",
                                    Description = "Something random 3"
                                }
                            }
                        }
                    }
                }
            };
            foreach (var user in usersWithData)
            {
                if (context.Users.Any(x => x.Username == user.Username)) continue;

                context.Users.Add(user);
            }
            context.SaveChanges();
        }
        private static string GenerateHash(string input)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input + SECRET_KEY);
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
