using OnlineVaccineSystem.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineVaccineSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public UserMaster _user { get; set; }
       public List<AppoimentMaster> AppoimentMasters { get; set; }
       
    }
}