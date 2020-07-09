using System;
using System.Runtime.Serialization;

namespace BookstoreLibrary
{
	/// <summary>
	/// Класс для продукта
	/// </summary>
	[DataContract]
	public class Product : IComparable<Product>
	{
		double price;
		[DataMember]
		public double Price
		{	
			set
			{
				if(value <= 0)
				{
					throw new ProductException("Цена должна быть положительной");
				}
				price = value;
			}
			get
			{
				return price;
			}
		}

		string title;
		[DataMember]
		public string Title
		{
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ProductException("Имя не должно быть пустым или null");
				}
				title = value;
			}
			get
			{
				return title;
			}
		}

		public Product(double price, string title)
		{
			Price = price;
			Title = title;
		}

		public override string ToString()
		{
			return $"Price = {Price:F2}";
		}

		public static explicit operator double(Product product)
		{
			return product.Price;
		}

		public int CompareTo(Product other)
		{
			return Price.CompareTo(other.Price);
		}
	}
}
