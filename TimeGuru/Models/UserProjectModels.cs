using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeGuru.Models
{
    public class UserProjects
    {
        public ObjectId _id { get; set; }
        public string User { get; set; }
        public string Project { get; set; }
    }
    public class UserProjectModels
    {
                List<UserProjects> userProjectss;
        public List<UserProjects> UserProjectss
        {
            get
            {
                var collection = DBEntities.database.GetCollection<Department>("UserProjects");
                return collection.FindAllAs<UserProjects>().ToList();
                // return null;
            }
            set { userProjectss = value; }
        }
        public void CreateUserProjects(UserProjects userProjects)
        {
            try
            {

                var UserProjects = DBEntities.database.GetCollection<UserProjects>("UserProjects");
                BsonDocument doc = new BsonDocument { 
                    {"Project",userProjects.Project},
                    {"User",userProjects.User}
                };

               // IMongoQuery query = Query.Matches("User", userProjects.User).EQ("", userProjects.Project);
                var count = UserProjects.AsQueryable<UserProjects>().Where(e => e.User == userProjects.User).Where
                (e=>  e.Project == userProjects.Project ).Count();
                if (count == 0)
                    UserProjects.Insert(doc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void EditUserProjects(UserProjects userProjects)
        {
            try
            {
                MongoCollection<UserProjects> MCollection = DBEntities.database.GetCollection<UserProjects>("UserProjects");
                IMongoQuery query = Query.EQ("_id", userProjects._id);
                IMongoUpdate update = MongoDB.Driver.Builders.Update.Set("User", userProjects.User).Set("Project", userProjects.Project); 
                MCollection.Update(query, update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        public void DeleteUserProjects(UserProjects userProjects)
        {
            try
            {
                MongoCollection<UserProjects> MCollection = DBEntities.database.GetCollection<UserProjects>("UserProjects");
                IMongoQuery query = Query.EQ("_id", userProjects._id);
                MCollection.Remove(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
    
}