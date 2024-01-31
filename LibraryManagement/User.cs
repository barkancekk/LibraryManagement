using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    internal class User
    {
        public string id;
        public string name;
        public string lastName;
        public int penalty = 0;

        public List<BorrowedBook> borrowed = new List<BorrowedBook>();

        public User(string name, string lastName, int count)
        {
            this.name = name;
            this.lastName = lastName;
            id = name.ToCharArray()[0].ToString() + lastName.ToCharArray()[0].ToString() + count.ToString();
        }

        public override bool Equals(object obj)
        {
            User user = (User)obj;
            return (this.id == user.id);
        }
    }
}
