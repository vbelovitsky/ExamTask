using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BookstoreLibrary
{
	/// <summary>
	/// Класс для книг
	/// </summary>
	[DataContract]
    public class Book : Product
    {
		short numberOfPages;
		[DataMember]
		public short NumberOfPages
		{
			set
			{
				if(value <= 0)
				{
					throw new ProductException("Количество страниц должно быть положительным");
				}
				numberOfPages = value;
			}
			get
			{
				return numberOfPages;
			}
		}

		short year;
		[DataMember]
		public short Year
		{
			set
			{
				if(value < 1990 || value > 2020)
				{
					throw new ProductException("Значение года может быть в диапазоне [1990, 2020]");
				}
				year = value;
			}
			get
			{
				return year;
			}
		}

		double rating;
		[DataMember]
		public double Rating
		{
			set
			{
				if (value < 0 || value >= 5)
				{
					throw new ProductException("Значение рейтинга может быть в диапазоне [0, 0.5)");
				}
				rating = value;
			}
			get
			{
				return rating;
			}
		}

		public Book(double price, string title, short num, short year, double rating) : base(price, title)
		{
			NumberOfPages = num;
			Year = year;
			Rating = rating;
		}

		/// <summary>
		/// Возвращает краткую информацию о книге
		/// </summary>
		/// <returns></returns>
		public string GetShortInfo()
		{
			HashSet<char> chars = new HashSet<char>(Title.ToArray());

			string info = $"{NumberOfPages}.{Year}.{chars.Count}.{Rating.ToString("F2").Replace(",", "").Replace(".", "")}";

			return info;
		}

		public override string ToString()
		{
			return base.ToString() + $" Title = {Title}, NumberOfPages = {NumberOfPages}, Year = {Year}, Rating = {Rating:F4} "+ GetShortInfo();
		}
	}
}
