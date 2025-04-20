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
        private Seller _seller = new Seller(new List<Product>() {
                new Product("Sword", 100),
                new Product("Bow", 120),
                new Product("Health bottle", 50),
                new Product("Mana bottle", 40)
            }, 0);
        private Buyer _buyer = new Buyer(280);

        public void Work()
        {
            const string CommandExit = "Exit";

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                ShowMenu();

                _seller.ShowProducts();
                userInput = GetUserInput($"Вы можете выбрать товар или ввести {CommandExit} для выхода");

                if (userInput == CommandExit)
                {
                    isWork = false;
                }
                else if (int.TryParse(userInput, out int userIndex))
                {
                    userIndex--;

                    Product product;

                    if(_seller.TryGetProduct(userIndex, out product))
                        if(_buyer.CanBuy(product.Price))
                        {
                            _buyer.BuyProduct(product);
                            _seller.SellProduct(product, userIndex);
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

            Console.WriteLine($"Добро пожаловать в магазин! У продавца сейчас {_seller.Money} монет. " +
                $"У нас вы можете приобрести:");       
        }

        private string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }

        private void ShowBuyerInfo()
        {
            Console.WriteLine($"У покупателя {_buyer.Money} монет. В его сумке имеется:");
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

        public void SellProduct(Product product, int index)
        {
            Money += product.Price;
            Products.RemoveAt(index);
        }

        public bool TryGetProduct(int index, out Product product)
        {
            if (index >= 0 && index < Products.Count)
            {
                product = Products[index];
                return true;
            }
            else
            {
                product = null;
                Console.WriteLine("Такого товара нет.");
                return false;
            }
        }
    }

    class Buyer : User
    {
        public Buyer(int money) : base(money)
        {
            Money = money;
        }

        public void BuyProduct(Product product)
        {
            Money -= product.Price;
            Products.Add(product);
        }

        public bool CanBuy(int money)
        {
            if (Money >= money)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Недостаточно денег.");
                return false;
            }
        }
    }

    class User
    {
        protected List<Product> Products = new List<Product>();

        public User(int money)
        {
            Money = money;
        }

        public int Money { get; protected set; }

        public int ProductsCount => Products.Count;

        public virtual void ShowProducts()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                Products[i].ShowInfo();
            }
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