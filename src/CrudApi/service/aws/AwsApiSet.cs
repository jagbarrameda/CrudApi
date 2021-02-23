using System.Collections.Generic;
using Amazon.CDK;

namespace io.jbarrameda.CrudApi.service.aws
{
    /// <summary>
    /// A IApiSet backed by AWS
    /// </summary>
    public abstract class AwsApiSet : IApiSet
    {
        protected readonly Stack Stack;
        protected List<Api> Apis { get; set; }

        protected AwsApiSet(Stack stack)
        {
            Stack = stack;
            Apis = new List<Api>();
        }

        public string Name { get; set; }
        
        public abstract void CreateResources();

        protected abstract void CreateApis();
        
        public List<Api> GetApis()
        {
            return Apis;
        }
    }
}