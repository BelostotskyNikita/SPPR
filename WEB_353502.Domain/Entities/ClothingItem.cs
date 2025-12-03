using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_353502.Domain.Entities
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string ImagePath { get; set; }
        public string MimeType { get; set; }

        // Внешний ключ для связи с категорией
        public int CategoryId { get; set; }

        // Навигационное свойство
        public virtual Category Category { get; set; }
    }
}
