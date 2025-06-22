using System.Net;

namespace Footstep.Exception.ExceptionsBase
{
    public class NotFoundException : FootstepException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
