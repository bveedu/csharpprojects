using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeGuru.Models
{
    public class TimeEntry
    {
        public ObjectId _id { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public float Hours { get; set; }
    }
    public class TimeEntryModels
    {
        List<TimeEntry> timeEntries;
        public List<TimeEntry> TimeEntries
        {
            get
            {
                var collection = DBEntities.database.GetCollection<Department>("TimeEntry");
                return collection.FindAllAs<TimeEntry>().ToList();
                // return null;
            }
            set { timeEntries = value; }
        }
        public void CreateTimeEntry(TimeEntry timeEntry)
        {
            try
            {

                MongoCollection<TimeEntry> MCollection = DBEntities.database.GetCollection<TimeEntry>("TimeEntry");
                BsonDocument doc = new BsonDocument { 
                    {"Date",timeEntry.Date},
                    {"Project",timeEntry.Project},
                    {"User",timeEntry.User},
                    {"Task",timeEntry.Task},
                    {"Hours",timeEntry.Hours}
                };
                MCollection.Insert(doc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void EditTimeEntry(TimeEntry timeEntry)
        {
            try
            {
                MongoCollection<TimeEntry> MCollection = DBEntities.database.GetCollection<TimeEntry>("TimeEntry");
                IMongoQuery query = Query.EQ("_id", timeEntry._id);
                IMongoUpdate update = MongoDB.Driver.Builders.Update.Set("Date", timeEntry.Date).
                    Set("User", timeEntry.User).Set("Task",timeEntry.Task).
                    Set("Project",timeEntry.Project).Set("Hours",timeEntry.Hours); 
                MCollection.Update(query, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void DeleteTimeEntry(TimeEntry timeEntry)
        {
            try
            {
                MongoCollection<TimeEntry> MCollection = DBEntities.database.GetCollection<TimeEntry>("TimeEntry");
                IMongoQuery query = Query.EQ("_id", timeEntry._id);
                MCollection.Remove(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
    
}