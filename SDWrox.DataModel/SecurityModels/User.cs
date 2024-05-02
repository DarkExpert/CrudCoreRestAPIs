using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SDWrox.DataModel.SecurityModels
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public bool isActive { get; set; }
    }
}
