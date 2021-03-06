﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheckIt.DataAccessLayer
{
    public class User
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid userID { get; set; }

        //TODO: make userEmail PK and redo dependencies
        //[Key, Column(Order = 1)]
        //[Index(IsUnique = true)]
        public string userEmail { get; set; }
        //setting up parent recursion
        public Guid? parentID { get; set; }
        [ForeignKey("parentID")]
        public virtual User Parent { get; set; }

        //setting up client
        public Guid? clientID { get; set; }
        [ForeignKey("clientID")]
        public virtual Client client { get; set; }

        public Guid? ssoID { get; set; }

        public int height { get; set; }

        public string accountType { get; set; }
        public bool active { get; set; }
        public bool trackTelData { get; set; }
       
        public DateTime createdAt { get; set; }


        
        public virtual List<UserAction> userActions { get; set; }
        public virtual List<ItemList> itemsList { get; set; }
        

        public User()
        {
            active = true;
            userActions = new List<UserAction>();
            itemsList = new List<ItemList>();
            createdAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Create a normal user - mainly made for admin user creation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="atype"></param>
        /// <param name="clientID"></param>
        /// <param name="parentID"></param>
        /// <param name="ssoID"></param>
        public User(string email, string atype,int height, Guid? clientID, Guid? parentID)
        {
            userEmail = email;
            accountType = atype;
            this.clientID = clientID;
            this.height = height;
            active = true;
            trackTelData = true;
            this.parentID = parentID;
            ssoID = null;
            userActions = new List<UserAction>();
            itemsList = new List<ItemList>();
            createdAt = DateTime.UtcNow;


        }

        /// <summary>
        /// Creates basic user - used for user registration
        /// </summary>
        /// <param name="email"></param>
        /// <param name="atype"></param>
        public User(string email, string atype, Guid? ssoID=null)
        {
            userEmail = email;
            accountType = atype;
            clientID = null;
            height = 2;
            active = true;
            trackTelData = true;
            parentID = null;
            this.ssoID = ssoID;
            userActions = new List<UserAction>();
            createdAt = DateTime.UtcNow;

        }

    }

}