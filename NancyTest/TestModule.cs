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
			
			Get["/person"] = req =>
			{
				var person = repository.FindPerson();
				return person.Name;
			};
			
			Patch["/person/{id}"] = req =>
			{
				var id = (string) req.id;
				//Person model = this.Bind();
				repository.Update(id);
				return id.ToString();
			};
			
			Delete["/person/{id}"] = req => {
				repository.Delete(req.id);
				return "done";
			};
		}
	}
}


