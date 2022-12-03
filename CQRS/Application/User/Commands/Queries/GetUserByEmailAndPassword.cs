using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.Commands.Queries
{
    public class GetUserByEmailAndPassword : IRequest<User>
    {
        public GetUserByEmailAndPassword(User user)
        {
            this.User = user;
        }
        public User User { get; set; }

        public class Handler : IRequestHandler<GetUserByEmailAndPassword, User>
        {
            private readonly IDbContext dbContext;
            public Handler(IDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<User> Handle(GetUserByEmailAndPassword request, CancellationToken cancellationToken)
            {

                var entity = dbContext.Users.FirstOrDefault(u => u.Email == request.User.Email);
                var password = entity.Password;
                bool verified = BCrypt.Net.BCrypt.Verify(request.User.Password, password);
                var user = new User();
                if (verified && entity !=null)
                {
                    user.Age = entity.Age;
                    user.Email = entity.Email;
                    user.FirstName = entity.FirstName;
                    user.LastName = entity.LastName;
                    user.Password = entity.Password;
                    return user;
                }
                return null;
            }
        }
    }
}
