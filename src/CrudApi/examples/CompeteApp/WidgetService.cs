using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;

namespace io.jbarrameda.CrudApi.examples.CompeteApp
{
    /// <summary>
    /// Experimentation from tutorials
    /// </summary>
    public class WidgetService : Construct
    {
        public WidgetService(Construct scope, string id) : base(scope, id)
        {
            var bucket = new Bucket(this, "WidgetStore");

            var handler = new Function(this, "WidgetHandler", new FunctionProps
            {
                Runtime = Runtime.NODEJS_10_X,
                Code = Code.FromAsset("src/CrudApi/resources"),
                Handler = "widgets.main",
                Environment = new Dictionary<string, string>
                {
                    ["BUCKET"] = bucket.BucketName
                }
            });

            bucket.GrantReadWrite(handler);

            var api = new RestApi(this, "Widgets-API", new RestApiProps
            {
                RestApiName = "Widget Service",
                Description = "This service services widgets."
            });

            var getWidgetsIntegration = new LambdaIntegration(handler, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                }
            });

            api.Root.AddMethod("GET", getWidgetsIntegration);
            
            var widget = api.Root.AddResource("{id}");

            // Add new widget to bucket with: POST /{id}
            var postWidgetIntegration = new LambdaIntegration(handler);

            // Get a specific widget from bucket with: GET /{id}
            var getWidgetIntegration = new LambdaIntegration(handler);

            // Remove a specific widget from the bucket with: DELETE /{id}
            var deleteWidgetIntegration = new LambdaIntegration(handler);

            widget.AddMethod("POST", postWidgetIntegration);        // POST /{id}
            widget.AddMethod("GET", getWidgetIntegration);          // GET /{id}
            widget.AddMethod("DELETE", deleteWidgetIntegration);    // DELETE /{id}
        }
    }
}