namespace Poc.Sqlite.Api.Domain
{
	public class User
	{
		protected User() { }
		public User(string firstName, string lastName, string email, int role)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Role = role;
		}

		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public int Role { get; set; } = 0;
	}

}
