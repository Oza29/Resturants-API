using Core.users;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Core.Users.Command.UpdateUserDetails
{

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly ILogger<UpdateUserDetailsCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly IUserStore<User> _userStore;
        public UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext
            , IUserStore<User> userStore)
        {
            _logger = logger;
            _userContext = userContext;
            _userStore = userStore;
        }

        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var User = _userContext.GetCurrentUser();
            _logger.LogInformation("Updating user: {UserId}, With {@Request}", User.Id, request);
            var DbUser = await _userStore.FindByIdAsync(User.Id, cancellationToken);
            DbUser.Nationality = request.Nationality;
            DbUser.DateOfBirth = request.DateOfBirth;
            await _userStore.UpdateAsync(DbUser, cancellationToken);
        }
    }
}
