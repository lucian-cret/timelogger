using TimeLogger.Application.Common.Interfaces;

namespace TimeLogger.UI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
        public string Name => "Freelancer 1";
    }
}
