using Common.DTOs;


namespace Application.Common.Contracts
{
    public interface ITokenService
    {
        Task<Result> GetToken(string userId);
    }
}
