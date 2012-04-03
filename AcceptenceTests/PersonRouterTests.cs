using System;
using NUnit.Framework;
using NUnit;
using Nancy.Testing;
using Nancy;

namespace AcceptenceTests
{
	[TestFixture]
	public class PersonRouterTests
	{
		DefaultNancyBootstrapper _bootstrapper;
		Browser _browser;
		
		[SetUp]
		public void Setup()
		{
			_bootstrapper = new DefaultNancyBootstrapper();
		    _browser = new Browser(_bootstrapper);	
		}
		
		[Test]
		public void Should_return_status_ok_on_status_endpoint()
		{		
		    var result = _browser.Get("/status", with => {
		        with.HttpRequest();
		    });
		
		    Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
		}
		
		[Test]
		public void Should_sucesfully_create_person_with_post()
		{
			var result = _browser.Post("/person", with => {
				with.Body("name=testing");
		        with.HttpRequest();
		    });
		
		    Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
		}
	}
}

