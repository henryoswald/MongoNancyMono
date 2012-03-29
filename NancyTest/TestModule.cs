using System;
using MongoDB;
using System.Linq;
using System.Dynamic;
using Nancy.IO;
using System.IO;
using Nancy.ModelBinding;

namespace NancyTest
{
	public class TestModule : Nancy.NancyModule
	{
		PersonRepository repository = new PersonRepository();
		public TestModule ()
		{
			Get["/status"] = req => "Working";
			
			Post["/person"] = req => 
			{
				Person model = this.Bind();
				repository.Save(model);
				return "OK";
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


