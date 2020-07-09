using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using BookstoreLibrary;

namespace ConsoleApp2
{
	class Program
	{
		static string fileName = "../../../books.json";

		static void DeserializeBooks(out Bookstore<Product> books)
		{
			books = null;
			try
			{
				using (FileStream fs = new FileStream(fileName, FileMode.Open))
				{
					DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Bookstore<Product>));
					books = (Bookstore<Product>)serializer.ReadObject(fs);
				}
			}
			catch(ProductException e)
			{
				Console.WriteLine(e.Message);
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

		static void PrintBooks(Bookstore<Product> books)
		{
			foreach (var book in books)
			{
				Console.WriteLine(book.ToString());
			}
		}

		static void Main(string[] args)
		{

			do
			{
				Console.Clear();

				Bookstore<Product> books;
				try
				{
					DeserializeBooks(out books);
					if(books != null)
					{
						Console.WriteLine("Вывод коллеции");
						PrintBooks(books);
						Console.WriteLine();

						///Можно было использовать синтаксис запросов
						Console.WriteLine("Первый запрос:");
						IEnumerable<Product> linq1 = books.Where(b => b is Book && ((Book)b).GetShortInfo().Length > 14).OrderByDescending(x => (double)x);
						foreach(var book in linq1)
						{
							Console.WriteLine(book.ToString());
						}
						Console.WriteLine();

						Console.WriteLine("Второй запрос:");
						IEnumerable<IGrouping<int, Product>> linq2 = books.Where(b => b is Book).GroupBy(b => (int)((Book)b).Rating).OrderBy(g => g.Key);
						foreach(var group in linq2)
						{
							Console.WriteLine(group.Key);
							foreach(var book in group.OrderBy(b => (double)b))
							{
								Console.WriteLine(book.ToString());
							}
						}
						Console.WriteLine();

						Console.WriteLine("Третий запрос:");
						IEnumerable<Product> linq3 = books.Where(b => b is Book).Where(x => ((Book)x).Year == books.Where(r => r is Book).Select(y => ((Book)y).Year).Max());
						foreach(var book in linq3)
						{
							Console.WriteLine(book.ToString());
						}
						Console.WriteLine(linq3.Count());
					}
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
