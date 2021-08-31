using System;
using Dapper;
//using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using Utility.Json;
using Xunit;
using MySqlConnector;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utility.Dapper.Test
{

    //[TestClass]
    public class Class1
    {
        //[Fact]
        //[TestMethod]
        public void Product(){
            var url="Database=jeeshop;Data Source=192.168.1.3;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";
            var connection=new MySqlConnection(url); 

            var catalogSql="select * from t_catalog order by pid ";
            var catalogs=connection.Query(catalogSql).ToList();
            //id name     pid order1 type code showInNav
            Dictionary<long, object> catas=new Dictionary<long, object>();
            Dictionary<long, object> cs = new Dictionary<long, object>();
            Catalog(catas,catalogs,cs);

            var catalogAttrsql= "select * from t_attribute order by pid ";
            //id name catalogID pid order1
            var attributes=connection.Query(catalogAttrsql).ToList();
             Dictionary<long, object> attrs=new Dictionary<long, object>();
            Dictionary<long, object> ats = new Dictionary<long, object>();
            Catalog(attrs,attributes,ats,1);

            var proAttrsql="select * from t_attribute_link";
            //id attrID productID value
            var proAttributes=connection.Query(proAttrsql).ToList();
            Dictionary<long, object> proAttrs=new Dictionary<long, object>();
            foreach(var it in proAttributes)
            {
                    var dic=new Dictionary<string, object>(){
                        ["id"]=it.id,
                        ["attr_id"]=Convert.ToInt64(it.attrID),
                        ["product_id"]=Convert.ToInt64(it.productID),
                        ["value"]=it.value == null ? "":(string)it.value
                    };
                    proAttrs.Add((int)it.id,dic);
            } 

            var proSql="select * from t_product";
            //id name introduce price nowPrice picture createtime createAccount
            //updateAccount updatetime isnew sale hit status productHTML
            //maxPicture images catalogID sellcount stock searchKey title
            //description keywords activityID unit score isTimePromotion giftID
            var pros=connection.Query(proSql).ToList();
            Dictionary<long,object> prods=new Dictionary<long, object>();
            Products(prods,pros,catas);
            //ProductCatalog(prods, proAttrs); //写的啥玩意

            var specSql="select * from t_spec";
            //id  productID specColor specStock specPrice specStatus
            var specs=connection.Query(specSql).ToList();
            Dictionary<long, object> sps=new Dictionary<long, object>();
            Specs(sps,specs,prods);

            foreach (var item in proAttrs)
            {
                var dic = (Dictionary<string, object>)item.Value;
                long proId = (long)dic["product_id"];
                long attr_id = (long)dic["attr_id"];
                if (prods.ContainsKey(proId))
                {
                    Dictionary<string, object> c = null;
                    foreach (var it in attrs)
                    {
                        c = (Dictionary<string, object>)it.Value;
                        if (it.Key == attr_id)
                        {
                            break;
                        }
                        if (c.ContainsKey("children"))
                        {
                            var list = (List<Dictionary<string, object>>)c["children"];
                            Dictionary<string, object> temp = null;
                            foreach (var ls in list)
                            {
                                temp = CursionCatalog(ls, attr_id);
                                if (temp != null)
                                {
                                    break;
                                }
                            }
                            if (temp != null)
                            {
                                c = temp;
                                break;
                            }
                        }
                        c = null;
                    }
                    if (c != null)
                    {
                        Console.WriteLine($"pro attr attr_id:{attr_id} proAttrId:{item.Key} match success");
                        List<Dictionary<string, object>> list =null;
                        if (c.ContainsKey("product_attributes"))
                        {
                             list = (List<Dictionary<string, object>>)c["product_attributes"];
                        }
                        else
                        {
                            list = new List<Dictionary<string, object>>();
                            c.Add("product_attributes", list);
                        }
                        list.Add(dic);
                    }
                    else
                    {
                        Console.WriteLine($"pro attr attr_id:{attr_id} proAttrId:{item.Key} match fail");
                    }
                }
                else
                {
                    Console.WriteLine($"pro attr proId:{proId} proAttrId:{item.Key} match fail");
                }
            }

            //整理 id 不然 乱七八糟
            //id pid
            // 1 0
            // 2 1
            //3 0
            //4 3
            //==
            //1 0
            // 2 0
            //3 1
            //4 2
            //1 catalog 2 catalog_attribute 3 product_attribute 4 product 5 spec
            Dictionary<int, long> ids = new Dictionary<int, long>() { [1]=1,[2]=1,[3]=1, [4] = 1, [5] = 1 };
            //catalog
            long id = ids[1];
            UpdateId(cs,ref id);

            //catalog_attribute
            id = ids[2];
            UpdateId(ats, ref id);

            

            //product
            id = ids[4];
            foreach (var item in prods)
            {
                var dic = (Dictionary<string, object>)item.Value;
                dic["id"] = id;
                //update  catalog_id
                //    var catalog_id =(long)dic["catalog_id"];
                var catalog_id =Convert.ToInt64(dic["catalog_id"]);
                var catalog = (Dictionary<string, object>)cs[catalog_id];
                dic["catalog_id"] = catalog["id"];
                id++;

            }
            //spec
            id = ids[5];
            foreach (var item in sps)
            {
                var dic = (Dictionary<string, object>)item.Value;
                dic["id"] = id;
                //update  product_id
                var product_id = (long)dic["product_id"];
                var product = (Dictionary<string, object>)prods[product_id];
                dic["product_id"] = product["id"];
                id++;
            }

            //product_attribute
            id = ids[3];
            //UpdateId(proAttrs, ref id);
            foreach (var item in proAttrs)
            {
                var dic = (Dictionary<string, object>)item.Value;
                dic["id"] = id;
                //update  product_id attr_id
                long proId = (long)dic["product_id"];
                long attr_id = (long)dic["attr_id"];
                if (prods.ContainsKey(proId) && ats.ContainsKey(attr_id))
                {
                    var product = (Dictionary<string, object>)prods[proId];
                    var attr = (Dictionary<string, object>)ats[attr_id];
                    dic["product_id"] = product["id"];
                    dic["attr_id"] = attr["id"];
                    id++;
                }
                else
                {
                    //match fail
                }

            }
            string json=JsonHelper.ToJson(new { catalogs = catas.Values.ToArray(),attrs=attrs.Values.ToArray() });
           // string json = JsonHelper.ToJson(new {  catas,attrs });
            System.Console.WriteLine(json);
        }
        void UpdateId(Dictionary<long, object> data,ref long id)
        {
            foreach (var item in data)
            {
                var dic = (Dictionary<string, object>)item.Value;
                dic["id"] = id;
                //update pid
                long pid = (long)dic["pid"];
                if (pid > 0)
                {
                    var temp = (Dictionary<string, object>)data[pid];
                    dic["pid"] = temp["id"];
                }
                id++;
            }
        }
        void Specs(Dictionary<long, object> sps,List<dynamic> specs, Dictionary<long, object> prods)
        {
            foreach (var it in specs)
            {
                long id = (long)it.id;
                long productID = Convert.ToInt64(it.productID);
                var dic = new Dictionary<string, object>()
                {
                    ["id"] = id,
                    ["product_id"] = productID,
                    ["color"] = it.specColor,
                    ["stock"] = it.specStock,
                    ["price"] = it.specPrice,
                    ["status"] = it.specStatus,
                };

                if (prods.ContainsKey(productID))
                {
                    Console.WriteLine($"proId:{productID} spenId:{it.id} match success");
                    var p = ((Dictionary<string, object>)prods[productID]);
                    List<Dictionary<string, object>> ss = null;
                    if (p.ContainsKey("specs"))
                    {
                        ss = (List<Dictionary<string, object>>)p["specs"];
                    }
                    else
                    {
                        ss = new List<Dictionary<string, object>>();
                        p.Add("specs", ss);
                    }
                    sps.Add(id, dic);
                    ss.Add(dic);
                }
                else
                {
                    Console.WriteLine($"proId:{productID} spenId:{it.id} match fail");
                }
            }
        }

        void ProductCatalog(Dictionary<long, object> prods,  Dictionary<long, object> proAttrs)
        {
            foreach (var it in proAttrs)
            {
                var dic = (Dictionary<string, object>)it.Value;
                long product_id = (long)dic["product_id"];
                if (prods.ContainsKey(product_id))
                {
                    List<Dictionary<string, object>> ss = null;
                    if (dic.ContainsKey("product_attributes"))
                    {
                        ss = (List<Dictionary<string, object>>)dic["product_attributes"];
                    }
                    else
                    {
                        ss = new List<Dictionary<string, object>>();
                        dic["product_attributes"] = ss;
                    }
                    ss.Add(dic);
                }
                else
                {
                    Console.WriteLine($"product attr proId:{it.Key} product_id:{product_id} match fail");
                }
            }
        }
        
        void Products(Dictionary<long, object> prods,List<dynamic> pros, Dictionary<long, object> catas)
        {
            foreach (var it in pros)
            {
                int catalogID = Convert.ToInt32(it.catalogID);
                Dictionary<string, object> temp = null;
                foreach (var item in catas)
                {
                    var d = (Dictionary<string, object>)item.Value;
                    temp = temp ?? (Dictionary<string, object>)CursionCatalog(d, catalogID);
                    if (temp != null)
                    {
                        break;
                    }
                }

                if (temp == null)
                {
                    Console.WriteLine($"catalogID:{catalogID} proId:{it.id} match fail");
                }
                else
                {
                    Console.WriteLine($"catalogID:{catalogID} proId:{it.id} match success");
                    var dic = new Dictionary<string, object>()
                    {
                        ["id"] = it.id,
                        ["introduce"] = it.introduce,
                        ["price"] = it.price,
                        ["now_price"] = it.nowPrice,
                        ["name"] = it.name,
                        ["picture"] = it.picture,
                        ["createtime"] = it.createtime,
                        ["create_account"] = it.createAccount == null ? "" : (string)it.createAccount,
                        ["update_account"] = it.updateAccount == null ? "" : (string)it.updateAccount,
                        ["updatetime"] = it.updatetime,
                        ["is_new"] = it.isnew,
                        ["sale"] = it.sale,
                        ["hit"] = it.hit,
                        ["status"] = it.status,
                        ["product_html"] = it.productHTML == null ? "" : (string)it.productHTMLis,
                        ["max_picture"] = it.maxPicture == null ? "" : (string)it.productHTMLis,
                        ["images"] = it.images == null ? "" : (string)it.productHTMLis,
                        ["catalog_id"] = catalogID,
                        ["sell_count"] = it.sellcount,
                        ["stock"] = it.stock,
                        ["search_key"] = it.searchKey == null ? "" : (string)it.searchKey,
                        ["title"] = it.title == null ? "" : (string)it.title,
                        ["description"] = it.description == null ? "" : (string)it.description,
                        ["keywords"] = it.keywords == null ? "" : (string)it.keywords,
                        ["activity_id"] = it.activityID == null ? "" : (string)it.activityID,
                        ["unit"] = it.unit,
                        ["score"] = it.score,
                        ["is_time_promotion"] = it.isTimePromotion,
                        ["gift_id"] = it.giftID == null ? "" : (string)it.giftID
                    };
                    List<Dictionary<string, object>> products = null;
                    if (temp.ContainsKey("products"))
                    {
                        products = (List<Dictionary<string, object>>)temp["products"];
                    }
                    else
                    {
                        products = new List<Dictionary<string, object>>();
                        temp["products"] = products;
                    }
                    prods.Add((int)it.id, dic);
                    products.Add(dic);
                }
            }
        }

        void Catalog(Dictionary<long, object> cas,List<dynamic> catalogs, Dictionary<long, object> cs,int falg=0)
        {
             foreach(var it in catalogs)
             {
                var id=(long)it.id;
                var pid=(long)it.pid;
                Dictionary<string, object> temp=null;
                if(pid>0)
                {
                    foreach (var item in cas)
                    {
                        var d=( Dictionary<string, object>)item.Value;
                        temp=temp??CursionCatalog(d,pid);
                        if(temp!=null){
                            //temp.Add("pid",pid);//error
                            break;
                        }
                    }
                }
                var dic=new Dictionary<string, object>(){
                    ["id"]=id,
                    ["orders"]=it.order1==null ? 0:(int)it.order1,
                    ["name"]=it.name,
                    ["pid"] = pid,//not exec
                };
                cs.Add(id,dic);
                if (falg==0){
                    dic ["code"]=it.code;
                    dic["flag"]=it.type=="a"?3:(pid==0&&it.type=="p"?1:2);
                }else{
                    dic ["catalog_id"]= it.catalogID==null?0:Convert.ToInt64(it.catalogID);
                }
               
                if(temp==null){
                   cas.Add(id,dic); 
                }else{
                        List<Dictionary<string,object>> list=null;
                        if(temp.ContainsKey("children")){
                            list=(List<Dictionary<string,object>>)temp["children"];
                        }else{
                            list=new List<Dictionary<string, object>>();
                            //temp["children"]=list;
                            temp.Add("children",list);
                        }
                        list.Add(dic);

                }
             }   

        }

        Dictionary<string,object> CursionCatalog(Dictionary<string,object> data, long pid){
            if(data.Count==0){
                   return (Dictionary<string,object>)null; 
            }
            long id =(long)data["id"];
            if(id==pid){
                return data;  
            }
            if(data.ContainsKey("children"))
            {
                List<Dictionary<string,object>> children=(List<Dictionary<string,object>>)data["children"];
                foreach(var it in children)
                 {
                    var temp=  CursionCatalog(it,pid);
                    if(temp!=null){
                        return temp;
                    }
                }
            }
             return (Dictionary<string,object>)null;
        }
    }

    
}
