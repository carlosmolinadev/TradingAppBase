using Application.Contracts.Persistance;
using Application.Responses;
using Domain.Entities;
using System.Diagnostics;

namespace Application.Features.Commands
{
    public class AccountCommand 
    {
        private readonly IRepositoryAsync<Account> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountCommand(IRepositoryAsync<Account> accountRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public async Task<AccountResponse> CreateAccount(Account updateAccount)
        {
            var response = new AccountResponse();
            try
            {
                using (_unitOfWork.BeginTransactionAsync())
                {
                    var accountId = await _accountRepository.InsertAsync(updateAccount);

                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<AccountResponse> UpdateAccount(int accountId, Account updateAccount)
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

        public async Task<AccountResponse> DeleteAccount(Account updateAccount)
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
