/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_Final.Models
{

    //permission diye ara tablo ekle sonradan
    //admin permission method 
    public static List<User> AdminPermission(EFC_FinalContext db)
    {
        var query =
            from user in db.Users
            join permission in db.Permissions
            on user.Id equals permission.UserId
            where permission.Name == "Admin"
            select user;
        return query.ToList();
    }

    //sadece adminin metotlara erişebilmesi
    public static void AdminOnly()
    {
        using (var db = new EFC_FinalContext())
        {
            var admin = db.Admins.FirstOrDefault();
            if (admin == null)
            {
                Console.WriteLine("Admin not found");
                return;
            }
            Console.WriteLine("Admin found");
        }
    }

    // admin rolu oluşturma için bak
    // https://stackoverflow.com/questions/60681474/how-can-we-give-grant-and-revoke-permissions-by-entity-framework-code-first
}
*/