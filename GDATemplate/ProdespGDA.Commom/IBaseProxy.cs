namespace ProdespGDA.Commom
{
    public interface IBaseProxy
    {
        string Access_Token { get; set; }
        string token_type { get; set; }
        int expires_in { get; set; }
        string ExecutionId { get; set; }
        string SessionId { get; set; }
    }
}
