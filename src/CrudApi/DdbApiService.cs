using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace io.jbarrameda.CrudApi
{
    /**
     * An IApiServiceNode backed by ddb, a lambda and an api gateway endpoint
     */
    public class DdbApiService : AbstractApiService
    {
        private readonly string _serviceName;
        private Table _table;

        public DdbApiService(SchoolStack stack, string serviceName) : base(stack)
        {
            _serviceName = serviceName;
            CreateDdb(stack);
        }
        
        /**
         * Creates the ddb storage of the data
         */
        private void CreateDdb(Construct scope)
        {
            TableProps props = new TableProps
            {
                PartitionKey = new Attribute
                {
                    Name = "partitionKey",
                    Type = AttributeType.STRING
                },
                BillingMode = BillingMode.PAY_PER_REQUEST,
                RemovalPolicy = RemovalPolicy.DESTROY
            };
            _table = new Table(scope, TableName, props);
        }

        /**
         * Gets the name of the ddb table
         */
        private string TableName => _serviceName + "Table";
    }
}