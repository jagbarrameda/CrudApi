using Amazon.CDK;

namespace io.jbarrameda.CrudApi.service.aws
{
    /// <summary>
    /// A IApiSet backed by AWS
    /// </summary>
    public abstract class AwsApiSet : IApiSet
    {
        protected readonly Stack Stack;

        protected AwsApiSet(Stack stack)
        {
            Stack = stack;
        }

        public abstract void CreateResources();
    }
}