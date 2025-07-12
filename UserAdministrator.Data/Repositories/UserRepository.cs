using Microsoft.Data.SqlClient;
using System.Data;
using UserAdministrator.Data.Interfaces;
using UserAdministrator.Data.Models;

namespace UserAdministrator.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppContext _appContext;
        public UserRepository(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                List<User> userCollection = new List<User>();
                using (var connection = _appContext.GetConnection())
                {
                    using (var command = new SqlCommand("GetUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                userCollection.Add(new User
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    BirthDate = reader.GetDateTime(2),
                                    Gender = reader.GetString(3)[0]
                                });
                            }
                        }
                    }
                }

                 return userCollection;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SaveAsync(User user)
        {
            try
            {
                using (var connection = _appContext.GetConnection())
                {
                    using (var command = new SqlCommand("SaveUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@BirthDate", user.BirthDate);
                        command.Parameters.AddWithValue("@Gender", user.Gender);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                using (var connection = _appContext.GetConnection())
                {
                    using (var command = new SqlCommand("DeleteUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
