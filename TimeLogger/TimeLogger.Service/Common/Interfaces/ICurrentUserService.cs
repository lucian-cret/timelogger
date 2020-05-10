namespace TimeLogger.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Name { get; }
    }
}
