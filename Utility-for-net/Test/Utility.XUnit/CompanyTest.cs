using Company.Domain.Entities;
using Company.Ef;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database;
using Utility.Database.Provider;
using Utility.Generator;
using Utility.IO;
using Utility.Json;
using Xunit;
using Xunit.Abstractions;
using Utility.Ioc;
using Autofac;
using Utility.Domain.Uow;
using Utility.Ef.Uow;
using Utility.Interceptors;

namespace Utility.Ef.Xunit
{
    public class CompanyDesign:Utility.Ef.AbstractDesignTimeDbContextFactory<CompanyDbContext>
    {
        public override CompanyDbContext CreateDbContext(string[] args)
        {
            return base.CreateDbContext(args);
        }
    }
    public class CompanyTest
    {
        ITestOutputHelper testOutput;

        static string path = "E:/work/utility/Utility-for-net/Example";
        public static readonly string ConnectionString = "Database=company;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";//mysql

        public CompanyTest(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }

        string db = "company";
        // [Fact]
        //[TestMethod]
        public void TestMethod1()
        {
            //new MySqlDbProvider().FindTable(null,db);
        }
        void SetUp()
        {
            //autofac 生命周期
            //https://www.cnblogs.com/supersnowyao/p/8455076.html
            AutofacIocManager.Instance.Builder.RegisterModule(new Company.CompanyModule());
            AutofacIocManager.Instance.SingleInstance<IocTranstationAopInterceptor, IocTranstationAopInterceptor>();
        }

        public static DbConnection Connection { get; set; }
        void CleanId(CompanyCatagoryEntity c)
        {
            c.Id = 0;
            c.Enable = true;
            c.parent_id = null;
        }
        [Fact]
        //删除 表 数据麻烦 直接删除 库 外键 关联 麻烦 按规则删除
        public void UpdateDbData()
        {
            SetUp();
            //数据整理 外键 有问题 怎么说 查询 不出来啊
            //表结构 改变 了 数据怎么整理 出来 真 尼玛 麻烦
            //id 不好 控制 关联 数据
            Connection = new MySqlConnection(ConnectionString);
            var tabs = AbstractDbProvider.FindTable(Connection, "company", SqlType.MySql);
            Dictionary<string, dynamic> datas = new Dictionary<string, dynamic>();
           
            var emptyList = new List<dynamic>();
            foreach (var tab in tabs)
            {
                var list = Connection.Query($"select * from {tab} order by id asc");
                datas.Add(tab, list ?? emptyList);
            }

            dynamic imgs = datas["image_info"];
            List<ImageEntity> imageEntities = new List<ImageEntity>(1000);
            foreach (var item in imgs)
            {
                ImageEntity a = JsonHelper.ToObject<ImageEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                imageEntities.Add(a);
            }
       
            List<Tuple<CompanyCatagoryFlag,long, CompanyCatagoryEntity>> ids = new List<Tuple< CompanyCatagoryFlag, long, CompanyCatagoryEntity>>();
            
            List<LangeEntity> langeEntities = new List<LangeEntity>(1000);
            List<RelationEntity> relationEntities = new List<RelationEntity>();

            IUnitWork efUnitWork =  AutofacIocManager.Instance.Resolver<IUnitWork>("CompanyUnitWork");
            efUnitWork.UseTransaction = false;
             dynamic abouts = datas["about_info"];
            foreach (var item in abouts)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.About;
                CleanId(c);
                 ImageEntity img = imageEntities.Find(it => it.Id == item.background_image);
                c.BackgroundImage = img.Src;
                if(img.Type== "image")
                {
                    c.BackgroundImage = $"upload/imgs/{c.BackgroundImage}";
                }
                else
                {
                    c.BackgroundImage = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.BackgroundImage}";
                }
                img = imageEntities.Find(it => it.Id == item.image);
                c.Logo = img.Src;
                if (img.Type == "image" )
                {
                    c.Logo = $"upload/imgs/{c.Logo}";
                }
                else
                {
                    c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                }
                efUnitWork.Insert(c);
                efUnitWork.Save();
                langeEntities.Add(new LangeEntity() { 
                    Val1=item.english_name ,
                    Val2=item.english_description,
                    Val3=item.english_title ,
                    Description= "name,description,title",
                    RelationId= c.Id,
                    RelationTable= "t_catagory"
                });
                //efUnitWork.Insert(langeEntities[0]);
               // efUnitWork.Save();
                //langeEntities.Clear();
            }

