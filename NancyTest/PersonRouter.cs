using System;
using MongoDB;
using System.Linq;
using System.Dynamic;
using Nancy.IO;
using Nancy.Json;
using System.IO;
using Nancy.ModelBinding;
using Nancy;

namespace NancyMongoCrud
{
	public class TestModule : Nancy.NancyModule
	{
		PersonRepository repository = new PersonRepository();
		public TestModule ()
		{
			Get["/status"] = req => "<h1>Working</h1>";
			
			Post["/person"] = req => 
			{
				Console.WriteLine(Request.Headers.ContentType);
				Person model = this.Bind();
				var result = repository.Save(model);
				if(Request.Headers.ContentType == "application/xml")
				{
					return Response.AsXml(result);
				}
				else
				{
					return Response.AsJson(result);
				}
			};
			
			Get["/person/{id}"] = req =>
			{
				var person = repository.FindPerson(req.id);
				return person.Name;
			};
			
			Patch["/person/{id}"] = req =>
			{
				Person model = this.Bind();
				repository.Update(req.id, model);
				return "updated";
			};
			
			Delete["/person/{id}"] = req => {
				repository.Delete((string)req.id);
				return "deleted " + req.id;
			};
		}
	}
}


