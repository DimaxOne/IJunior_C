using System;
using System.Collections.Generic;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            shop.Work();
        }
    }

    class Shop
    {
        private const string CommandExit = "Exit";

        private Seller _seller = new Seller(new List<Product>() {
                new Product("Sword", 100),
                new Product("Bow", 120),
                new Product("Health bottle", 50),
                new Product("Mana bottle", 40)
            }, 0);
        private Buyer _buyer = new Buyer(280);
        private bool _isWork = true;

        private string _userInput;
        private int _userIndex;

        public void Work()
        {
            while (_isWork)
            {
                ShowMenu();

                _seller.ShowProducts();
                _userInput = GetUserInput($"Вы можете выбрать товар или ввести {CommandExit} для выхода");

                if (_userInput == CommandExit)
                {
                    _isWork = false;
                }
                else if (TryGetIndex(_userInput, out _userIndex))
                {
                    _userIndex--;

                    if (_userIndex >= 0 && _userIndex < _seller.ProductsCount)
                    {
                        Product product = _seller.GetProduct(_userIndex);

                        if (_buyer.CanBuy(product.Price))
                        {
                            _buyer.BuyProduct(product.Name, product.Price);
                            _seller.SellProduct(_userIndex, product.Price);
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно денег.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Такого товара нет.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный индекс товара.");
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            ShowBuyerInfo();
            Console.WriteLine();

            Console.WriteLine($"Добро пожаловать в магазин! У продавца сейчас {_seller.ShowMoney()} монет. " +
                $"У нас вы можете приобрести:");       
        }

        private bool TryGetIndex(string userInput, out int userIndex)
        {
            return int.TryParse(userInput, out userIndex);
        }

        private string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }

        private void ShowBuyerInfo()
        {
            Console.WriteLine($"У покупателя {_buyer.ShowMoney()} монет. В его сумке имеется:");
            _buyer.ShowProducts();
        }
    }

    class Seller : User
    {
        public Seller(List<Product> products, int money) : base(money)
        {
            for (int i = 0; i < products.Count; i++)
            {
                Products.Add(products[i]);
            }
        }

        public void SellProduct(int index, int money)
        {
            Money += money;
            Products.RemoveAt(index);
        }

        public Product GetProduct(int index)
        {
            return Products[index];
        }
    }

    class Buyer : User
    {
        public Buyer(int money) : base(money)
        {
            Money = money;
        }

        public virtual void BuyProduct(string name, int price)
        {
            Money -= price;
            Products.Add(new Product(name, price));
        }

        public bool CanBuy(int money)
        {
            return Money >= money;
        }
    }

    class User
    {
        protected List<Product> Products = new List<Product>();

        protected int Money;

        public User(int money)
        {
            Money = money;
        }

        public int ProductsCount => Products.Count;

        public virtual void ShowProducts()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                Products[i].ShowInfo();
            }
        }

        public int ShowMoney()
        {
            return Money;
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

        public void ShowInfo()
        {
            Console.WriteLine($"Товар: {Name}, цена - {Price}.");
        }
    }
}