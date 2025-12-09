using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_353502_Belostotsky.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public ICollection<ClothingItem> ClothingItems { get; set; }

        public Category()
        {
            ClothingItems = new List<ClothingItem>();
        }
    }
}
