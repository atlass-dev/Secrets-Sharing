using Secrets_Sharing.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secrets_Sharing.Domain.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }       
        public ResourceType Type { get; set; }
        public string Path { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}