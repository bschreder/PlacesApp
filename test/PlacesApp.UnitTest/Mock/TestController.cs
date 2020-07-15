using Library.BusinessErrors;
using Library.Infrastructure;
using PlacesApp.Controllers;
using System.Web.Mvc;

namespace PlacesApp.UnitTest.Mock
{
    public class TestController : Controller
    {

        public ActionResult AR_TestModel()
        {
            var tm = new BusinessResult<TestModel>() { Result = new TestModel() };
            return new ActionResponse<TestModel>(tm, true, new NullLogger());
        }
    }


}
