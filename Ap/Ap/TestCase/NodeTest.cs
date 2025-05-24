using Ap.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ap.TestCase
{
    public class NodeTest
    {
        public static void OrForkTest()
        {
            LineBuilder builder = new LineBuilder("Edit");


            builder.Then("FirstApprove")
                .Then("SecondApprove")
                //.Complete("Completed");
        }
    }
}
