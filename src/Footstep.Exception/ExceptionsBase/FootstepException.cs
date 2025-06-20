namespace Footstep.Exception.ExceptionsBase
{
    public abstract class FootstepException : SystemException
    {
        protected FootstepException(string message) : base(message)
        {
        }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
