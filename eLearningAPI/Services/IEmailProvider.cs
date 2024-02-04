namespace eLearningAPI.Services
{
    public interface IEmailProvider
    {
        public Task<int> SendResetCode(string to);
    }
}
