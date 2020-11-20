using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wineApi.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserInfo { get; set; }
        public List<WineModel> UserWines { get; set; }
    }
}
