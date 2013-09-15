using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace uchange.Models
{
    public class ItemDB
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class ItemDBContext : DbContext
    {
        public DbSet<ItemDB> Items { get; set; }
    }

    public class PersonDB
    {
        [Key]
        public string student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int item_now { get; set; }
        public int item_original { get; set; }
    }

    public class PersonDBContext : DbContext
    {
        public DbSet<PersonDB> Persons { get; set; }
    }

    public class RequestDB
    {
        [Key]
        public int id { get; set; }
        public string from { get; set; }
        public int to { get; set; }
    }

    public class RequestDBContext : DbContext
    {
        public DbSet<RequestDB> Requests { get; set; }
    }

    public class DealDB
    {
        [Key]
        public int id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int item { get; set; }
        public DateTime deal_time { get; set; }
    }

    public class DealDBContext : DbContext
    {
        public DbSet<DealDB> Deals { get; set; }
    }

    public class CommentDB
    {
        [Key]
        public int id { get; set; }
        public string student_id { get; set; }
        public int item_id { get; set; }
        public string content { get; set; }
        public DateTime comment_time { get; set; }
    }

    public class CommentDBContext : DbContext
    {
        public DbSet<CommentDB> Comments { get; set; }
    }

}