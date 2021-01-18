using System.Threading.Tasks;
using MediatR;
using Pubquiz.Domain;
using Pubquiz.Domain.Models;
using Pubquiz.Persistence;

namespace Pubquiz.Logic.Requests.Commands
{
    /// <summary>
    /// Command to login to the system.
    /// </summary>
    public class LoginCommand : Command<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginCommand(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork, mediator)
        {
        }

        protected override async Task<User> DoExecute()
        {
            var userCollection = UnitOfWork.GetCollection<User>();
            var user = await userCollection.FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == Password);

            if (user == null)
            {
                throw new DomainException(ResultCode.InvalidUserNameOrPassword, "Invalid username or password.", true);
            }

            return user;
        }
    }
}