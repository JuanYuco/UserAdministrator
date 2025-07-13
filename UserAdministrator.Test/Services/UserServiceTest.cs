using Moq;
using UserAdministrator.Api.Services;
using UserAdministrator.Data.Interfaces;
using UserAdministrator.Data.Models;

namespace UserAdministrator.Test.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task GetAll_ItMustReturnAllElements()
        {
            var mockData = new List<Data.Models.User>
                {
                    new Data.Models.User {  Id = 1, BirthDate = DateTime.Today.AddYears(-5).Date, Gender = 'M', Name = "Juan Fernando" },
                    new Data.Models.User {  Id = 2, BirthDate = DateTime.Today.AddYears(-7).Date, Gender = 'F', Name = "Lorena Jimenez" },
                    new Data.Models.User {  Id = 3, BirthDate = DateTime.Today.AddYears(-10).Date, Gender = 'M', Name = "Henry Jimenez" },
                };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(rep => rep.GetAllAsync())
                .ReturnsAsync(mockData);

            var userService = new UserService(userRepositoryMock.Object);

            var result = await userService.GetAllAsync();
            Assert.True(result.Successful, "An error occurred consulting the information.");

            var userCollectionResult = result.EntityCollection;
            Assert.Equal(mockData.Count, userCollectionResult.Count);
            foreach (var mockItem in mockData)
            {
                var user = userCollectionResult.FirstOrDefault(x => x.Id == mockItem.Id);
                Assert.NotNull(user);
                Assert.Equal(mockItem.Name, user.Name);
                Assert.Equal(mockItem.BirthDate, user.BirthDate);
                Assert.Equal(mockItem.Gender, user.Gender);
            }

            userRepositoryMock.Verify(rep => rep.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_ItMustSaveRecord()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock
                .Setup(rep => rep.SaveAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var userService = new UserService(userRepositoryMock.Object);

            var userDTO = new Api.DTOS.CreateUserRequestDTO { Name = "Juan Fernando", BirthDate = DateTime.Today.AddYears(-5).Date, Gender = 'M' };
            var result = await userService.CreateAsync(userDTO);

            Assert.True(result.Successful, "An error occurred saving the user.");
            userRepositoryMock.Verify(rep => rep.SaveAsync(It.Is<User>(u =>
                u.Name == userDTO.Name &&
                u.BirthDate == userDTO.BirthDate &&
                u.Gender == userDTO.Gender
            )), Times.Once);
        }

        [Fact]
        public async Task Create_SendingInvalidRequest_ItMustFail()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock
                .Setup(rep => rep.SaveAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var userService = new UserService(userRepositoryMock.Object);

            var userDTO = new Api.DTOS.CreateUserRequestDTO { Name = "", BirthDate = DateTime.Today.AddYears(1).Date };
            var result = await userService.CreateAsync(userDTO);

            Assert.True(!result.Successful, "It didn't fail and it must fail because it received an invalid request.");

            userRepositoryMock.Verify(rep => rep.SaveAsync(It.Is<User>(u =>
                u.Name == userDTO.Name &&
                u.BirthDate == userDTO.BirthDate
            )), Times.Never);
        }

        [Fact]
        public async Task Update_ItMustSaveRecord()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock
                .Setup(rep => rep.SaveAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var userService = new UserService(userRepositoryMock.Object);

            var request = new Api.DTOS.UpdateUserRequestDTO { Id = 1, Name = "Juan Fernando", BirthDate = DateTime.Today.AddYears(-5).Date, Gender = 'M' };
            var result = await userService.UpdateAsync(request);

            Assert.True(result.Successful, "An error occurred saving the user.");
            userRepositoryMock.Verify(rep => rep.SaveAsync(It.Is<User>(u =>
                u.Id == request.Id &&
                u.Name == request.Name &&
                u.BirthDate == request.BirthDate &&
                u.Gender == request.Gender
            )), Times.Once);
        }

        [Fact]
        public async Task Update_SendingInvalidRequest_ItMustFail()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock
                .Setup(rep => rep.SaveAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var userService = new UserService(userRepositoryMock.Object);

            var request = new Api.DTOS.UpdateUserRequestDTO { Id = -1, Name = "", BirthDate = DateTime.Today.AddYears(5).Date };
            var result = await userService.UpdateAsync(request);

            Assert.True(!result.Successful, "It didn't fail and it must fail because it received an invalid request.");

            userRepositoryMock.Verify(rep => rep.SaveAsync(It.Is<User>(u =>
                u.Id == request.Id && 
                u.Name == request.Name &&
                u.BirthDate == request.BirthDate
            )), Times.Never);
        }

        [Fact]
        public async Task Delete_ItMustDeleteRecord()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            int id = 1;
            userRepositoryMock
                .Setup(rep => rep.DeleteAsync(id))
                .Returns(Task.CompletedTask);

            var userService = new UserService(userRepositoryMock.Object);
            var result = await userService.DeleteAsync(id);

            Assert.True(result.Successful, "An error occurred deleting the user.");
            userRepositoryMock.Verify(rep => rep.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task Delete_SendingInvalidRequest_ItMustFail()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            int id = -5;
            userRepositoryMock
                .Setup(rep => rep.DeleteAsync(id))
                .Returns(Task.CompletedTask);

            var userService = new UserService(userRepositoryMock.Object);
            var result = await userService.DeleteAsync(id);

            Assert.True(!result.Successful, "It didn't fail and it must fail because it received an invalid request.");
            userRepositoryMock.Verify(rep => rep.DeleteAsync(id), Times.Never);
        }
    }
}
