using System;
using System.IO;
using System.Text;
using BookstoreLibrary;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ConsoleApp1
{
	class Program
	{

		static Random rnd = new Random();
		static string alphabet = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM ";

		static string fileName = "../../../books.json";
		
		/// <summary>
		/// Считывает натуральное цисло
		/// </summary>
		/// <returns></returns>
		static int ReadInt()
		{
			int num;
			Console.WriteLine("Введите размер коллекции: ");
			while (!(int.TryParse(Console.ReadLine(), out num) && num > 0 && num <= int.MaxValue))
			{
				Console.WriteLine("Неверный ввод, заново: ");
			}
			return num;
		}

		/// <summary>
		/// Наполняет магазин книгами
		/// </summary>
		/// <param name="books"></param>
		/// <param name="n">количество книг</param>
		static void FillBooks(ref Bookstore<Product> books, int n)
		{
			for(int i = 0; i < n; i++)
			{
				try
				{
					double price = rnd.Next(0, 20) + rnd.NextDouble();
					short numberOfPages = (short)rnd.Next(0, 701);
					short year = (short)rnd.Next(1980, 2030);
					string title = GenerateTitle();
					double rating = rnd.Next(-2, 7) + rnd.NextDouble();

					Book book = new Book(price, title, numberOfPages, year, rating);
					books.Add(book);
				}
				catch (ProductException)
				{
					i--;
				}
		}

			books.Add(new Product(1, "Товар1"));

		}

		/// <summary>
		/// Генерирует название
		/// </summary>
		/// <returns></returns>
		static string GenerateTitle()
		{
			StringBuilder title = new StringBuilder();
			int len = rnd.Next(3, 16);
			for(int i = 0; i < len; i++)
			{
				title.Append(alphabet[rnd.Next(0, alphabet.Length)]);
			}
			return title.ToString();
		}

		static void PrintBooks(Bookstore<Product> books)
		{
			foreach(var book in books)
			{
				Console.WriteLine(book.ToString());
			}
		}


		/// <summary>
		/// Метод для Json сериализации
		/// </summary>
		/// <param name="books"></param>
		static void SerializeBooks(Bookstore<Product> books)
		{
			// Можно было использовать newtonsoft.json
			try
			{
				using (FileStream fs = new FileStream(fileName, FileMode.Create))
				{
					DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Bookstore<Product>));
					serializer.WriteObject(fs, books);
				}
			}
			catch (SerializationException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Main(string[] args)
		{
			do
			{
				Console.Clear();

				Bookstore<Product> books = new Bookstore<Product>();
				try
				{
					int N = ReadInt();

					FillBooks(ref books, N);

					PrintBooks(books);

					SerializeBooks(books);
				}
				catch (OutOfMemoryException)
				{
					// В случае, если введено слишком большое N
					Console.WriteLine("Попытка создать слишком большую коллекцию");
				}
				catch (Exception)
				{
					Console.WriteLine("Непредвиденная ошибка");
				}

				Console.WriteLine("Для выхода Esc: ");
			}
			while (Console.ReadKey(true).Key != ConsoleKey.Escape);
		}
	}
}
