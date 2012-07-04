using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using NancyMongo.Web;

namespace NancyMongo.AcceptenceTests
{
	[TestFixture]
	public class CrudTests
	{
		Browser _browser;
		
		[SetUp]
		public void SetUp()
		{
			_browser = new Browser(new AppBootstrapper());
		}
		
		[Test]
		public void StatusCheck()
		{
			var result = _browser.Get("/status", with => {
		        with.HttpRequest();
		    });
		
		    Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
		}
		
		
			
	}
}

