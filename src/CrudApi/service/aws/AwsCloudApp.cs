using Amazon.CDK;

namespace io.jbarrameda.CrudApi.service.aws
{
    /// <summary>
    /// A CloudApp backed by Aws.
    /// </summary>
    public class AwsCloudApp : Stack, ICloudApp
    {
        private readonly App _app;
        
        internal AwsCloudApp(App app, string id, IStackProps props = null) : base(app, id, props)
        {
            _app = app;
        }

        public void AddApiSet(IApiSet set)
        {
            throw new System.NotImplementedException();
        }

        public void Synth()
        {
            _app.Synth();
        }
    }
}