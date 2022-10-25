﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{

    public class BlogComment : BlogCommentCreate
    {
        public string Username { get; set; }

        public string ApplicationUserId { get; set; }

        public  DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
