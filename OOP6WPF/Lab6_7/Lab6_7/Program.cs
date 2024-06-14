using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_7
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите таблицу:\n1 - Job\n2 - Status\n3 - Person\n4 - Выход\n");
                string input = Console.ReadLine();
                int tableChoice;
                if (!int.TryParse(input, out tableChoice) || tableChoice < 1 || tableChoice > 4)
                {
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    continue;
                }

                if (tableChoice == 4)
                {
                    break;
                }

                dynamic dao;
                switch (tableChoice)
                {
                    case 1:
                        dao = new JobDao();
                        break;
                    case 2:
                        dao = new StatusDao();
                        break;
                    case 3:
                        dao = new PersonDao();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        Console.ReadKey();
                        continue;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Выберите действие:\n1 - Показать все\n2 - Добавить\n3 - Удалить\n4 - Изменить\n5 - Вернуться назад");
                    int actionChoice = Convert.ToInt32(Console.ReadLine());

                    if (actionChoice == 5)
                    {
                        break;
                    }

                    switch (actionChoice)
                    {
                        case 1:
                            // Показать все
                            var allItems = dao.GetAll();
                            foreach (var item in allItems)
                            {
                                Console.WriteLine(item);
                            }
                            Console.ReadKey();
                            break;
                        case 2:
                            // Добавить
                            Console.WriteLine("Введите данные для нового элемента.");
                            if (dao is JobDao)
                            {
                                Console.Write("Введите название вакансии: ");
                                string jobName = Console.ReadLine();

                                Job job = new Job
                                {
                                    Name = jobName
                                };

                                dao.Add(job);
                            }
                            else if (dao is StatusDao)
                            {
                                Console.Write("Введите название статуса: ");
                                string statusName = Console.ReadLine();

                                Status status = new Status
                                {
                                    StatusName = statusName
                                };

                                dao.Add(status);
                            }
                            else if (dao is PersonDao)
                            {
                                Console.Write("Введите имя сотрудника: ");
                                string personName = Console.ReadLine();
                                Console.Write("Введите пол сотрудника: ");
                                string gender = Console.ReadLine();
                                Console.Write("Введите возраст сотрудника: ");
                                int age = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Введите ID вакансии: ");
                                int jobId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Введите ID статуса: ");
                                int statusId = Convert.ToInt32(Console.ReadLine());

                                // Проверка корректности JobID и StatusID
                                if (new JobDao().Get(jobId) == null || new StatusDao().Get(statusId) == null)
                                {
                                    Console.WriteLine("Некорректный JobID или StatusID. Попробуйте еще раз.");
                                    break;
                                }

                                Person person = new Person
                                {
                                    Name = personName,
                                    Gender = gender,
                                    Age = age,
                                    Job_ID = jobId,
                                    Status_ID = statusId
                                };

                                dao.Add(person);
                            }
                            break;
                        case 3:
                            // Удалить
                            Console.WriteLine("Введите ID элемента, который нужно удалить.");
                            int id = Convert.ToInt32(Console.ReadLine());
                            dao.Delete(id);
                            Console.WriteLine("Элемент успешно удален.");
                            break;
                        case 4:
                            // Изменить
                            Console.WriteLine("Введите ID элемента, который нужно изменить.");
                            id = Convert.ToInt32(Console.ReadLine());

                            if (dao is JobDao)
                            {
                                Console.Write("Введите новое название вакансии: ");
                                string jobName = Console.ReadLine();

                                Job job = new Job
                                {
                                    ID = id,
                                    Name = jobName
                                };

                                dao.Update(job);
                            }
                            else if (dao is StatusDao)
                            {
                                Console.Write("Введите новое название статуса: ");
                                string statusName = Console.ReadLine();

                                Status status = new Status
                                {
                                    StatusID = id,
                                    StatusName = statusName
                                };

                                dao.Update(status);
                            }
                            else if (dao is PersonDao)
                            {
                                Console.Write("Введите новое имя сотрудника: ");
                                string personName = Console.ReadLine();
                                Console.Write("Введите новый пол сотрудника: ");
                                string gender = Console.ReadLine();
                                Console.Write("Введите новый возраст сотрудника: ");
                                int age = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Введите новый ID вакансии: ");
                                int jobId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Введите новый ID статуса: ");
                                int statusId = Convert.ToInt32(Console.ReadLine());

                                // Проверка корректности JobID и StatusID
                                if (new JobDao().Get(jobId) == null || new StatusDao().Get(statusId) == null)
                                {
                                    Console.WriteLine("Некорректный JobID или StatusID. Попробуйте еще раз.");
                                    break;
                                }

                                Person person = new Person
                                {
                                    PersonID = id,
                                    Name = personName,
                                    Gender = gender,
                                    Age = age,
                                    Job_ID = jobId,
                                    Status_ID = statusId
                                };

                                dao.Update(person);
                            }
                            Console.WriteLine("Элемент успешно обновлен.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                            Console.ReadKey();
                            continue;
                    }
                }
            }
        }
    }

}
