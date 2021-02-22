using Amazon.CDK;

namespace io.jbarrameda.CrudApi.service.aws
{
    /**
     * An IApiServiceNode composed by one or more IApiServiceNode.
     *
     * Use this service to build a service that gets and processes data from other services.
     * It does not have its own storage.
     */
    public abstract class AggregatorAwsApiSet : AwsApiSet
    {
        protected AggregatorAwsApiSet(Stack stack) : base(stack)
        {
        }
    }
}