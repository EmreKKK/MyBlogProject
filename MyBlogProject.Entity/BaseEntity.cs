using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Required,DisplayName("Create On")]
        public DateTime CreateOn { get; set; }

        [Required,DisplayName("Modified On")]
        public DateTime ModifiedOn { get; set; }

        [Required, StringLength(30),DisplayName("Modified Username")]
        public string ModifiedUsername { get; set; }
    }
}
