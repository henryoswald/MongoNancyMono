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
		MongoCollection _people;
		
		public PersonRepository()
		{	
			string connectionString = "mongodb://localhost";
			_server = MongoServer.Create(connectionString);
			_peopleDb = _server.GetDatabase("Mono");
			_people = _peopleDb.GetCollection<Person>("Person");

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
				var person = _people.FindOneAs<Person>();
				return person;
            }
		}
		
		public void Update(string id)
		{
			using (_server.RequestStart(_peopleDb))
		 	{
				var query = new QueryDocument(){
					{ "_id", ObjectId.Parse(id) }
				};
				var update = new UpdateDocument{
					{"$set", new BsonDocument("Corners",500)}
				};
				
				var sort = new SortByDocument();
				_people.FindAndModify(query, sort,update);
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

