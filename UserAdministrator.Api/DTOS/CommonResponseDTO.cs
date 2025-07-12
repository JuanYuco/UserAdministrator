namespace UserAdministrator.Api.DTOS
{
    public class CommonResponseBaseDTO
    {
        public bool Successful { get; set; }
        public string UserMessage { get; set; }
        public string InternalMessage { get; set; }
        public int StatusCode { get; set; }
    }

    public class CommonCollectionResponseDTO<TEntity> : CommonResponseBaseDTO
    {
        public ICollection<TEntity> EntityCollection { get; set; }
    }
}
