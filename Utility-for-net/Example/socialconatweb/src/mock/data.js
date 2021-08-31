import Mock from "mockjs";
const admins = () => {
  let result = {
    data: [],
    success: true,
    msg: "查询成功!",
    code: 20000
  }
};
for (let index = 0; index < 10; index++) {
  result.data.push(Mock.mock({
    id: "@id"

  }));
}
const menus = () => {
  let result = {
    data: [],
    success: true,
    msg: "查询成功!",
    code: 20000
  }
};