using AutoMapper;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Application.Services.Dtos
{
    #region Edution
    public abstract class BaseEdutionInput
    {
        public virtual string FirstEdution { get; set; }
        public virtual string FirstSchool { get; set; }
        [IgnoreMap] public virtual DateTime? FirstStartDate { get; set; }
        [IgnoreMap] public virtual DateTime? FirstEndDate { get; set; }

        public virtual string SecondEdution { get; set; }
        public virtual string SecondSchool { get; set; }
        [IgnoreMap] public virtual DateTime? SecondStartDate { get; set; }
        [IgnoreMap] public virtual DateTime? SecondEndDate { get; set; }

        public virtual string ThreeEdution { get; set; }
        public virtual string ThreeSchool { get; set; }
        [IgnoreMap] public virtual DateTime? ThreeStartDate { get; set; }
        [IgnoreMap] public virtual DateTime? ThreeEndDate { get; set; }

        public virtual string FourEdution { get; set; }
        public virtual string FourSchool { get; set; }
        [IgnoreMap] public virtual DateTime? FourStartDate { get; set; }
        [IgnoreMap] public virtual DateTime? FourEndDate { get; set; }


        public virtual string FiveEdution { get; set; }
        public virtual string FiveSchool { get; set; }
        [IgnoreMap] public virtual DateTime? FiveStartDate { get; set; }
        [IgnoreMap] public virtual DateTime? FiveEndDate { get; set; }
    }
    [AutoMap(typeof(EdutionEntity))]
    public class CreateEdutionInput : BaseEdutionInput
    {
        [Required(ErrorMessage = "create_date is null null ")]
        [IgnoreMap] public virtual DateTime? CreateDate { get; set; }
        [Required]
        public virtual long? CatagoryId { get; set; }
        [Required]
        public virtual long? UserId { get; set; }
    }
    [AutoMap(typeof(EdutionEntity))]
    public class UpdateEdutionInput : BaseEdutionInput
    {
        [Required]
        public virtual long? CatagoryId { get; set; }
        [Required]
        public virtual long? UserId { get; set; }
        [Required(ErrorMessage = "id is null null ")]
        // [Compare]
        public virtual long? Id { get; set; }
        [Required(ErrorMessage = "update_date is null null ")]
        [IgnoreMap] public virtual DateTime? UpdateDate { get; set; }
    }
    [AutoMap(typeof(EdutionEntity))]
    public class EdutionInput
    {
        public virtual long? CatagoryId { get; set; }
        public virtual long? UserId { get; set; }
    }
    [AutoMap(typeof(EdutionEntity))]
    public class EdutionDto: BaseEdutionInput
    {
        public virtual long? CatagoryId { get; set; }
        public virtual long? UserId { get; set; }
    }
    #endregion Edution
}
