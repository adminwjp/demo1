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
    public class BaseCreateInput
    {
        [Required(ErrorMessage ="create_date is null null ")]
        public virtual DateTime? CreateDate { get; set; }
    }
    public class BaseUpdateInput
    {
        [Required(ErrorMessage = "id is null null ")]
        // [Compare]
        public virtual long? Id { get; set; }
        [Required(ErrorMessage = "update_date is null null ")]
        public virtual DateTime? UpdateDate { get; set; }
        
    }
    public class BaseDto
    {
        public virtual long Id { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
  
}
