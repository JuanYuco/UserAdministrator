using Azure.Core;
using UserAdministrator.Api.DTOS;
using UserAdministrator.Api.Services.Interfaces;
using UserAdministrator.Data.Interfaces;

namespace UserAdministrator.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
            this._userRepository = userRepository;
        }

        public async Task<UserCollectionResponseDTO> GetAllAsync()
        {
            var result = new UserCollectionResponseDTO() { Successful = false };

            try
            {
                List<UserDTO> userDTOCollection = new List<UserDTO>();
                var userCollection = await _userRepository.GetAllAsync();
                if (userCollection != null && userCollection.Count > 0)
                {
                    foreach (var user in userCollection)
                    {
                        userDTOCollection.Add(new UserDTO
                        {
                            Id = user.Id,
                            Name = user.Name,
                            BirthDate = user.BirthDate,
                            Gender = user.Gender
                        });
                    }
                }

                result.EntityCollection = userDTOCollection;
                result.Successful = true;
            }
            catch (Exception ex)
            {
                result.UserMessage = "Ocurrió un error consultando la información de los usuarios.";
                result.InternalMessage = ex.Message;
                result.StatusCode = 500;
            }

            return result;
        }

        public async Task<CreateUserResponseDTO> CreateAsync(CreateUserRequestDTO request)
        {
            var result = new CreateUserResponseDTO() { Successful = false };

            try
            {
                var message = UserValidation(request.Name, request.Gender, request.BirthDate);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    result.UserMessage = message;
                    result.StatusCode = 404;
                    return result;
                }

                await _userRepository.SaveAsync(new Data.Models.User
                {
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender
                });

                result.UserMessage = "El usuario fue creado satisfactoriamente.";
                result.Successful = true;
            } catch(Exception ex)
            {
                result.UserMessage = "Ocurrió un error creando el usuario.";
                result.InternalMessage = ex.Message;
                result.StatusCode = 500;
            }

            return result;
        }

        public async Task<UpdateUserResponseDTO> UpdateAsync(UpdateUserRequestDTO request)
        {
            var result = new UpdateUserResponseDTO() { Successful = false };

            try
            {
                var message = UserValidation(request.Name, request.Gender, request.BirthDate);
                if (request.Id <= 0)
                {
                    message = $"{message}Se envió un identificador invalido.";
                }

                if (!string.IsNullOrWhiteSpace(message))
                {
                    result.UserMessage = message;
                    result.StatusCode = 404;
                    return result;
                }

                await _userRepository.SaveAsync(new Data.Models.User
                {
                    Id = request.Id,
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender
                });

                result.UserMessage = "El usuario fue actualizado satisfactoriamente.";
                result.Successful = true;
            }
            catch (Exception ex)
            {
                result.UserMessage = "Ocurrió un error actualizando el usuario.";
                result.InternalMessage = ex.Message;
                result.StatusCode = 500;
            }

            return result;
        }

        private string UserValidation(string name, char gender, DateTime birthDate)
        {
            string message = "";
            if (string.IsNullOrEmpty(name))
            {
                message = "El nombre del usuario es obligatorio. ";
            }

            var today = DateTime.Today;
            if (birthDate.Date > today.Date)
            {
                message = $"{message}La fecha de nacimiento no pude ser mayor a la fecha de hoy.";
            }

            if (string.IsNullOrEmpty(gender.ToString()))
            {
                message = $"{message}El género es obligatorio. ";
            }

            if (gender != 'M' && gender != 'F')
            {
                message = $"{message}El género es invalido.";
            }

            return message;
        }

        public async Task<DeleteUserResponseDTO> DeleteAsync(int id)
        {
            var result = new DeleteUserResponseDTO() { Successful = true };
            
            try
            {
                if (id <= 0)
                {
                    result.UserMessage = $"Se envió un identificador invalido.";
                    return result;
                }

                await _userRepository.DeleteAsync(id);

                result.UserMessage = "El usuario fue eliminado satisfactoriamente.";
                result.Successful = true;
            } catch (Exception ex)
            {
                result.UserMessage = "Ocurrió un error eliminando el usuario.";
                result.InternalMessage = ex.Message;
                result.StatusCode = 500;
            }

            return result;
        }
    }
}
