namespace Poc.Sqlite.Api.Domain
{
	public interface IUserRepository
    {
		Task<User> GetById(int id);
		Task<IEnumerable<User>> GetAll();
		Task Insert(User user);
		Task Delete(int id);
		Task Update(User user);
    }
}
