using UserAdministrator.Api.DTOS;

namespace UserAdministrator.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserCollectionResponseDTO> GetAllAsync();
        Task<CreateUserResponseDTO> CreateAsync(CreateUserRequestDTO request);
        Task<UpdateUserResponseDTO> UpdateAsync(UpdateUserRequestDTO request);
        Task<DeleteUserResponseDTO> DeleteAsync(int id);
    }
}
