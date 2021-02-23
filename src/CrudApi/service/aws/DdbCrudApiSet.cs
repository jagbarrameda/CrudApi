using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace io.jbarrameda.CrudApi.service.aws
{
    /**
     * A CRUD ApiSet backed by ddb, a lambda and an api gateway endpoint
     */
    public class DdbCrudApiSet : AwsApiSet
    {
        private readonly string _serviceName;
        private Table _table;

        /**
         * Gets the name of the ddb table
         */
        private string TableName => _serviceName + "Table";

        public DdbCrudApiSet(Stack stack, string serviceName) : base(stack)
        {
            _serviceName = serviceName;
        }

        public override void CreateResources()
        {
            CreateDdb(Stack);
            CreateLambda(Stack);
            CreateApiEndpoints(Stack);
            CreateApis();
        }

        protected override void CreateApis()
        {
            Apis.Add(GetCreateApi());
            Apis.Add(GetReadApi());
        }

        /// <summary>
        /// Gets the Create api
        /// </summary>
        /// <returns></returns>
        private Api GetCreateApi()
        {
            var metrics = new List<DashboardMetric>
            {
                new()
                {
                    Name = "Availability", Visible = true
                },
                new()
                {
                    Name = "Latency", Visible = true
                },
                new()
                {
                    Name = "Throughput", Visible = true
                }
            };
            var api = new Api
            {
                Name = "Create", DashboardMetrics = metrics
            };
            return api;
        } 
        
        /// <summary>
        /// Gets the Create api
        /// </summary>
        /// <returns></returns>
        private Api GetReadApi()
        {
            var metrics = new List<DashboardMetric>
            {
                new()
                {
                    Name = "Availability", Visible = true
                },
                new()
                {
                    Name = "Latency", Visible = true
                },
                new()
                {
                    Name = "Throughput", Visible = true
                }
            };
            var api = new Api
            {
                Name = "Read", DashboardMetrics = metrics
            };
            return api;
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
                    Name = "userId",
                    Type = AttributeType.STRING
                },
                BillingMode = BillingMode.PAY_PER_REQUEST,
                RemovalPolicy = RemovalPolicy.DESTROY
            };
            _table = new Table(scope, TableName, props);
        }

        /// <summary>
        /// Creates the lambdas for the crud operations
        /// </summary>
        /// <param name="scope"></param>
        private void CreateLambda(Construct scope)
        {
        }

        /// <summary>
        /// Creates the endpoints to expose the crud operations
        /// </summary>
        /// <param name="scope"></param>
        private void CreateApiEndpoints(Construct scope)
        {
        }
    }
}