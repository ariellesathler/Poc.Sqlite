using Dapper;
using Poc.Sqlite.Api.Domain;
using System.Collections.Generic;
using System.Data;

namespace Poc.Sqlite.Api.Infra
{
	public class UserRepository : IUserRepository
	{
		private readonly IDbConnection _connection;
		public UserRepository(IDbConnection connection)
		{
			_connection = connection;

			//Alterar para criar a tabela no start da apliação
			var createTable = @"
			CREATE TABLE IF NOT EXISTS	
			Users(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
						   FirstName TEXT,
						   LastName TEXT,
						   Email TEXT,
						   Role INTEGER,
						   PasswordHash TEXT
					   );
		";
			_connection.Execute(createTable);
		}

		public async Task Delete(int id)
		{
			var delete = @" DELETE FROM Users WHERE Id = @id";
			await _connection.ExecuteAsync(delete, new { id });
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			var select = @"
					SELECT * 
					FROM Users
					";
			return await _connection.QueryAsync<User>(select);
		}

		public async Task<User> GetById(int id)
		{
			var select = @"
					SELECT * 
					FROM Users
					WHERE Id = @id
					";
			return (await _connection.QueryAsync<User>(select, new { id })).First();
		}

		public async Task Insert(User user)
		{
			var insert = @"
				INSERT INTO Users (FirstName, LastName, Email, Role)
				VALUES (@FirstName, @LastName, @Email, @Role)
			";

			await _connection.ExecuteAsync(insert, new { user });
		}

		public async Task Update(User user)
		{
			var update = @"
					UPDATE Users
						SET FirstName = @FirstName,
						LastName = @LastName,
						Email = @Email,
						Role = @Role
					Where Id = @Id
					";
			await _connection.ExecuteAsync(update, new { user });
		}
	}
}
