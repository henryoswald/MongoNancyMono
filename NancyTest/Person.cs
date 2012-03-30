using MongoDB.Bson;
using System;

namespace NancyMongoCrud
{
	public class Person
    {
		public ObjectId _id {get; set;}
        public string Name { get; set; }
        public int Age { get; set; }
		public string Nationality {get; set;}
	}
}

