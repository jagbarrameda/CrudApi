using Amazon.CDK;

namespace io.jbarrameda.CrudApi
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new SchoolStack(app, "CrudApiStack");
            app.Synth();
        }
    }
}
