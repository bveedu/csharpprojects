using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeGuru.Models
{

    public class Project
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ProjectModels 
    {
        List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                var collection = DBEntities.database.GetCollection<Department>("Project");
                return collection.FindAllAs<Project>().ToList();
                // return null;
            }
            set { projects = value; }
        }
        public void CreateProject(Project project)
        {
            try
            {

                MongoCollection<Project> MCollection = DBEntities.database.GetCollection<Project>("Project");
                BsonDocument doc = new BsonDocument { 
                    {"Name",project.Name},
                    {"Description",project.Description}
                };

                IMongoQuery query = Query.EQ("Name", project.Name);
                var exists = MCollection.Find(query);
                if (exists.ToList().Count == 0)
                    MCollection.Insert(doc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void EditProject(Project project)
        {
            try
            {
                MongoCollection<Project> MCollection = DBEntities.database.GetCollection<Project>("Project");
                IMongoQuery query = Query.EQ("Name", project.Name);
                IMongoUpdate update = MongoDB.Driver.Builders.Update.Set("Description", project.Description); ;
                MCollection.Update(query, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void DeleteProject(Project project)
        {
            try
            {
                MongoCollection<Project> MCollection = DBEntities.database.GetCollection<Project>("Project");
                IMongoQuery query = Query.EQ("_id", project._id);
                MCollection.Remove(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}
