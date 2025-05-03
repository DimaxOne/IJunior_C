using System;
using System.Collections.Generic;
using System.Linq;

namespace Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();

            supermarket.Work();
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }

        public static int GenerateRandomNumber(int mininimumValue, int maximumValue)
        {
            return s_random.Next(mininimumValue, maximumValue + 1);
        }
    }

    class Supermarket
    {
        private List<Product> _products = new List<Product>();

        private int _money;
        private Queue<Client> _clients;

        public Supermarket()
        {
            int maximumRandomClients = 50;
            int minimumRandomClients = 15;
            int clientCount = UserUtils.GenerateRandomNumber(minimumRandomClients, maximumRandomClients + 1);
            _money = 0;

            _products.Add(new Product("Арбуз", 40));
            _products.Add(new Product("Дыня", 60));
            _products.Add(new Product("Огурцы", 70));
            _products.Add(new Product("Помидоры", 90));
            _products.Add(new Product("Ананас", 120));
            _products.Add(new Product("Кокос", 100));
            _products.Add(new Product("Бананы", 55));
            _products.Add(new Product("Апельсины", 35));
            _products.Add(new Product("Гранат", 65));
            _products.Add(new Product("Виноград", 85));
            _products.Add(new Product("Велосипед", 2500));

            _clients = new Queue<Client>();

            for (int i = 0; i < clientCount; i++)
            {
                _clients.Enqueue(new Client(_products.ToList()));
            }
        }

        public void Work()
        {
            int maximumRandomValue = 100;
            int minimumRandomValue = 0;
            int chanceAddNewClient = 10;

            while (_clients.Count > 0)
            {
                Console.WriteLine($"В кассе супермаркета: {_money} рублей.\n");

                if (UserUtils.GenerateRandomNumber(minimumRandomValue, maximumRandomValue + 1) < chanceAddNewClient)
                {
                    Console.WriteLine("В очереди появился новый клиент!");
                    _clients.Enqueue(new Client(_products.ToList()));
                }

                ServeClient();

                Console.WriteLine("\nНажмите любую кнопку для продолжения.");
                Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine("Супермаркет закрылся.");
        }

        private void ServeClient()
        {
            Client client = _clients.Dequeue();
            int totalPrice = 0;

            Console.WriteLine($"У клиента {client.Money} рублей. В корзине клиента: ");
            client.ShowBasket();

            while (client.Money < client.GetProductsSum() && client.BasketCount >= 1)
            {
                DrawDivider();
                Console.WriteLine("Не хватает денег. Убираем случайный товар!");
                client.RemoveRandomProduct();
                DrawDivider();
            }

            if (client.BasketCount == 0)
            {
                Console.WriteLine("Клиент ничего не купил.");
            }
            else
            {
                _money += client.GetProductsSum();
                client.BuyProducts();
                client.ShowBag();
            }
        }

        private void DrawDivider()
        {
            Console.WriteLine(new string('*', 50));
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product Clone()
        {
            return new Product(Name, Price);
        }
    }

    class Client
    {
        private List<Product> _basket;
        private List<Product> _bag;

        public Client(List<Product> products)
        {
            int maximumRandomMoney = 3000;
            int minimumRandomMoney = 200;

            Money = UserUtils.GenerateRandomNumber(minimumRandomMoney, maximumRandomMoney + 1);
            _basket = new List<Product>();
            _bag = new List<Product>();
            AddProductsToBasket(products);
        }

        public int Money { get; private set; }
        public int BasketCount => _basket.Count;

        public void AddProductsToBasket(List<Product> products)
        {
            int maximumProductsInBasket = 10;
            int minimumProductsInBasket = 3;
            int productInBasket = UserUtils.GenerateRandomNumber(minimumProductsInBasket, maximumProductsInBasket + 1);

            for (int i = 0; i < productInBasket; i++)
            {
                _basket.Add(products[UserUtils.GenerateRandomNumber(0, products.Count - 1)].Clone());
            }
        }

        public void ShowBasket()
        {
            foreach (Product product in _basket)
            {
                Console.WriteLine($"{product.Name} по цене {product.Price}");
            }
        }

        public void ShowBag()
        {
            Console.WriteLine($"Клиент купил:");

            foreach (Product product in _bag)
            {
                Console.Write($"{product.Name} ");
            }
        }

        public void BuyProducts()
        {
            int totalPrice = GetProductsSum();
            Money -= totalPrice;
            Console.WriteLine($"Клиент потратил {totalPrice} рублей и у него осталось {Money}.");

            _bag.AddRange(_basket);
            _basket.Clear();
        }

        public int GetProductsSum()
        {
            int sum = 0;

            foreach (Product product in _basket)
            {
                sum += product.Price;
            }

            return sum;
        }

        public void RemoveRandomProduct()
        {
            int productIndex = UserUtils.GenerateRandomNumber(0, _basket.Count - 1);

            if (_basket.Count >= 1)
            {
                Console.WriteLine($"Из корзины убрано: {_basket[productIndex].Name}");
                _basket.RemoveAt(productIndex);
            }
            else
            {
                Console.WriteLine("Товаров не осталось.");
            }
        }
    }
}