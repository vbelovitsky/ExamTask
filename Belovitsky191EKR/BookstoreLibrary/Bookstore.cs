using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BookstoreLibrary
{
	/// <summary>
	/// Класс для магазина книг
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DataContract]
	[KnownType(typeof(Product))]
	[KnownType(typeof(Book))]
	public class Bookstore<T> : IEnumerable<T>
	{
		[DataMember]
		List<T> items;

		public Bookstore()
		{
			items = new List<T>();
		}

		public void Add(T item)
		{
			items.Add(item);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator(); 
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
