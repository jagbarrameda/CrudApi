using Amazon.CDK;

namespace io.jbarrameda.CrudApi
{
    public abstract class AbstractApiService : IApiService
    {
        protected readonly Stack Stack;

        protected AbstractApiService(Stack stack)
        {
            Stack = stack;
        }
    }
}