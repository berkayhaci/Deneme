using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase  //Çünkü verilen temel değerlerin diğer sınıflarda değişikliğe uğramasını isteyebiliriz
    {
        public virtual int Id { get; set; }

        public virtual DateTime CreatedDate { get; set; } = DateTime.Now; //override CreatedDate = new DateTime (2022/07/27);

        public virtual DateTime ModifiedDate { get; set; } =DateTime.Now;

        public virtual bool IsDeleted { get; set; } = false; //silinip silinmediği. Örnek silinmemiş makaleleri getir
        public virtual bool IsActive { get; set; } = true; //makale aktif 
        public virtual string CreatedByName { get; set; } = "Admin";  //Blogta kayıt ol kısmı olmayacak. Genelde okunduktan sonra yorum bırakma 
        public virtual string ModifiedByName { get; set; } = "Admin"; // default değer admin
        public virtual string Note { get; set; }


    }
}
