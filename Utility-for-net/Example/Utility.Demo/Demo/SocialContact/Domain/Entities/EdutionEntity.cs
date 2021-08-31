using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Entities
{
    public class EdutionEntity : BaseEntity
    {
        public virtual long? CatagoryId { get; set; }
        public virtual long? UserId { get; set; }
        public virtual CatagoryEntity Catagory { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual string FirstEdution { get; set; }
        public virtual string FirstSchool { get; set; }
        public virtual long FirstStartDate { get; set; }
        public virtual long FirstEndDate { get; set; }

        public virtual string SecondEdution { get; set; }
        public virtual string SecondSchool { get; set; }
        public virtual long SecondStartDate { get; set; }
        public virtual long SecondEndDate { get; set; }

        public virtual string ThreeEdution { get; set; }
        public virtual string ThreeSchool { get; set; }
        public virtual long ThreeStartDate { get; set; }
        public virtual long ThreeEndDate { get; set; }

        public virtual string FourEdution { get; set; }
        public virtual string FourSchool { get; set; }
        public virtual long FourStartDate { get; set; }
        public virtual long FourEndDate { get; set; }


        public virtual string FiveEdution { get; set; }
        public virtual string FiveSchool { get; set; }
        public virtual long FiveStartDate { get; set; }
        public virtual long FiveEndDate { get; set; }

    }
}
