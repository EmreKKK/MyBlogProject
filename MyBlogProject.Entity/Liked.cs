using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    [Table("Likes")]
    public class Liked
    {

        public int Id { get; set; }
        /// <summary>
        /// ///////////// hata burada
        /// </summary>
        public int? BlogId{ get; set; }
        public virtual Blog Blog { get; set; }
        public int? UserId { get; set; }
        public virtual User LikedUser { get; set; }
    }
}
