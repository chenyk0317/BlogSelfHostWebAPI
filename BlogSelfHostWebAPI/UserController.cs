﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlogSelfHostWebAPI
{
    public class UserController: ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "user1", "user2" };
        }

    }
}
