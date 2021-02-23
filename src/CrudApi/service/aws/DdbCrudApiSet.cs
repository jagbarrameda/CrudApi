using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Attribute = Amazon.CDK.AWS.DynamoDB.Attribute;

namespace io.jbarrameda.CrudApi.service.aws
{
    /**
     * A CRUD ApiSet backed by ddb, a lambda and an api gateway endpoint
     */
    public class DdbCrudApiSet : AwsApiSet
    {
        /**
         * Gets the name of the ddb table
         */
        private string TableName { get; }

        public DdbCrudApiSet(Stack stack, string name) : base(stack)
        {
            Name = name;
            TableName = Name + "Table";
        }

        public override void CreateResources()
        {
            CreateDdb(Stack);
            CreateLambda(Stack);
            CreateApiEndpoints(Stack);
        }

        public override List<ApiMonitor> GetApiMonitors()
        {
            return new List<ApiMonitor>
            {
                GetStandardApiMonitor("Create"),
                GetStandardApiMonitor("Read"),
                GetStandardApiMonitor("Update"),
                GetStandardApiMonitor("Delete")
            };
        }

        private ApiMonitor GetStandardApiMonitor(string apiName)
        {
            return new()
            {
                ApiName = apiName, MetricMonitors = GetStandardMetricMonitors()
            };
        }

        private List<MetricMonitor> GetStandardMetricMonitors()
        {
            return new List<MetricMonitor>
            {
                new()
                {
                    MetricName = "Availability", DashboardVisible = true
                },
                new()
                {
                    MetricName = "Latency", DashboardVisible = true
                },
                new()
                {
                    MetricName = "Throughput", DashboardVisible = true
                }
            };
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
            var table = new Table(scope, TableName, props);
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