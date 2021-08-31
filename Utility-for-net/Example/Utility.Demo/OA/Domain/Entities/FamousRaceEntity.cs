using NHibernate.Mapping.Attributes;

namespace OA.Domain.Entities
{
    [Class(Table = "famous_race_info", Name = "FamousRaceInfo", NameType = typeof(FamousRaceEntity), Lazy = false)]
    public class FamousRaceEntity:BaseEntity
    {
        private string _name;
        private long _userId;
        [Property(Column = "name", NotNull = true,  Length = 50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [Drop]
        public long UserId
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
  
    }
}