            dynamic main = datas["main_info"];
            foreach (var item in main)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                CleanId(c);
                c.Flag = CompanyCatagoryFlag.Main;
                var img = imageEntities.Find(it => it.Id == item.background_image);
                if (img != null)
                {
                    c.BackgroundImage = img.Src;
                    if (img.Type == "image"  )
                    {
                        c.BackgroundImage = $"upload/imgs/{c.BackgroundImage}";
                    }
                    else
                    {
                        c.BackgroundImage = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.BackgroundImage}";
                    }
                }
                efUnitWork.Insert(c);
                efUnitWork.Save(); 
                langeEntities.Add(new LangeEntity()
                {
                    Val1 = item.english_name,
                    Val2 = item.english_button_name1,
                    Val3 = item.english_button_name2,
                    Val4=item.english_description,
                    Description = "name,button_name1,button_name2,description",
                    RelationId = c.Id,
                    RelationTable = "t_catagory"
                });
            }
          
            dynamic basic = datas["basic_category_info"];
            foreach (var item in basic)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.BasicCategory;
                ids.Add(new Tuple<CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                CleanId(c);
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setName(langeEntities, c, item);
            }

            dynamic category = datas["category_info"];
            foreach (var item in category)
            {
                var str = JsonHelper.ToJson(item);
                //testOutput.WriteLine(str);
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                   str, JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.None;
                ids.Add(new Tuple<CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                CleanId(c);
               
                if (!(item.background_image is DBNull))
                {
                    var img = imageEntities.Find(it => it.Id == item.background_image);
                    if (img != null)
                    {
                        c.BackgroundImage = img.Src;
                        if (img.Type == "image" )
                        {
                            c.BackgroundImage = $"upload/imgs/{c.BackgroundImage}";
                        }
                        else
                        {
                            c.BackgroundImage = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.BackgroundImage}";
                        }
                    }
                }
                if (item.category_id != null)
                {
                    c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.BasicCategory).Item3;
                    //c.parent_id = c.Parent.Id;

                    //c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                    //c.Parent.Children.Add(c); c.Parent = null;
                }
                else
                {
                    //catagoryEntities.Add(c);
                }
                efUnitWork.Insert(c);
                efUnitWork.Save();
                //ids.Add(new Tuple<string,CompanyCatagoryFlag, long>("t_catagory", c.Flag, c.Id), c);
                setNameAndDesc(langeEntities, c, item);

            }

            dynamic brand = datas["brand_info"];
            foreach (var item in brand)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Brand;
                ids.Add(new Tuple<CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                CleanId(c);
                var  img = imageEntities.Find(it => it.Id == item.logo);
                if (img != null)
                {
                    c.Logo = img.Src;
                    if (img.Type == "image" )
                    {
                        c.Logo = $"upload/imgs/{c.Logo}";
                    }
                    else
                    {
                        c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                    }
                }
                //catagoryEntities
                c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.None).Item3;
                //c.parent_id = c.Parent.Id;
                //c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                //c.Parent.Children.Add(c); c.Parent = null;
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setNameAndDesc(langeEntities, c, item);
            
            }


            dynamic company = datas["company_info"];
            foreach (var item in company)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                CleanId(c); 
                c.Flag = CompanyCatagoryFlag.Company;
                var img = imageEntities.Find(it => it.Id == item.logo);
                if (img != null)
                {
                    c.Logo = img.Src;
                    if (img.Type == "image" )
                    {
                        c.Logo = $"upload/imgs/{c.Logo}";
                    }
                    else
                    {
                        c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                    }
                }
                c.Id = 0;
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setNameAndDesc(langeEntities, c, item);
            }

            dynamic media = datas["media_info"];
            foreach (var item in media)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Media;
                CleanId(c);
                efUnitWork.Insert(c);
                efUnitWork.Save();
                langeEntities.Add(new LangeEntity()
                {
                    Val1 = item.english_name,
                    Val2 = item.english_body,
                    Description = "name,body",
                    RelationId = c.Id,
                    RelationTable = "t_catagory"
                });
            }

            dynamic nav = datas["nav_info"];
            foreach (var item in nav)
            {
                //testOutput.WriteLine(JsonHelper.ToJson(item));
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Nav;
                ids.Add(new Tuple<CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                CleanId(c);
                c.Parent=ids.Find(it => it.Item2 == item.parent_id && it.Item1 == CompanyCatagoryFlag.Nav)?.Item3;
                if (c.Parent == c)
                {
                    c.Parent = null;
                }
                efUnitWork.Insert(c);
                efUnitWork.Save();
                //if (c.Parent == null)
                {
                    //    c.parent_id = c.Id;
                    //    efUnitWork.Update(c);
                    //    efUnitWork.Save();
                }
                langeEntities.Add(new LangeEntity()
                {
                    Val1 = item.english_name,
                    Description = "name",
                    RelationId = c.Id,
                    RelationTable = "t_catagory"
                });
            }

            dynamic service = datas["service_info"];
            foreach (var item in service)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                //catagoryEntities.Add(c);
                c.Flag = CompanyCatagoryFlag.Service;
                ids.Add(new Tuple< CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));

                CleanId(c);
                var img = imageEntities.Find(it => it.Id == item.img);
                if (img != null)
                {
                    c.Logo = img.Src;
                    if (img.Type == "image" )
                    {
                        c.Logo = $"upload/imgs/{c.Logo}";
                    }
                    else
                    {
                        c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                    }
                }
                c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.None).Item3;
                //c.parent_id = c.Parent.Id;
                //c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                //c.Parent.Children.Add(c); c.Parent = null;
               efUnitWork.Insert(c);
                efUnitWork.Save();
                setNameAndDesc(langeEntities, c, item);
            }

            dynamic skill = datas["skill_info"];
            foreach (var item in skill)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                //catagoryEntities.Add(c);
                CleanId(c); 
                c.Flag = CompanyCatagoryFlag.Skill;
                c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.None).Item3;
                //c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                //c.Parent.Children.Add(c);
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setName(langeEntities, c, item);
            }

            dynamic skin = datas["skin_info"];
            foreach (var item in skin)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Skin;
                CleanId(c); 
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setName(langeEntities, c, item);
            }

            dynamic social = datas["social_info"];
            foreach (var item in social)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Social;
                ids.Add(new Tuple< CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                CleanId(c);
                efUnitWork.Insert(c);
                efUnitWork.Save();
            }

            dynamic team = datas["team_info"];
            foreach (var item in team)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                    JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                //testOutput.WriteLine(JsonHelper.ToJson(item));
                c.Flag = CompanyCatagoryFlag.Team;
                ids.Add(new Tuple< CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                var img = imageEntities.Find(it => it.Id == item.img);
                if (img != null)
                {
                    c.Logo = img.Src;
                    if (img.Type == "image" )
                    {
                        c.Logo = $"upload/imgs/{c.Logo}";
                    }
                    else
                    {
                        c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                    }
                }
                c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.None).Item3;
                //c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                // c.Parent.Children.Add(c); c.Parent = null;
                CleanId(c);
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setName(langeEntities, c, item);
                RelationEntity relation = new RelationEntity()
                {
                    Fk1 = c.Id,
                    Fk2 = ids.Find(it => it.Item2 == item.service_id && it.Item1 == CompanyCatagoryFlag.Service).Item3.Id,
                    Flag = "team_service"
                };
                relationEntities.Add(relation);
            }


            dynamic testimonial_person = datas["testimonial_person_info"];
            foreach (var item in testimonial_person)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                 JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.TestimonialPerson;
                var img = imageEntities.Find(it => it.Id == item.person_pic);
                if (img != null)
                {
                    c.Logo = img.Src;
                    if (img.Type == "image" )
                    {
                        c.Logo = $"upload/imgs/{c.Logo}";
                    }
                    else
                    {
                        c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                    }
                }
                CleanId(c);
               c.Parent = ids.Find(it => it.Item2 == item.testimonial_id && it.Item1 == CompanyCatagoryFlag.None)?.Item3;

                // c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                // c.Parent.Children.Add(c); c.Parent = null;

                efUnitWork.Insert(c);
                efUnitWork.Save();
                setNameAndDesc(langeEntities, c, item);
            }

            dynamic team_source = datas["team_source_info"]; 
            foreach (var item in team_source)
            {
                RelationEntity relation = new RelationEntity()
                {
                    Fk1 = ids.Find(it => it.Item2 == item.team_id && it.Item1 == CompanyCatagoryFlag.Team).Item3.Id,
                    Fk2 = ids.Find(it => it.Item2 == item.social_id && it.Item1 == CompanyCatagoryFlag.Social).Item3.Id,
                    //Fk1 = item.team_id,
                    // Fk2 = item.social_id,
                    Flag = "team_social"
                };
                relationEntities.Add(relation);
            }

            dynamic theme = datas["theme_info"];
            foreach (var item in theme)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Theme; 
                ids.Add(new Tuple< CompanyCatagoryFlag, long,CompanyCatagoryEntity>( c.Flag, c.Id, c));
                c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.None).Item3;
                //c.Parent.Children ??= new List<CompanyCatagoryEntity>();
                //c.Parent.Children.Add(c); c.Parent = null;
                CleanId(c); 
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setName(langeEntities, c, item);
            }

            dynamic work = datas["work_info"];
            foreach (var item in work)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                c.Flag = CompanyCatagoryFlag.Work;
                ids.Add(new Tuple< CompanyCatagoryFlag, long,CompanyCatagoryEntity>(c.Flag, c.Id, c)); 
                var img = imageEntities.Find(it => it.Id == item.img_id);
                if (img != null)
                {
                    c.Logo = img.Src;
                    if (img.Type == "image" )
                    {
                        c.Logo = $"upload/imgs/{c.Logo}";
                    }
                    else
                    {
                        c.Logo = $"upload/imgs/{img.Type}{(img.Type == "bg" ? "" : "s")}/{c.Logo}";
                    }
                }
                c.Parent = ids.Find(it => it.Item2 == item.category_id && it.Item1 == CompanyCatagoryFlag.None).Item3;
                CleanId(c); 
                efUnitWork.Insert(c);
                efUnitWork.Save();
                setName(langeEntities, c, item);
            }

            dynamic work_category = datas["work_category_info"];
            foreach (var item in work_category)
            {
                CompanyCatagoryEntity c = JsonHelper.ToObject<CompanyCatagoryEntity>(
                JsonHelper.ToJson(item), JsonHelper.JsonSerializerSettings);
                //testOutput.WriteLine(JsonHelper.ToJson(item));
                c.Flag = CompanyCatagoryFlag.WorkCategory;
                ids.Add(new Tuple< CompanyCatagoryFlag, long,CompanyCatagoryEntity>(c.Flag, c.Id, c));
                CleanId(c); 
                

                // to see the conflicting key
                efUnitWork.Insert(c);
                efUnitWork.Save();
                c.Parent = ids.Find(it => it.Item2 == item.parent_id && it.Item1 == CompanyCatagoryFlag.WorkCategory)?.Item3;
                if (c.Parent != null)
                {
                    c.Parent = efUnitWork.FindSingle<CompanyCatagoryEntity>(it => it.Id == c.Parent.Id);
                    //c==c.Parent
                    if (c.Equals(c.Parent))
                    {
                        c.Parent = null;
                        testOutput.WriteLine("clean");
                    }
                    else
                    {
                        if (c.Parent != null)
                        {
                            // to see the conflicting key
                            //efUnitWork.Update(c);
                            //efUnitWork.Save();
                            efUnitWork.Connection.Execute($"update {CompanyCatagoryEntity.Table} set parent_id=@parent_id where id=@id",new { parent_id =c.Parent.Id,id=c.Id});
                            c.Parent = null;
                            testOutput.WriteLine("set fk");
                        }
                    }
                }
                setName(langeEntities, c, item);
                if (item.work_id != null)
                {
                    RelationEntity relation = new RelationEntity()
                    {
                        Fk1 = c.Id,
                        Fk2 = //item.work_id,
                        ids.Find(it => it.Item2 == item.work_id && it.Item1 == CompanyCatagoryFlag.Work).Item3.Id,
                        Flag = "work_category_work"
                    };
                    relationEntities.Add(relation);
                }
         
            }

            if (relationEntities.Count > 0)
            {
                efUnitWork.BatchInsert(relationEntities.ToArray());
                efUnitWork.Save();
            }
            if (langeEntities.Count > 0)
            {
                efUnitWork.BatchInsert(langeEntities.ToArray());
                efUnitWork.Save();
            }


            // FileHelper.WriteFile(path.Replace("Example",
            //     "Test/Utility.Ef.Xunit/json/data.json"),
            //    JsonHelper.ToJson(datas));
        }
        void set(List<CompanyCatagoryEntity> catagoryEntities)
        {
            foreach (var item in catagoryEntities)
            {
                item.Id = 0;
                item.Parent = null;
                
                if (item.Children != null)
                {
                    set(item.Children);
                }
            }
        }

      void setName(List<LangeEntity> langeEntities, CompanyCatagoryEntity c, dynamic item)
        {
            langeEntities.Add(new LangeEntity()
            {
                Val1 = item.english_name is DBNull ? "" : item.english_name,
                Description = "name",
                RelationId = c.Id,
                Flag=c.Flag,
                RelationTable = "t_catagory"
            });
        }
        void setNameAndDesc(List<LangeEntity> langeEntities,CompanyCatagoryEntity c, dynamic item)
        {
            langeEntities.Add(new LangeEntity()
            {
                Val1 = item.english_name,
                Val2 = item.english_description,
                Description = "name,description",
                RelationId = c.Id,
                Flag = c.Flag,
                RelationTable = "t_catagory"
            });
        }
        //[Fact]
        public void GeneratorEfMap()
        {
            List<Type> types = new List<Type>();
            Type type = typeof(RelationEntity);
            foreach (var item in type.Assembly.Modules)
            {
                foreach (var t in item.GetTypes())
                {
                    if (t.Namespace == type.Namespace)
                    {
                        types.Add(t);
                    }
                }
            }
            //types.Clear();
            //types.Add(typeof(LangeEntity));
            CsharepGeneratorHelper.NameSpace = "Company.Ef.EntityMappings";
            CsharepGeneratorHelper.ParseEntity(types);
            CsharepGeneratorHelper.EntityFrameworkCoreMap.EntityToMap();
            CsharepGeneratorHelper.EntityFrameworkCoreMap.EntityToMapStringCode(path + "/Utility.Demo/Company/Ef/EntityMappings");
        }
    }
}
