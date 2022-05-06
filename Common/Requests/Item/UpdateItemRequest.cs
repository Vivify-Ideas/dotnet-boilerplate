
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.Requests
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
