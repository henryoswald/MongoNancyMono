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

namespace NancyMongoCrud
{
	public class PersonRepository
	{
		MongoServer _server;
		MongoDatabase _peopleDb;
		MongoCollection _people;
		
		public PersonRepository()
		{	
			string connectionString = "mongodb://localhost";
			_server = MongoServer.Create(connectionString);
			_peopleDb = _server.GetDatabase("Mono");
			_people = _peopleDb.GetCollection<Person>("Person");

		}
		
		public Person Save(Person person)
		{
            using (_server.RequestStart(_peopleDb))
            {
				_people.Save(person);
            }
			return person;
		}
		
		public Person FindPerson(string id)
		{
		 	using (_server.RequestStart(_peopleDb))
		 	{
				var person = _people.FindOneByIdAs<Person>(ObjectId.Parse(id));
				return person;
            }
		}
		
		public void Update(string id, Person updatedProperties)
		{
			using (_server.RequestStart(_peopleDb))
		 	{
				var person = _people.FindOneByIdAs<Person>(ObjectId.Parse(id));
				person.Name = updatedProperties.Name;
				_people.Save(person);
			}
		}
		
		public void Delete(string id){
			using (_server.RequestStart(_peopleDb))
		 	{
				var query = new QueryDocument(){
					{ "_id", ObjectId.Parse(id) }
				};
				_people.Remove(query);
			}
		}
	}
}

