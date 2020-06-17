using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudApi
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CrudApiStack(app, "CrudApiStack");
            app.Synth();
        }
    }
}
