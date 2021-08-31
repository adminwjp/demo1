// https://eslint.org/docs/user-guide/configuring

module.exports = {
  root: true,
  parserOptions: {
    parser: 'babel-eslint'
  },
  env: {
    browser: true,
  },
  extends: [
    // https://github.com/vuejs/eslint-plugin-vue#priority-a-essential-error-prevention
    // consider switching to `plugin:vue/strongly-recommended` or `plugin:vue/recommended` for stricter rules.
    'plugin:vue/essential',
    // https://github.com/standard/standard/blob/master/docs/RULES-en.md
    'standard'
  ],
  // required to lint *.vue files
  plugins: [
    'vue'
  ],
  // add your custom rules here
  rules: {
    plugin: 'vue/essential',
    'vue/no-duplicate-attributes': [2, {
      allowCoexistClass: true, // default: true
      allowCoexistStyle: true, // default: true
    }],
    "newIsCap": true,
    // 不需要
    "space-before-function-paren": 0, // 函数定义时括号前面要不要有空格
    "eol-last": 0, // 文件以单一的换行符结束
    "no-extra-semi": 0, // 可以多余的冒号
    "semi": 0, // 语句可以不需要分号结尾
    "eqeqeq": 0, // 必须使用全等
    "one-var": 0, // 连续声明
    "no-undef": 0, // 可以 有未定义的变量
    "no-unused-vars": "off",
    // "no-unused-vars": [2, {
    //   "vars": "all",
    //   "args": "after-used"
    // }], //不能有声明后未被使用的变量或参数
    /***
     *"double"(默认) 要求尽可能地使用双引号
       "single"
     要求尽可能地使用单引号
       "backtick"
     要求尽可能地使用反勾号
     */
    "quotes": [1, "double"], //引号类型 `` "" ''
    'arrow-parens': 0, // 表示的是箭头函数用小括号括起来；

    'generator-star-spacing': 0, // 表示生成器函数 * 的前后空格
    // allow async-await
    'generator-star-spacing': 'off',
    // allow debugger during development
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off'
  }
}
