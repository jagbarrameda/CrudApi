using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace io.jbarrameda.CrudApi
{
    /**
     * A stack for the backend for a school system.
     *
     * A school has a Student crud API.
     */
    public class SchoolStack : Stack
    {
        private DdbApiService _userApiService;

        internal SchoolStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
            CreateResources(scope);
        }

        private void CreateResources(Construct scope)
        {
            _userApiService = new DdbApiService(this, "student");
        }
    }
}
