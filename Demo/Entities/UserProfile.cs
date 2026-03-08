using Demo.Data;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Demo.Entities
{
    public class UserProfile
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore] // prevents client from sending in JSON
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
