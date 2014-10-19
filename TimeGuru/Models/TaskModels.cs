using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeGuru.Models
{
    public class Task
    {
        public ObjectId _id { get; set; }
        public string Project { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class TaskModels
    {
        List<Task> tasks;
        public List<Task> Tasks
        {
            get
            {
                var collection = DBEntities.database.GetCollection<Department>("Task");
                return collection.FindAllAs<Task>().ToList();
                // return null;
            }
            set { tasks = value; }
        }
        public void CreateTask(Task task)
        {
            try
            {

                MongoCollection<Task> MCollection = DBEntities.database.GetCollection<Task>("Task");
                BsonDocument doc = new BsonDocument { 
                    {"Project",task.Project},
                    {"Name",task.Name},
                    {"Description",task.Description}
                };

                IMongoQuery query = Query.EQ("Name", task.Name);
                var exists = MCollection.Find(query);
                if (exists.ToList().Count == 0)
                    MCollection.Insert(doc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void EditTask(Task task)
        {
            try
            {
                MongoCollection<Task> MCollection = DBEntities.database.GetCollection<Task>("Task");
                IMongoQuery query = Query.EQ("_id", task._id);
                IMongoUpdate update = MongoDB.Driver.Builders.Update.Set("Name", task.Name).Set("Description", task.Description).Set("Project",task.Project); 
                MCollection.Update(query, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void DeleteTask(Task task)
        {
            try
            {
                MongoCollection<Task> MCollection = DBEntities.database.GetCollection<Task>("Task");
                IMongoQuery query = Query.EQ("_id", task._id);
                MCollection.Remove(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
 }
