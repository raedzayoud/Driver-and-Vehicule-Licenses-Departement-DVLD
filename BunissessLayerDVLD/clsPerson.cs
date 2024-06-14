using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessDVLD;

namespace BunissessLayerDVLD
{
    public class clsPerson
    {
            public enum Mode { Add = 1, Update = 2 };

            private Mode _mode;
            public int idPerson { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string SecondName { get; set; }
            public string NationalNo { get; set; }
            public DateTime DateofBirth { get; set; }
            public string Gender { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int NationalityCountry { get; set; }
            public string ImagePath { get; set; }

            public clsPerson()
            {
                idPerson = -1;
                NationalityCountry = 0;
                FirstName = "";
                LastName = "";
                SecondName = "";
                ImagePath = "";
                Address = "";
                Phone = "";
                Gender = "";
                DateofBirth = DateTime.Now;
                NationalNo = "";
                _mode = Mode.Add;
            }

            private clsPerson(int id, string firstName, string lastName, string secondName,
               string nationalNo, DateTime dateOfBirth, string gender, string address, string phone,
               string email, int nationalityCountry, string imagePath)
            {
                idPerson = id;
                NationalityCountry = nationalityCountry;
                FirstName = firstName;
                LastName = lastName;
                SecondName = secondName;
                ImagePath = imagePath;
                Address = address;
                Phone = phone;
                Gender = gender;
                DateofBirth = dateOfBirth;
                NationalNo = nationalNo;
                Email= email;
                _mode = Mode.Update;
            }

            public bool AddNewPerson()
            {
                idPerson = clsPersonData.AddNewPerson(FirstName, LastName, SecondName,
                NationalNo, DateofBirth, Gender, Address, Phone,
                Email, NationalityCountry, ImagePath);
                return idPerson != -1;

            }

            public bool UpdatePerson()
            {
                return clsPersonData.UpdatePerson(idPerson, FirstName, LastName, SecondName,
                NationalNo, DateofBirth, Gender, Address, Phone,
                Email, NationalityCountry, ImagePath);
            }

            public bool Save()
            {
                switch (_mode)
                {
                    case Mode.Add:
                        {
                            if (AddNewPerson())
                            {
                                _mode = Mode.Update;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    case Mode.Update:
                        {
                            return UpdatePerson();
                        }
                    default:
                        {
                            return false;
                        }
                }
            }

            public static clsPerson FindPersonByID(int id)
            {
                string firstName = "", lastName = "", secondName = "", nationalNo = "", address = "", phone = "", email = "", imagePath = "";
                DateTime dateOfBirth = DateTime.Now;
                string gender = ""; int nationalityCountry = 0;

                if (clsPersonData.GETPersonByID(id, ref firstName, ref lastName, ref secondName,
                    ref nationalNo, ref dateOfBirth, ref gender, ref address, ref phone,
                    ref email, ref nationalityCountry, ref imagePath))
                {
                    return new clsPerson(id, firstName, lastName, secondName, nationalNo, dateOfBirth,
                        gender, address, phone, email, nationalityCountry, imagePath);
                }
                else
                {
                    return null;
                }
            }
        public static clsPerson FindPersonByNational(string NationalNo)
        {
            string firstName = "", lastName = "", secondName = "", address = "", phone = "", email = "", imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            string gender = ""; int nationalityCountry = 0;
            int id = 0;

            if (clsPersonData.GETPersonByNational(ref id,ref firstName, ref lastName, ref secondName,
                 NationalNo, ref dateOfBirth, ref gender, ref address, ref phone,
                ref email, ref nationalityCountry, ref imagePath))
            {
                return new clsPerson(id, firstName, lastName, secondName, NationalNo, dateOfBirth,
                    gender, address, phone, email, nationalityCountry, imagePath);
            }
            else
            {
                return null;
            }
        }

        public static  clsPerson FindPersonByNationality(string Nationality)
            {
                string firstName = "", lastName = "", secondName = "", address = "", phone = "", email = "", imagePath = "";
                DateTime dateOfBirth = DateTime.Now;
                string gender = "";int nationalityCountry = 0;
                int id = 0;
                if (clsPersonData.GETPersonByNationality(id, ref firstName, ref lastName, ref secondName, ref Nationality,
                  ref dateOfBirth, ref gender, ref address, ref phone,
                    ref email, ref nationalityCountry, ref imagePath))
                {
                    return new clsPerson(id, firstName, lastName, secondName, Nationality, dateOfBirth,
                        gender, address, phone, email, nationalityCountry, imagePath);
                }
                else
                {
                    return null;
                }
            }

            public static DataTable GetAllPeople()
            {
                return clsPersonData.GetAllPeople();

            }

            public bool isExistPerson(int id)
            {
                return clsPersonData.isExistPerson(id);
            }

            public bool isExistPerson(string NationalNo)
            {
                return clsPersonData.isExistPerson(NationalNo);
            }

            public static bool DeletePerson(int id)
            {
               
                  return clsPersonData.deletePerson(id);
            
            }
            public static DataTable Search(string s,string Data)
        {
            return clsPersonData.SearchData(s,Data);
        }

        public static bool updatePersonOfuser(int idPerson, string first, string second, string third)
        {
            return clsPersonData.updatePersonOfuser(idPerson, first, second, third);
        }

        }
}

