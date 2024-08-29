namespace EmaAPI.Models.Token
{
	public class GenerateTokenRequest
	{
		/// <summary>Gets or sets the username.</summary>
		/// <value>The username.</value>
		public string Username { get; set; }
		public int? RecordId { get; set; }
	}
}
