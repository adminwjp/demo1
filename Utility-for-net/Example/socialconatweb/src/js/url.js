export const baseUrl = "http://localhost:41059/social_contact/admin/api/v1/";
//export const baseUrl = "https://localhost:5002/admin/api/v1/";
export const urls = {
    role: new BasicUrl("role", {
        parent_category: getUrl("role/parent_category"),
        required: getUrl("role/required")
    }),
    admin: new BasicUrl("admin", {
        role_required: getUrl("admin/role_required"),
        login: getUrl("admin/login")
    }),
    user: new BasicUrl("user"),
    jobcategory: new BasicUrl("jobcategory"),
    likecategory: new BasicUrl("likecategory"),
    like: new BasicUrl("like"),

    skillcategory: new BasicUrl("skillcategory"), skill: new BasicUrl("skill"),
    filecategory: new BasicUrl("filecategory"),
    file: new BasicUrl("file"),
    edutioncategory: new BasicUrl("edutioncategory"),
    userstatus: new BasicUrl("userstatus"),
    userlevel: new BasicUrl("userlevel"),
    usertagcategory: new BasicUrl("usertagcategory"),
    usertag: new BasicUrl("usertag"),
    usermaritalstatus: new BasicUrl("maritalstatus"),
    menu: new BasicUrl("menu", {
        parent_category: getUrl("menu/parent_category")
    }),
    icon: new BasicUrl("icon"), 
    work: new BasicUrl("work"),
    usermenu: new BasicUrl("usermenu", {
        init: getUrl("usermenu/init")
    })
};

function BasicUrl(controller, options) {
  this.add = getUrl(controller + "/add");
  this.edit = getUrl(controller + "/edit");
  this.delete = getUrl(controller + "/delete");
  this.query = getUrl(controller + "/query");
  this.operator = getUrl(controller + "/operator");
  this.category = getUrl(controller + "/catagory");
  if (options) {
    for (const key in options) {
      this[key] = options[key];
    }
  }
}
/**
 * 拼接请求地址  "https://localhost:44328/admin/api/v1/" +url
 * @param {any} url 地址
 * @returns
 */
export function getUrl(url) {
  return baseUrl + url + "";
}
