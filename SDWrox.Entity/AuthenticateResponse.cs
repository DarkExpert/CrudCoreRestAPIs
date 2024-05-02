using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDWrox.Entity
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(int userId, string firstName, string lastName, string userName, string token)
        {
            Id = userId;
            FirstName = firstName;
            LastName = lastName;
            Username = userName;
            Token = token;
        }
    }
}
