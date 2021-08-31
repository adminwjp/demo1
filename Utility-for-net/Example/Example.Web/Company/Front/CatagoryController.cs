using Company.Domain.Entities;
using Company.Ef;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.AspNetCore.Controllers;
using Utility.Attributes;
using Utility.Ef;
using Utility.Ef.Repositories;

namespace Company.Api.Front
{
    //ef linq select 最后一步使用 否则 使用 ex while reader null () 
    [Area("company")]
    [Route("company/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    [Transtation(UseTranstation =false)]
    public class CatagoryController : BaseController
    {
        new BaseEfRepository<CompanyDbContext, CompanyCatagoryEntity, long> Repository;
        public CatagoryController(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            //ex 哪里 冲突 排查麻烦 要么 手动注册 ioc 不要用泛型
            //Repository = IocManager.Get<BaseEfRepository<CompanyDbContext, Entity, long>>("CompanyRepository");

            Repository = new BaseEfRepository<CompanyDbContext, CompanyCatagoryEntity, long>(IocManager.Get<DbContextProvider<CompanyDbContext>>());
      
        }
        [HttpGet("testimonials")]
        public IActionResult Testimonial()
        {

            //name desc   Catagory_id(Catagory) 
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            //Catagory//name desc back Catagory_id(Basic)
            var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.TestimonialPerson && (!it.Enable.HasValue || it.Enable.Value == true))
               .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None && (!it.Enable.HasValue || it.Enable.Value == true)), it => it.parent_id, it => it.Id, (it, it1) => new
            {
                   it.Name,
                   it.Description,
                   it.Logo,
                   Category = new
                   {
                       it1.Id,
                       it1.Name,
                       it1.Description,
                       it1.BackgroundImage
                   }
               })
            .ToList();
            var temp = new List<dynamic>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.Category.Id);
                if (it == null)
                {
                    it = new
                    {
                        item.Category.Id,
                        item.Category.Name,
                        item.Category.Description,
                        item.Category.BackgroundImage,
                        Testimonials = new List<dynamic>()
                    };
                    temp.Add(it);
                }
                it.Testimonials.Add(new {
                    item.Name,
                    item.Description,
                    item.Logo
                });
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }

        [HttpGet("About")]
        public virtual ResponseApi About() {
            //name desc back logo title
            if(GetLanguage()!= Language.Chinese)
            {
                var data = Repository.Query(it =>
                it.Flag== CompanyCatagoryFlag.About&& (!it.Enable.HasValue || it.Enable.Value==true))
                    .Select(it => new {
                    it.Id,
                    //it.Name,
                    //it.Description,
                    it.BackgroundImage,
                    it.Logo,
                    //it.Title
                })
                    .Join(Repository.UnitWork.Query<LangeEntity>(it=> 
                    it.Flag == CompanyCatagoryFlag.About
                    //&&it.Lanage==""
                    ),a=>a.Id,it=>it.RelationId,(a,it)=>new {
                        a.Id,
                        Name=it.Val1,
                        Description=it.Val2,
                        a.BackgroundImage,
                        a.Logo,
                        Title=it.Val3,
                    })
                    .ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.About
                &&(!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.Logo,
                    it.Title
                }).FirstOrDefault();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }

        [HttpGet("Main")]
        public virtual ResponseApi Main()
        {
            //name desc back btnxxname btnxxxhref
            if (GetLanguage() != Language.Chinese)
            {
                var data = Repository.Query(it =>
                it.Flag == CompanyCatagoryFlag.About && (!it.Enable.HasValue || it.Enable.Value == true))
                    .Select(it => new {
                        it.Id,
                       // it.Name,
                       // it.Description,
                        it.BackgroundImage,
                       // it.ButtonName1,
                       // it.ButtonName2,
                        it.ButtonHref1,
                        it.ButtonHref2
                    })
                    .Join(Repository.UnitWork.Query<LangeEntity>(it =>
                    it.Flag == CompanyCatagoryFlag.About
                    //&&it.Lanage==""
                    ), a => a.Id, it => it.RelationId, (a, it) => new {
                        a.Id,
                        Name=it.Val1,
                        Description=it.Val2,
                        a.BackgroundImage,
                        ButtonName1=it.Val3,
                        ButtonName2=it.Val4,
                        a.ButtonHref1,
                        a.ButtonHref2
                    })
                    .ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Main
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.ButtonName1,
                    it.ButtonName2,
                    it.ButtonHref1,
                    it.ButtonHref2
                }).ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }

        [HttpGet("Basic")]
        public virtual ResponseApi Basic()
        {
            //name 
            if (GetLanguage() != Language.Chinese)
            {
                var data = Repository.Query(it =>
                it.Flag == CompanyCatagoryFlag.BasicCategory && (!it.Enable.HasValue || it.Enable.Value == true))
                    .Select(it => new {
                        it.Id
                    })
                    .Join(Repository.UnitWork.Query<LangeEntity>(it =>
                    it.Flag == CompanyCatagoryFlag.About
                    //&&it.Lanage==""
                    ), a => a.Id, it => it.RelationId, (a, it) => new {
                        a.Id,
                        Name = it.Val1,
                    })
                    .ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.BasicCategory
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name
                }).ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }


        [HttpGet("Catagory")]
        public virtual ResponseApi Catagory()
        {
            //name desc back Catagory_id(Basic)
            if (GetLanguage() != Language.Chinese)
            {
                var data = Repository.Query(it =>
                it.Flag == CompanyCatagoryFlag.BasicCategory && (!it.Enable.HasValue || it.Enable.Value == true))
                    .Select(it => new {
                        it.Id,
                        it.BackgroundImage,
                        it.parent_id
                    })
                    .Join(Repository.UnitWork.Query<LangeEntity>(it =>
                    it.Flag == CompanyCatagoryFlag.About
                    //&&it.Lanage==""
                    ), a => a.Id, it => it.RelationId, (a, it) => new {
                        a.Id,
                        Name = it.Val1,
                        Description = it.Val2,
                        a.BackgroundImage,
                        a.parent_id
                    }).GroupJoin(Repository.Query(it => it.Flag == CompanyCatagoryFlag.BasicCategory
                && (!it.Enable.HasValue || it.Enable.Value == true)).Join(Repository.UnitWork.Query<LangeEntity>(it =>
                   it.Flag == CompanyCatagoryFlag.About
                    //&&it.Lanage==""
                    ), a => a.Id, it => it.RelationId, (a, it) => new {
                        a.Id,
                        Name = it.Val1
                    }),
                it => it.parent_id, p => p.Id, (it, p) => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.parent_id,
                    p
                }).SelectMany(it => it.p.DefaultIfEmpty(), (it, p) => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.parent_id,
                    p,//asp net core restful not output
                    //BasicCategory
                   // CatagoryName = p.Name,
                    //PId = p.Id
                })
                    .ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.None
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.parent_id
                }).GroupJoin(Repository.Query(it => it.Flag == CompanyCatagoryFlag.BasicCategory
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it=>new { it.Id,it.Name}),
                it=>it.parent_id, p=>p.Id,(it,p)=>new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.parent_id,
                    p
                }).SelectMany(it=>it.p.DefaultIfEmpty(),(it, p)=>new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage,
                    it.parent_id,
                    p //asp net core restful not output
                      //BasicCategory
                      //ex null while reader
                      //CatagoryName = p.Name,
                      // PId=p.Id
                }).ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }
        //Partners  Brand
        [HttpGet("Partners")]
        public virtual ResponseApi Partners()
        {
            //Catagory//name desc back Catagory_id(Basic)
            //name desc logo href fea Catagory_id(Catagory)
            if (GetLanguage() == Language.English)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Brand
               && it.Feature == null && (!it.Enable.HasValue || it.Enable.Value == true)).Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None
               && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new { it.Id, it.Name, it.Description, it.BackgroundImage }),
                it => it.parent_id, p => p.Id, (it, p) => new {
                    it.Id,
                    it.Logo,
                    it.Href,
                    Category = new
                    {
                        p.Id,
                        p.Name,
                        p.Description,
                        p.BackgroundImage
                    }
                }).ToList();
                var temp = new List<dynamic>(data.Count);
                foreach (var item in data)
                {
                    var it = temp.Find(it => it.Id == item.Category.Id);
                    if (it == null)
                    {
                        it = new
                        {
                            item.Category.Id,
                            item.Category.Name,
                            item.Category.Description,
                            item.Category.BackgroundImage,
                            OurPartners = new List<dynamic>()
                        };
                        temp.Add(it);
                    }
                    it.OurPartners.Add(new
                    {
                        item.Id,
                        item.Logo,
                        item.Href
                    });
                }
                var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
                respone.Data = temp.FirstOrDefault();
                return respone;
            }
        }
        //Features  Brand
        [HttpGet("features")]
        public virtual ResponseApi Features()
        {
            //Catagory//name desc back Catagory_id(Basic)
            //name desc logo href fea Catagory_id(Catagory)
            if (GetLanguage() == Language.English)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Brand
               && it.Feature != null && (!it.Enable.HasValue || it.Enable.Value == true))
                    .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new { it.Id, it.Name,it.Description,it.BackgroundImage }),
                it => it.parent_id, p => p.Id, (it, p) => new {
                    it.Id,
                    it.Logo,
                    it.Href,
                    it.Feature,
                    it.parent_id,
                    it.Name,
                    it.Description,
                    Category = new
                    {
                        p.Id,
                        p.Name,
                        p.Description,
                        p.BackgroundImage
                    }
                }).ToList();
                var temp = new List<dynamic>(data.Count);
                foreach (var item in data)
                {
                    var it = temp.Find(it => it.Id == item.Category.Id);
                    if (it == null)
                    {
                        it = new
                        {
                            item.Category.Id,
                            item.Category.Name,
                            item.Category.Description,
                            item.Category.BackgroundImage,
                            Features = new List<dynamic>()
                        };
                        temp.Add(it);
                    }
                    it.Features.Add(new
                    {
                        item.Id,
                        item.Logo,
                        item.Href,
                        item.Feature,
                        item.parent_id,
                        item.Name,
                        item.Description,
                    });
                }
                var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
                respone.Data = temp.FirstOrDefault();
                return respone;
            }
        }
        
        [HttpGet("Company")]
        public virtual ResponseApi Company()
        {
            //name desc logo logo1 tel
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Company
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Logo,
                    it.Logo1,
                    it.Tel,
                    it.Name,
                    it.Description,
                }).FirstOrDefault();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }
        
        [HttpGet("Media")]
        public virtual ResponseApi Media()
        {
            //name body 
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Media
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Body,
                }).ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }

        [HttpGet("services")]
        public virtual ResponseApi Services()
        {
            //Catagory//name desc back Catagory_id(Basic)
            //name desc logo Catagory_id(Catagory)
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Service
                && (!it.Enable.HasValue || it.Enable.Value == true)).Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage
                }),a=>a.parent_id,p=>p.Id,(it,p)=>new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.Logo,
                    it.parent_id,
                    p
                }).ToList();
                List<dynamic> result = new List<dynamic>(data.Count);
                foreach (var item in data)
                {
                    var it = result.Find(it => it.Id == item.p.Id);
                    if (it == null)
                    {
                        it = new
                        {
                            item.p.Id,
                            item.p.Name,
                            item.p.Description,
                            item.p.BackgroundImage,
                            Services = new List<dynamic>()
                    };
                        result.Add(it);
                    }
                    it.Services.Add(new {
                        item.Id,
                        item.Name,
                        item.Description,
                        item.Logo
                   });
                }
                return ResponseApi.CreateSuccess().SetData(result.FirstOrDefault());
            }
        }

        [HttpGet("skills")]
        public virtual ResponseApi Skill()
        {
            //Catagory//name desc back Catagory_id(Basic)
            //name proc style Catagory_id(Catagory)
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Skill
                && (!it.Enable.HasValue || it.Enable.Value == true))
                    .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Description,
                    it.BackgroundImage
                }), a => a.parent_id, p => p.Id, (it, p) => new {
                    it.Id,
                    it.Name,
                    it.Process,
                    it.Style,
                    it.parent_id,
                    p
                }).ToList();
                List<dynamic> result = new List<dynamic>(data.Count);
                foreach (var item in data)
                {
                    var it = result.Find(it => it.Id == item.p.Id);
                    if (it == null)
                    {
                        it = new
                        {
                            item.p.Id,
                            item.p.Name,
                            item.p.Description,
                            item.p.BackgroundImage,
                            Skills = new List<dynamic>()
                        };
                        result.Add(it);
                    }
                    it.Skills.Add(new
                    {
                        item.Id,
                        item.Name,
                        item.Process,
                        item.Style
                    });
                }
                return ResponseApi.CreateSuccess().SetData(result.FirstOrDefault());
            }
        }

        [HttpGet("skin")]
        public virtual ResponseApi Skin()
        {
            //name color
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Skin
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Name,
                    it.Color
                }).ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }


        [HttpGet("social")]
        public virtual ResponseApi Social()
        {
            //icon href
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            else
            {
                var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Social
                && (!it.Enable.HasValue || it.Enable.Value == true)).Select(it => new {
                    it.Id,
                    it.Icon,
                    it.Href
                }).ToList();
                return ResponseApi.CreateSuccess().SetData(data);
            }
        }

        [HttpGet("teams")]
        public ResponseApi Teams()
        {
            //name logo  Catagory_id(Catagory) service_id
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            //Catagory//name desc back Catagory_id(Basic)
            var data = Repository.Query(it =>it.Flag== CompanyCatagoryFlag.Team&& 
            (!it.Enable.HasValue || it.Enable.Value == true))
               .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None &&
            (!it.Enable.HasValue || it.Enable.Value == true)), it => it.parent_id, it => it.Id, (it, it1) => new
            {
                Category = new
                {
                    it1.Id,
                    it1.Name,
                    it1.Description,
                    it1.BackgroundImage
                },
                it
            })
            .GroupJoin(Repository.UnitWork.Query<RelationEntity>(it => it.Flag == "team_service")
            .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.Service && (!it.Enable.HasValue || it.Enable.Value == true)),
            it => it.Fk2, it => it.Id, (it, it1) => new {
                // it.Category,
                it.Fk1,
                Team = new TeamDto()
                {
                    Service = new
                    {
                        it1.Name,
                    }
                }
            })
            , a=>a.it.Id, s=>s.Fk1,(t,s)=>new {
                t,
                s
            }).SelectMany(it => it.s.DefaultIfEmpty(), (it, it1) => new
            {
                // it.Category,
                Team = new TeamDto()
                {
                    Service = it1.Team.Service,
                    Category=it.t.Category,
                    Id = it.t.it.Id,
                    Name = it.t.it.Name,
                    Logo = it.t.it.Logo,
                }
            })
            .GroupJoin(
               
                Repository.UnitWork.Query<RelationEntity>(it => it.Flag == "team_social")
           .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.Social)
            , a => a.Fk2, s => s.Id, (t, s) => new {
               t,
                s
            }),
                a => a.Team.Id, s => s.t.Fk1, (t, s) => new {
                t,
                s
            }).SelectMany(it => it.s.DefaultIfEmpty(), (it, it1) => new
            {
                // it.Category,
                Team = new TeamDto()
                {
                    Service = it.t.Team.Service,
                    Category = it.t.Team.Category,
                    Id = it.t.Team.Id,
                    Name = it.t.Team.Name,
                    Logo = it.t.Team.Logo,
                },
                it1.s.Icon,
                it1.s.Href,
            })
            .ToList();
            var temp = new List<dynamic>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.Team.Category.Id);
                if (it == null)
                {
                    it = new{
                        item.Team.Category.Id,
                        item.Team.Category.Name,
                        item.Team.Category.Description,
                        item.Team.Category.BackgroundImage,
                        Teams = new List<TeamDto>()
                    };
                    temp.Add(it);
                }
                var exists = ((List<TeamDto>)it.Teams).Find(it => it.Id == item.Team.Id);
                if (exists != null)
                {
                    exists.Source += "," + item.Icon;
                    exists.Href += "," + item.Href;
                    continue;
                }
                item.Team.Source = item.Icon;
                item.Team.Href = item.Href;
                it.Teams.Add(item.Team);
            }
            var firstData = temp.FirstOrDefault();
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = firstData;
            return respone;
        }
        public class TeamDto
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Source { get; set; }
            public string Logo { get; set; }
            public string Href { get; set; }
            public dynamic Category { get; set; }
            public dynamic Service { get; set; }
        }
        [HttpGet("themes")]
        public ResponseApi Theme()
        {
            //name href  Catagory_id(Catagory) 
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            //Catagory//name desc back Catagory_id(Basic)

            var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Theme && (!it.Enable.HasValue || it.Enable.Value == true))
                .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None && (!it.Enable.HasValue || it.Enable.Value == true)), it => it.parent_id, it => it.Id, (it, it1) => new
            {
                    Name = it.Name,
                    Href = it.Href,
                    Category = new
                    {
                        it1.Id,
                        it1.Name
                    }
                })
            .ToList();
            var temp = new List<dynamic>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.Category.Id);
                if (it == null)
                {
                    it = new
                    {
                        item.Category.Id,
                        item.Category.Name,
                        Themes = new List<dynamic>()
                    };
                    temp.Add(it);
                }
                it.Themes.Add(new
                {
                    Name = item.Name,
                    Href = item.Href
                });
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp;
            return respone;
        }

        [HttpGet("works")]
        public ResponseApi Works()
        {      
            //logo   Catagory_id(Catagory) 
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            //Catagory//name desc back Catagory_id(Basic)
            var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.Work && (!it.Enable.HasValue || it.Enable.Value == true))
              .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.None && (!it.Enable.HasValue || it.Enable.Value == true)), it => it.parent_id, it => it.Id, (it, it1) => new
              {
                  it.Logo,
                  Category=new
                  {
                      it1.Id,
                      it1.Name,
                      it1.Description,
                      it1.BackgroundImage
                  }
              })
          .ToList();
            var temp = new List<dynamic>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.Category.Id);
                if (it == null)
                {
                    it = new
                    {
                        item.Category.Id,
                        item.Category.Name,
                        item.Category.Description,
                        item.Category.BackgroundImage,
                        Works = new List<dynamic>()
                    };
                    temp.Add(it);
                }
                it.Works.Add(new
                {
                     item.Logo
                });
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return respone;
        }

        [HttpGet("porworks")]
        public ResponseApi PorWorks()
        {
            //name filter work_id   parend_id
            if (GetLanguage() != Language.Chinese)
            {
                return null;
            }
            //Catagory//name desc back Catagory_id(Basic)
            var data = Repository.Query(it => it.Flag == CompanyCatagoryFlag.WorkCategory 
            && (it.Enable.HasValue == false || it.Enable.Value == true)&&it.parent_id.HasValue==true)
                .Include(it => it.Parent)
                .GroupJoin(Repository.UnitWork.Query<RelationEntity>(it => it.Flag == "work_category_work")
                .Join(Repository.Query(it => it.Flag == CompanyCatagoryFlag.Work && (!it.Enable.HasValue || it.Enable.Value == true))
                //.Include(it => it.Parent)
                , it => it.Fk2, it => it.Id, (it, it1) => new { it.Fk1,it1.Logo }
                ),
                it => it.Id, it => it.Fk1, (it, it1) => new { it, it1 }
                ).SelectMany(it => it.it1.DefaultIfEmpty(), (it, it1) =>
                   new  {
                       it1.Logo,
                       it.it
                   })
               .ToList();
            //return ResponseApi.CreateFail().SetData(data);
            var work1 = new {
                //data[0].Work.Category,
                WorkCategories=new List<dynamic>(),
                Works = new List<WorkDto>() };
            dynamic parent = null;
            foreach (var item in data)
            {
                if (parent == null && item.it.Parent != null&& item.it.parent_id==item.it.Id)
                {
                    parent = new
                    {
                        item.it.Id,
                        //ParentId = item.it.Id,
                        item.it.Name,
                        item.it.Description,
                        item.it.Filter,
                        // Children=new List<dynamic>()
                    };
                   // continue;
                }
                var workCategory = work1.WorkCategories.Find(it => it.Id == item.it.parent_id);
                if (item.it.Parent != null&& item.it.parent_id==parent.Id)
                {
                    workCategory = new
                    {
                        Id = item.it.Id,
                        Filter = item.it.Filter,
                        Name = item.it.Name,
                        Ids = new List<int?>()
                    };
                    work1.WorkCategories.Add(workCategory);
                    continue;
                }
                //workCategory.Ids.Add(item.Id);
                var work = work1.Works.Find(it => it.Logo == item.Logo);
                if (work == null)
                {
                    work = new WorkDto (){ Id = item.it.Id, Filter = item.it.Parent.Filter.Trim('.'), Logo = item.Logo };
                    work.Filter = work.Filter == "*" ? string.Empty : work.Filter;
                    work1.Works.Add(work);
                }
                else
                {
                    if (item.it.Parent.Filter.Equals("*") || work.Filter.IndexOf(item.it.Parent.Filter.Trim('.')) > -1)
                    {

                    }
                    else
                    {
                        work.Filter += " " + item.it.Parent.Filter.Trim('.');
                    }
                }
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = new { work1.WorkCategories,work1.Works, Category=parent };
            return respone;
        }
        public class WorkDto
        {
            public long Id { get; set; }
            public string Filter { get; set; }
            public string Logo { get; set; }
        }
        [HttpGet("nav/{flag}")]
        public virtual ResponseApi Nav(int flag)
        {
            //1 include 2 linq 3 from linq
            //name href parent_id
           
            if(GetLanguage()== Language.English)
            {
                
            }
            //mysql 
            List<dynamic> result = null;
            if (flag == 1)
            {
                //error sql:data fromat err
                //select t.* from t left joion t t2 on t.id== t.parent_id where t.flag=2 and t2.parent_id is null
                //[]
                var data = Repository.
                        Query(it => it.Flag == CompanyCatagoryFlag.Nav && it.Enable.HasValue && it.Enable.Value == true
                        && it.parent_id.HasValue == false)
                     //Query(it => it.Flag == CompanyCatagoryFlag.Nav && it.Enable.HasValue&& it.Enable.Value&&it.Id==it.Parent.Id)
                    
                        //sqlite ex close reader 
                    //Query(it => it.Flag == CompanyCatagoryFlag.Nav)
                    .Include(it => it.Children).Select(it => new { it.Id, it.Name, it.Href, it.Children }).ToList();
                result = new List<dynamic>(data.Count);
                foreach (var item in data)
                {
                    var da = result.Find(it => it.Id == item.Id);
                    if (da == null)
                    {
                        da = new { item.Id, item.Name, item.Href, Children = new List<dynamic>() };
                        result.Add(da);
                    }
                    if (item.Children != null && item.Children.Count > 0)
                    {
                        foreach (var it in item.Children)
                        {
                            var d = new { it.Id, it.Name, it.Href };
                            da.Children.Add(d);
                        }
                    }
                }
                return ResponseApi.CreateSuccess().SetData(result);
            }
             else   if (flag == 2)
            {
                //left join sql pass ef linq must val is not null ex
                var data = Repository.Query()//.Select(it=>new { it.Id, it.Name, it.Href ,it.Flag,it.Enable,it.parent_id})
                    .GroupJoin(Repository.Query(
                        //it => it.Flag == CompanyCatagoryFlag.Nav
                   //&& it.Enable.HasValue && it.Enable.Value == true
                    //    && it.parent_id.HasValue == true
                        ),
                    p => p.Id, c => c.parent_id, (it, it1) => new { it,it1})
                    .SelectMany(it=>it.it1.DefaultIfEmpty(), (it, it1)=>
                    new { C= it.it, it1.Id, it1.Name, it1.Href, it1.parent_id })
                    .Where(it => it.C.Flag == CompanyCatagoryFlag.Nav && it.C.Enable.HasValue && it.C.Enable.Value == true
                        && it.C.parent_id.HasValue == false)
                    .ToList();
                result = new List<dynamic>(data.Count);
                Console.WriteLine(data.Count);
                foreach (var item in data)
                {
                    var da = result.Find(it => it.Id == item.parent_id);
                    if (da == null)
                    {
                        da = new { item.C.Id, item.C.Name, item.C.Href, Children = new List<dynamic>() };
                        result.Add(da);
                    }
                    if (item.parent_id.HasValue)
                    {
                        var d= new { item.Id, item.Name, item.Href };
                        da.Children.Add(d);
                    }
                }

            }
            else if (flag == 3)
            {
                //left join
                var da = from it in
                      Repository.Read.
                      Catagories.Where(it => it.Flag == CompanyCatagoryFlag.Nav && !it.parent_id.HasValue)
                         select new { it.Id, it.Name, it.Href } into p
                         from ch in Repository.Read.Catagories.Where(it => it.Flag == CompanyCatagoryFlag.Nav && it.parent_id.HasValue)
                         select new { ch.Id, ch.Name, ch.Href, ch.parent_id, p } into chs
                         where chs.parent_id == chs.p.Id
                         select chs;
            }
           
            return ResponseApi.CreateSuccess().SetData(result);
        }
    }
}
