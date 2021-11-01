namespace SiT.Scheduler.Utilitites.Errors
{
    public class StandardError : IError
    {
        public StandardError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
