using Amazon.CDK;
using io.jbarrameda.CrudApi.examples.CompeteApp;

namespace io.jbarrameda.CrudApi
{
    internal static class Program
    {
        /// <summary>
        /// Program to test the CrudApi library.
        ///
        /// To build:
        /// dotnet build src
        /// 
        /// To test, use cdk cli:
        /// cdk ls
        /// cdk synth
        /// cdk deploy
        /// cdk diff
        /// cdk destroy
        ///
        /// see `cdk help`
        /// From https://docs.aws.amazon.com/cdk/latest/guide/hello_world.html
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var competeApp = new CompeteApp(new App(), "CompeteApp");
            competeApp.Synth();
        }
    }
}
