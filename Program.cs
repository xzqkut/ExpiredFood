using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpiredFood
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StewMenu menu = new StewMenu();
            menu.Run();
        }
    }

    class Stew
    {
        public Stew(string name, int shelfLifeYears, int productionYear)
        {
            Name = name;
            ShelfLifeYears = shelfLifeYears;
            ProductionYear = productionYear;
        }

        public string Name { get; private set; }
        public int ShelfLifeYears { get; private set; }
        public int ProductionYear { get; private set; }

        public bool IsExpired()
        {
            return ShelfLifeYears + ProductionYear < DateTime.Now.Year;
        }
    }

    class StewMenu
    {
        private const int ShowAllStewsCommand = 1;
        private const int ShowExpiredStewsCommand = 2;
        private const int ExitCommand = 3;

        private StewStorage _storage;

        public StewMenu()
        {
            _storage = new StewStorage();
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Menubar();

                string input = Console.ReadLine();
                if (int.TryParse(input, out int command))
                {
                    switch (command)
                    {
                        case ShowAllStewsCommand:
                            _storage.ShowAllStews();
                            break;
                        case ShowExpiredStewsCommand:
                            var expiredStews = _storage.FindExpiredStews();
                            _storage.ShowStews(expiredStews);
                            break;
                        case ExitCommand:
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Неверная команда. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                }
            }
        }

        public void Menubar()
        {
            Console.WriteLine("Меню:");
            Console.WriteLine($"{ShowAllStewsCommand}. Показать всю тушенку");
            Console.WriteLine($"{ShowExpiredStewsCommand}. Показать просроченную тушенку");
            Console.WriteLine($"{ExitCommand}. Отойти от стелажа");
            Console.Write("Выберите действие: ");
        }
    }

    class StewFactory
    {
        public Stew Create(string name, int shelfLifeYears, int productionYear)
        {
            return new Stew(name, shelfLifeYears, productionYear);
        }
    }

    class StewStorage
    {
        private List<Stew> _stews;
        private StewFactory _factory;

        public StewStorage()
        {
            _factory = new StewFactory();
            _stews = new List<Stew>();
            CreateStews();
        }

        public void CreateStews()
        {
            _stews.Add(_factory.Create("Тушенка \"Золотой Бычок\"", 3, 2021));
            _stews.Add(_factory.Create("Тушенка \"Мясная Держава\"", 2, 2023));
            _stews.Add(_factory.Create("Тушенка \"Барс\"", 4, 2020));
            _stews.Add(_factory.Create("Тушенка \"Главпродукт\"", 3, 2022));
            _stews.Add(_factory.Create("Тушенка \"Кронидов\"", 2, 2024));
            _stews.Add(_factory.Create("Тушенка \"Стрелецкие\"", 5, 2019));
            _stews.Add(_factory.Create("Тушенка \"Балтком\"", 3, 2023));
            _stews.Add(_factory.Create("Тушенка \"Ближние Горки\"", 2, 2021));
            _stews.Add(_factory.Create("Тушенка \"Мясной Дом Бородина\"", 4, 2024));
            _stews.Add(_factory.Create("Тушенка \"Йола\"", 3, 2022));
        }

        public List<Stew> FindExpiredStews()
        {
            var expired = _stews.Where(stew => stew.IsExpired()).ToList();
            _stews = _stews.Where(normal => !normal.IsExpired()).ToList();
            return expired;
        }

        public void ShowStews(List<Stew> stewsToShow)
        {
            Console.WriteLine("Полка с тушенкой:");
            foreach (var stew in stewsToShow)
            {
                Console.WriteLine($"Название тушенки: {stew.Name}, Срок годности: {stew.ShelfLifeYears}, Дата производства: {stew.ProductionYear}");
            }
        }

        public void ShowAllStews()
        {
            ShowStews(_stews);
        }
    }
}

