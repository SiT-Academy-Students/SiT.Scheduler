namespace SiT.Scheduler.Utilities.Errors
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
