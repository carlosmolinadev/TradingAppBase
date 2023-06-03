using Application.Contracts.Persistance;
using Application.Responses;
using Domain.Entities;

namespace Application.Features.Queries
{
    public class AccountQuery 
    {
        private readonly IRepositoryAsync<@int> _accountRepo;
        public AccountQuery(IRepositoryAsync<@int> accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<AccountResponse> LoadAccountInformation(int accountId)
        {
            var accountResponse = new AccountResponse();
            try
            {
                var account = await _accountRepo.SelectByIdAsync(accountId);

                if (account != null)
                {
                    accountResponse.Account = account;
                    accountResponse.Success = true;
                }
                return accountResponse;
            }
            catch (Exception e)
            {
                accountResponse.Message = e.Message;
                throw;
            }
        }
    }
}
