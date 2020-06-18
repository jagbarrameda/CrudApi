using Amazon.CDK;

namespace io.jbarrameda.CrudApi
{
    /**
     * An IApiServiceNode composed by one or more IApiServiceNode.
     *
     * The ApiDomainService gets data from other APIs,
     * it does not have its own storage.
     */
    public abstract class AggregatorApiService : AbstractApiService
    {
        protected AggregatorApiService(Stack stack) : base(stack)
        {
        }
    }
}