using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TimeGuru.Models
{
    public class Department
    {
        public ObjectId _id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }



  
   

    public class DBEntities : DbContext
    {
        public DBEntities() : base("name=MongoConnection") { }
        static MongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString.ToString()); 
        static MongoServer server = client.GetServer();
        static MongoDatabase mongodb = server.GetDatabase("timeapp");

        public static MongoDatabase database { get {
            return mongodb;
        }
        }
        List<Department> department;
   
        public List<Department> Departments
        {
            get
            {
                var collection = database.GetCollection<Department>("Department");
                return collection.FindAllAs<Department>().ToList();
               // return null;
            }
            set { department = value; }
        }
       
        public void CreateDepartment(Department colleciton)
        {
            try
            {
                int Id = 0;
                if (Departments.Count() > 0)
                    Id = Departments.Max(x => x.DepartmentId);
                Id += 1;
                MongoCollection<Department> MCollection = database.GetCollection<Department>("Department");
                BsonDocument doc = new BsonDocument { 
                    {"DepartmentId",Id},
                    {"DepartmentName",colleciton.DepartmentName}
                };

                IMongoQuery query = Query.EQ("DepartmentName", colleciton.DepartmentName);
                var exists = MCollection.Find(query);
                if (exists.ToList().Count == 0)
                    MCollection.Insert(doc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void EditDepartment(Department collection)
        {
            try
            {
                MongoCollection<Department> MCollection = database.GetCollection<Department>("Department");
                IMongoQuery query = Query.EQ("DepartmentId", collection.DepartmentId);
                IMongoUpdate update = MongoDB.Driver.Builders.Update.Set("DepartmentName", collection.DepartmentName);
                MCollection.Update(query, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void DeleteDepartment( Department collection)
        {
            try
            { 
                MongoCollection<Department> MCollection = database.GetCollection<Department>("Department");
                IMongoQuery query = Query.EQ("_id", collection._id);
                MCollection.Remove(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}