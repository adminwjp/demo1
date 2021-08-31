namespace SocialContact.Domain.Entities
{
    public class IconEntity : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual string Style { get; set; }
        public virtual string Description { get; set; }
    }
}
