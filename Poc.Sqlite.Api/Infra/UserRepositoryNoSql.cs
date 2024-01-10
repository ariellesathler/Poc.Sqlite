using LiteDB;
using Poc.Sqlite.Api.Domain;
using System.Data;

namespace Poc.Sqlite.Api.Infra
{
	public class UserRepositoryNoSql : IUserRepository
	{
		private readonly ILiteDatabase _database;
		private readonly ILiteCollection<User> _users;

		public UserRepositoryNoSql(ILiteDatabase database)
		{
			_database = database;
			_users = _database.GetCollection<User>("users");
		}

		public Task Delete(int id)
		{
			return Task.FromResult(_users.Delete(id));
		}

		public Task<IEnumerable<User>> GetAll()
		{
			return Task.FromResult(_users.FindAll());
		}

		public Task<User> GetById(int id)
		{
			return Task.FromResult(_users.FindById(id));
		}

		public Task Insert(User user)
		{
			return Task.FromResult(_users.Insert(user));
		}

		public Task Update(User user)
		{
			return Task.FromResult(_users.Update(user));
		}
	}
}
