using System;

namespace BookstoreLibrary
{

	[Serializable]
	public class ProductException : Exception
	{
		public ProductException() { }
		public ProductException(string message) : base(message) { }
		public ProductException(string message, Exception inner) : base(message, inner) { }
		protected ProductException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
