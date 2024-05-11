using System.Text.Json.Serialization;

namespace E_Commerce.Core.Entity.OrderEntities
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum PaymentStatus
	{
		Pending, Failed, Received
	}
}
