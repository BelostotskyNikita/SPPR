using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_353502.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string NormalizedName { get; set; }

        // Навигационное свойство для связи один-ко-многим
        public virtual ICollection<ClothingItem> ClothingItems { get; set; }
    }
}
