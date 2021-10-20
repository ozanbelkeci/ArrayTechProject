using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DbOperations
{
    public class UserDb
    {
        private static UserDb _instance;
        private UserDb()
        {

        }
        public static UserDb GetInstance()
        {
            return _instance ?? new UserDb();
        }

        public Users ControlLogin(string _username, string _userPassword)
        {
            try
            {
                using (var context = new Entities.Entities())
                {

                        var user = context.Users.FirstOrDefault(x => x.Username == _username && x.Password == _userPassword);
                        if (user != null)
                            return user;
                        return null;
                    

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
