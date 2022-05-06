using Domain.Common.Entities;

namespace Domain.Entities
{
    public class Item : BaseEntity<int>
    { 
        public string Name { get; set; }
  
    }
}
