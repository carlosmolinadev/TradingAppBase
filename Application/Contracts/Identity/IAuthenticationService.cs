
using Application.Models.Authentication;
using GloboTicket.TicketManagement.Application.Models.Authentication;

namespace Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
