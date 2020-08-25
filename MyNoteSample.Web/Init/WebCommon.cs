using MyNoteSample.Common;
using MyNoteSample.Entities;
using MyNoteSample.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNoteSample.Web.Init
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            NoteUser user = CurrentSession.user;
            if (user != null)
            {
                return user.UserName;
            }
            else
            {
                return "_system";
            }
        }
    }
}