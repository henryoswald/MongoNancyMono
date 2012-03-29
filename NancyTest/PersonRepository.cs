using System;
using MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Dynamic;
using Nancy.IO;
using System.IO;
using Nancy.ModelBinding;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace NancyTest
{
	public class PersonRepository
	{
		MongoServer _server;
		MongoDatabase _peopleDb;
		
		public PersonRepository()
		{	
			string connectionString = "mongodb://localhost";
			_server = MongoServer.Create(connectionString);
			_peopleDb = _server.GetDatabase("Mono");
			

		}
		
		public void Save(Person person)
		{
            using (_server.RequestStart(_peopleDb))
            {
	            var collection = _peopleDb.GetCollection<Person>("Person");
				collection.Save(person);
            }
		}
		
		public Person FindPerson()
		{
		 	using (_server.RequestStart(_peopleDb))
		 	{
                var people = _peopleDb.GetCollection<Person>("Person");
				var person = people.FindOneAs<Person>();
				return person;
				//return person;					
            }
			
		}
	}
}

