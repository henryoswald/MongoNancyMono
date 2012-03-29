using System;
using MongoDB;
using MongoDB.Configuration;
using MongoDB.Linq;
using System.Dynamic;
using Nancy.IO;
using System.IO;
using Nancy.ModelBinding;

namespace NancyTest
{
	public class TestModule : Nancy.NancyModule
	{
		public TestModule ()
		{
			Get["/"] = req => "Hello World";
			
			Post["/addPerson"] = req => 
			{
				Person model = this.Bind();
				SavePeople(model);
				return "OK";
				
			};
		}
		
		public string CreatePerson(RequestStream stream)
		{
			var reader = new StreamReader(stream);
			reader.ReadToEnd();
			Console.Write("");
			return "";
		}
		
		public void SavePeople(Person person)
        {
            var config = new MongoConfigurationBuilder();

            // COMMENT OUT FROM HERE
            config.Mapping(mapping =>
            {
                mapping.DefaultProfile(profile =>
                {
                    profile.SubClassesAre(t => t.IsSubclassOf(typeof(Person)));
                });
                mapping.Map<Person>();
                mapping.Map<Child>();
            });
            // TO HERE

            config.ConnectionString("Server=127.0.0.1");

            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                try
                {
                    var db = mongo.GetDatabase("Mono");
                    var collection = db.GetCollection<Person>();
					collection.Save(person);

//                    Person mum = new Person()
//                    {
//                        Corners = 4,
//                        Name = "Square"
//                    };
//
//                    Person dad = new Person()
//                    {
//                        Corners = 0,
//                        Name = "Circle"
//                    };
//
//                    Child child = new Child()
//                    {
//                        Name = "SubClass",
//                        Corners = 6,
//                        Ratio = 3.43
//                    };
//
//                    collection.Save(mum);
//                    collection.Save(dad);
//                    collection.Save(child);
                }
                finally
                {
                    mongo.Disconnect();
                }
            }
        }
	}
	
	
    public class Person
    {
        public string Name { get; set; }
        public  int Corners { get; set; }
    }

    internal class Child : Person
    {
        public double Ratio { get; set; }
    }	
	
	public class MongoTest
    {
    
        
    }
}


