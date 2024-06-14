using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDVLD;

namespace BunissessLayerDVLD
{
    public  class clsUser
    {
        private enum enMode { add = 0, Update = 1 };
        enMode _mode;
        public int idUser { get; set; }
        public int idPerson { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string FullName { get; set; }
        public int Bit { get; set; }


        private clsUser(ref int idUser, ref int PersonID, ref string username,
           ref string password, ref int bit,ref string fullName)
        {
            this.idUser = idUser;
            this.idPerson = idPerson;
            this.username = username;
            this.password = password;
            this.Bit = bit;
            this.FullName = fullName;
            _mode = enMode.Update;
        }

        private clsUser(ref int idUser, int PersonID, ref string username,
           ref string password, ref int bit, ref string fullName)
        {
            this.idUser = idUser;
            this.idPerson = idPerson;
            this.username = username;
            this.password = password;
            this.Bit = bit;
            this.FullName = fullName;
            _mode = enMode.Update;
        }


        public clsUser()
        {
            this.idUser = -1;
            this.idPerson = -1;
            this.username ="";
            this.password = "";
            this.Bit =-1;
            this.FullName ="";
            _mode = enMode.add;
        }

        private bool AddNewUsers()
        {
            idUser = clsUserData.AddNewUser(idPerson, username, password, Bit,FullName);
            return idUser != -1;
        }
        public static bool UpdatatePassword(int idUser,string password)
        {
            return clsUserData.Updatepassword(idUser, password);
        }

        private bool UpdateUser()
        {
            return clsUserData.UpdateUser(idUser, username, password, Bit,FullName);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.add:
                    {
                        if (AddNewUsers())
                        {
                            _mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return UpdateUser();
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public static clsUser FindUserByID(int idUser)
        {
            string username = "";
            string password = ""; int bit = 0;
            int person = 0;
            string fullname = "";


            if (clsUserData.GetUserByID(idUser, ref person, ref username, ref password, ref bit,ref fullname))
            {
                return new clsUser(ref idUser, ref person, ref username, ref password, ref bit,ref fullname );
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindUserByIDPerson(int idPerson)
        {
            string username = "";
            string password = ""; int bit = 0;
            string fullname = "";
            int idUser = 0;

            if (clsUserData.GetUserByIDPerson(ref idUser, idPerson, ref username, ref password, ref bit, ref fullname))
            {
                return new clsUser(ref idUser,ref idPerson, ref username, ref password, ref bit, ref fullname);
            }
            else
            {
                return null;
            }
        }



        public static  DataTable GetAllUser()
        {
            return clsUserData.GetAllUsers();

        }

        public bool isExistUser(int id)
        {
            return clsUserData.isExistUser(id);
        }

        public static DataTable Search(string s,string data)
        {
            return clsUserData.SearchData(s,data);
        }

        public static DataTable GererCombox(string s)
        {
            return clsUserData.GereCombox(s);
        }

        public static bool DeleteUser(int idUser)
        {
            return clsUserData.deleteUser(idUser);
        }

    }

}

