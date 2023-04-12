using Domain.Entities;

namespace Application.Responses
{
    public class AccountResponse : BaseResponse
    {
        public Account Account { get; set; }
    }
}