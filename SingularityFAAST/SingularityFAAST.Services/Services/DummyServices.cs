using System.Collections.Generic;
using System.Linq;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;

namespace SingularityFAAST.Services.Services
{
    public class DummyServices
    {
        //often - will create service layer specific model for what we are returning
        //we can safely use Dummy use because it reisdes in Core
        public IList<Dummy> GetAllDummies()
        {
            using (var context = new SingularityDBContext())
            {
                //var dummies = context.Dummies.Where(dummy => dummy.Age > 50);

                //grab dummies in Dummies table - NOT A LIST YET
                var dummies = context.Dummies;

                //instantiate into list of concrete dummy objects (c# objs)
                var dummyList = dummies.ToList();

                //return them
                return dummyList;
            }
        }
    }
}
