using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class UserRegistrationCommand : IRequest<Unit>
    {
        public UserRegistrationCommand(User user)
        {
            this.User = user;
        }

        public User User { get; set; }
        public class Handler : IRequestHandler<UserRegistrationCommand, Unit>
        {
            private readonly IDbContext dbContext;
            public Handler(IDbContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async Task<Unit> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
            {
                var users = dbContext.Users;
                var entity = new Domain.Entities.User
                {
                    FirstName = request.User.FirstName,
                    LastName = request.User.LastName,
                    Email = request.User.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password),
                    Age = request.User.Age
                };
                users.Add(entity);
                await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
                return Unit.Value;
            }

        }
    }
}
