namespace UserAdministrator.Api.DTOS
{
    public class CreateUserRequestDTO
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
    }
}
