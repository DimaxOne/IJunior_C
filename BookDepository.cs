using System;
using System.Collections.Generic;

namespace BookDepository
{
    class Program
    {
        static void Main(string[] args)
        {
            Depository depository = new Depository();

            depository.Work();
        }
    }

    class Depository
    {
        private const string CommandAddBook = "1";
        private const string CommandRemoveBook = "2";
        private const string CommandShowBooks = "3";
        private const string CommandSearchBook = "4";
        private const string CommandExit = "5";

        private List<Book> _books = new List<Book>()
        {
            new Book("Евгений Онегин", "Пушкин А.С.", 1838),
            new Book("Преступление и наказание", "Достоевский Ф.М.", 1866),
            new Book("Анна Каренина", "Толстой Л.Н.", 1877)
        };
        private bool _isWork = true;

        public void Work()
        {
            while (_isWork)
            {
                ShowMenu();

                switch (GetUserInput("Ваша команда"))
                {
                    case CommandAddBook:
                        AddBook();
                        break;

                    case CommandRemoveBook:
                        RemoveBook();
                        break;

                    case CommandShowBooks:
                        ShowBooks();
                        break;

                    case CommandSearchBook:
                        SearchBook();
                        break;

                    case CommandExit:
                        _isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена некорректная команда.");
                        break;
                }

                Console.WriteLine("Нажмите любую кнопку для продолжения.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void AddBook()
        {
            int userNumber;

            if (TryGetNumber("Введите год издания книги", out userNumber))
            {
                _books.Add(new Book(GetUserInput("Введите название книги"), GetUserInput("Введите автора книги"),
                userNumber));
                Console.WriteLine("Книга добавлена.");
            }
            else
            {
                Console.WriteLine("Некорректные данные по книге.");
            }
        }

        private void RemoveBook()
        {
            int index;

            if (TryGetNumber("Введите индекс удаляемой книги", out index))
            {
                index--;

                if (index >= 0 && index < _books.Count)
                {
                    _books.RemoveAt(index);
                    Console.WriteLine("Книга удалена.");
                }
                else
                {
                    Console.WriteLine("Указан некорректный индекс.");
                }
            }
            else
            {
                Console.WriteLine("Некорректное число.");
            }
        }

        private void ShowBooks()
        {
            for (int i = 0; i < _books.Count; i++)
            {
                Console.WriteLine(i + 1 + " - " + _books[i].ShowInfo());
            }
        }

        private void SearchBook()
        {
            const string CommandSearchByName = "1";
            const string CommandSearchByAuthor = "2";
            const string CommandSearchByYearOfIssue = "3";

            Console.WriteLine($"Поиск доступен по следующим параметрам:" +
                $"\n{CommandSearchByName} - поиск по названию книги;" +
                $"\n{CommandSearchByAuthor} - поиск по автору;" +
                $"\n{CommandSearchByYearOfIssue} - поиск по году издания.");

            switch (GetUserInput("Ваша команда"))
            {
                case CommandSearchByName:
                    SearchByName(GetUserInput("Введите полное название книги"));
                    break;

                case CommandSearchByAuthor:
                    SearchByAuthor(GetUserInput("Введите полное имя автора"));
                    break;

                case CommandSearchByYearOfIssue:
                    SearchByYearOfIssue();
                    break;

                default:
                    Console.WriteLine("Введена некорректная команда.");
                    break;
            }
        }

        private void SearchByName(string name)
        {
            bool isSearchSuccessful = false;

            foreach (Book book in _books)
            {
                if (book.Name == name)
                {
                    Console.WriteLine(book.ShowInfo());
                    isSearchSuccessful = true;
                }
            }

            if(isSearchSuccessful == false)
                Console.WriteLine("Такой книги нет.");
        }

        private void SearchByAuthor(string author)
        {
            bool isSearchSuccessful = false;

            foreach (Book book in _books)
            {
                if (book.Author == author)
                {
                    Console.WriteLine(book.ShowInfo());
                    isSearchSuccessful = true;
                }
            }

            if (isSearchSuccessful == false)
                Console.WriteLine("Такого автора нет.");
        }

        private void SearchByYearOfIssue()
        {
            bool isSearchSuccessful = false;

            if (TryGetNumber("Введите год издания книги", out int userYearOfIssue))
            {
                foreach (Book book in _books)
                {
                    if (book.YearOfIssue == userYearOfIssue)
                    {
                        Console.WriteLine(book.ShowInfo());
                        isSearchSuccessful = true;
                    }
                }

                if (isSearchSuccessful == false)
                    Console.WriteLine("Книги с таким годом издания нет.");
            }
            else
            {
                Console.WriteLine("Введен некорректный год.");
            } 
        }

        private bool TryGetNumber(string message, out int number)
        {
            bool isNumber = false;
            int inputNumber = 0;

            if (int.TryParse(GetUserInput(message), out int userNumber))
            {
                inputNumber = userNumber;
                isNumber = true;
            }

            number = inputNumber;

            return isNumber;
        }

        private void ShowMenu()
        {
            Console.WriteLine($"Управление библиотекой:" +
                    $"\n{CommandAddBook} - Добавить книгу;" +
                    $"\n{CommandRemoveBook} - Убрать книгу;" +
                    $"\n{CommandShowBooks} - Показать все книги;" +
                    $"\n{CommandSearchBook} - Найти книги по указанному параметру;" +
                    $"\n{CommandExit} - Выход.");
        }

        private string GetUserInput(string message)
        {
            Console.Write(message + ": ");

            return Console.ReadLine();
        }
    }

    class Book
    {
        public Book(string name, string author, int yearOfIssue)
        {
            Name = name;
            Author = author;
            YearOfIssue = yearOfIssue;
        }

        public string Name { get; private set; }
        public string Author { get; private set; }
        public int YearOfIssue { get; private set; }

        public string ShowInfo()
        {
            return $"{Name}, автор {Author}, год издания {YearOfIssue}.";
        }
    }
}