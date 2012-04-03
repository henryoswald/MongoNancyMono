using System;
using NUnit.Framework;

namespace NancyMongoCrud.Acceptance.Tests
{
	[TestFixture]
	public class CrudTests
	{
		[Test]
		public void Test1(){
			
		 	// Given
		    var bootstrapper = new DefaultNancyBootstrapper();
		    var browser = new Browser(bootstrapper);
		
		    // When
		    var result = browser.Get("/", with => {
		        with.HttpRequest();
		    });
		
		    // Then
		    Assert.Equal(result.StatusCode, HttpStatusCode.OK);
		}
		
		
			
	}
}

