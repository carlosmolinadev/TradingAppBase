using Application.Contracts.Persistance;
using Application.Responses;
using Domain.Entities;

namespace Application.Features.Commands
{
    public class AccountCommand 
    {
        private readonly IRepositoryAsync<@int> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountCommand(IRepositoryAsync<@int> accountRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponse> CreateAccount(@int updateAccount)
        {
            var response = new AccountResponse();
            try
            {
                using (_unitOfWork.BeginTransactionAsync())
                {
                    var accountId = await _accountRepository.AddAsync(updateAccount);

                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<AccountResponse> UpdateAccount(int accountId, @int updateAccount)
        {
            var response = new AccountResponse();
            try
            {
                using (_unitOfWork.BeginTransactionAsync())
                {
                    await _accountRepository.UpdateAsync(updateAccount);
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<AccountResponse> DeleteAccount(@int updateAccount)
        {
            var response = new AccountResponse();
            try
            {
                using (_unitOfWork.BeginTransactionAsync())
                {
                    await _accountRepository.DeleteAsync(updateAccount);
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }


    }
}
