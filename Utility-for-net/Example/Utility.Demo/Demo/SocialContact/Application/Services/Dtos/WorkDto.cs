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
    #region work
    public abstract class BaseWorkInput
    {
        [Required]
        public virtual string CompanyName { get; set; }
        [Required]
        public virtual string Job { get; set; }
        [Required]
        public virtual long? CatagoryId { get; set; }
        [Required]
        public virtual string Description { get; set; }
        [Required]
        [IgnoreMap] public virtual DateTime? StartDate { get; set; }
        [Required]
        [IgnoreMap] public virtual DateTime? EndDate { get; set; }
        [Required]
        public virtual long? UserId { get; set; }
    }
    [AutoMap(typeof(WorkEntity))]
    public class CreateWorkInput : BaseWorkInput
    {
        [Required(ErrorMessage = "create_date is null null ")]
        [IgnoreMap] public virtual DateTime? CreateDate { get; set; }
    }
    [AutoMap(typeof(WorkEntity))]
    public class UpdateWorkInput : BaseWorkInput
    {
        [Required(ErrorMessage = "id is null null ")]
        // [Compare]
        public virtual long? Id { get; set; }
        [Required(ErrorMessage = "update_date is null null ")]
        [IgnoreMap] public virtual DateTime? UpdateDate { get; set; }
    }
    [AutoMap(typeof(WorkEntity))]
    public class WorkInput
    {
        public virtual string CompanyName { get; set; }
        public virtual string Job { get; set; }
        public virtual long? CatagoryId { get; set; }
        public virtual string Description { get; set; }
        [Newtonsoft.Json.JsonProperty("start_date")]
        [IgnoreMap] public virtual DateTime[] StartDates { get; set; }
        [Newtonsoft.Json.JsonProperty("end_date")]
        [IgnoreMap] public virtual DateTime[] EndDates { get; set; }
        public virtual long? UserId { get; set; }
    }
    [AutoMap(typeof(WorkEntity))]
    public class WorkDto
    {
        public virtual string CompanyName { get; set; }
        public virtual string Job { get; set; }
        public virtual long? CatagoryId { get; set; }
        public virtual string Description { get; set; }
        [IgnoreMap] public virtual DateTime? StartDate { get; set; }
        [IgnoreMap] public virtual DateTime? EndDate { get; set; }
        public virtual long? UserId { get; set; }

        [IgnoreMap] public virtual DateTime CreateDate { get; set; }
        [IgnoreMap] public virtual DateTime UpdateDate { get; set; }
    }
    #endregion work
}
