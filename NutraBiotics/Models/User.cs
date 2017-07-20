using SQLite.Net.Attributes;

namespace NutraBiotics.Models
{
    public class User
    {
        [PrimaryKey]
        public int UserId
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Picture
        {
            get;
            set;
        }

        public int Gender
        {
            get;
            set;
        }

        public string IMEI
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName);  }
        }

        public string PictureFullPath
        {
            get {
                if (string.IsNullOrEmpty(Picture)) {
                    if (Gender == 1) {
                        return "male.png";
                    }

                    return "female.png";
                }

                return string.Format(
                    "http://nutrabioticsbackend.azurewebsites.net{0}", 
                    Picture.Substring(1));
            }
        }

        public override int GetHashCode()
        {
            return UserId;
        }
    }
}
