using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JooleWebAPI.Unitofwork;

namespace JooleWebAPI.Models
{
    public class UserValidation
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new JooleContext());

        public bool validateUserName(string userName)
        {
            return _unitOfWork.Users.userNameCheck(userName);
        }
        public bool validateUserEmail(string emailID)
        {
            return _unitOfWork.Users.emailCheck(emailID);
        }
    }
}