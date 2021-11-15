namespace SiT.Scheduler.Utilitites.Errors
{
    public class StandardError : IError
    {
        public StandardError(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }
    }
}
