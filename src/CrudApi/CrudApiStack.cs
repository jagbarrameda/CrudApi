using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace CrudApi
{
    
    public class CrudApiStack : Stack
    {
        internal CrudApiStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
            CreateResources(scope);
        }

        private void CreateResources(Construct scope)
        {
            CreateDdb(this);
        }

        /**
         * Creates the ddb storage of the data
         */
        private void CreateDdb(Construct scope)
        {
            string tableId = "crudTable";
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
            Table t = new Table(scope, tableId, props);
        }
    }
}
