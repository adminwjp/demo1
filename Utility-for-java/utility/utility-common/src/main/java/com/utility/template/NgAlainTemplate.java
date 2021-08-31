package com.utility.template;

import com.utility.util.StringUtil;
import java.util.List;

/**
 * ng-alain list from  模板
 * */
public class NgAlainTemplate {
    /** 通用 ng html 页面 */
    public  static  String getNgHtml(String title){
         return "<page-header [title]=\\\"\\\'"+title+"\\\'\\\"></page-header> <nz-card [nzBordered]=\\\"false\\\"> <button nz-button (click)=\\\"operEvent(1)\\\" [nzType]=\\\"\\\"primary\\\"\\\"> <i nz-icon nzType=\\\"plus\\\"></i> <span>添加</span> </button> <ng-container *ngIf=\\\"selectedRows.length > 0\\\"> <button nz-button nz-dropdown [nzDropdownMenu]=\\\"batchMenu\\\" nzPlacement=\\\"bottomLeft\\\"> 更多操作 <i nz-icon nzType=\\\"down\\\"></i> </button> <nz-dropdown-menu #batchMenu=\\\"nzDropdownMenu\\\"> <ul nz-menu> <li nz-menu-item (click)=\\\"modfiy()\\\">编辑</li> <li nz-menu-item (click)=\\\"remove()\\\">删除</li> </ul> </nz-dropdown-menu> </ng-container> <st #st [widthMode]=\\\"{ type: \\\"strict\\\" }\\\" [columns]=\\\"columns\\\" [page]=\\\"page\\\" [total]=\\\"pageInfo.records\\\" [pi]=\\\"q.pi\\\" [ps]=\\\"q.ps\\\" [data]=\\\"data\\\" [loading]=\\\"loading\\\" (change)=\\\"stChange($event)\\\"> </st> </nz-card> <nz-modal [(nzVisible)]=\\\"dialogVisible\\\" [(nzTitle)]=\\\"title\\\" (nzOnCancel)=\\\"handleCancel()\\\" (nzOnOk)=\\\"handleOk()\\\"> <sf #sf mode=\\\"edit\\\" [schema]=\\\"validateForm\\\" [formData]=\\\"formData\\\" button=\\\"none\\\"> </sf> </nz-modal>";
    }

    public  static  String getNgComponentCheckStr(){
        return   "    { title: '', index: 'id', type: 'checkbox', width: '5%', fixed: 'left' },\n";
    }

   public  static  String getNgComponentOperatorStr(){
        return   "    {\n" +
                "      title: '操作',\n" +
                "      width: '10%',\n" +
                "      buttons: [\n" +
                "        {\n" +
                "          text: '预览',\n" +
                "          click: (item: any) => {\n" +
                "            this.operator(item, OperatorFlag.Query);\n" +
                "          },\n" +
                "        },\n" +
                "        {\n" +
                "          text: '编辑',\n" +
                "          click: (item: any) => {\n" +
                "            this.operator(item, OperatorFlag.Modify);\n" +
                "          },\n" +
                "        },\n" +
                "        {\n" +
                "          text: '删除',\n" +
                "          click: (item: any) => {\n" +
                "            this.operator(item, OperatorFlag.Delelte);\n" +
                "          },\n" +
                "        },\n" +
                "      ],\n" +
                "    },\n" ;
  }

  public static String getNgComponent(Class<?> clas){
        return "";
  }

    /** 通用 ng compoent 组件 */
    public  static  String getNgComponent(boolean test, NgJson ngJson){
        String name= ngJson.className;
        String columnStr="";
        String formColumnStr="";
        if(ngJson.getCheckbox()){
            columnStr+= "    { title: '', index: '"+ngJson.getId()+"', type: 'checkbox', width: '5%', fixed: 'left' },\n" ;
            formColumnStr+="        id: { type: 'number', title: '编号', readOnly: true },\n" ;
        }
        String requiredStr="";
        for (NgColumnJson it:ngJson.getNgColumnJsons()) {
            columnStr+="    { title: '"+it.getName()+"', index: '"+it.getIndex()+"', width: '"+it.getWidth()+"' },\n";
            if(it.getRequired()){
                columnStr+="'"+it.getIndex()+"',";
            }
            formColumnStr+="        "+it.getIndex()+": {\n" +
                    "          type: '"+it.getType()+"',\n" +
                    "          title: '"+it.getName()+"',\n" +
                    "          default: '"+it.getDefaultValue()+"',\n" +

                    (it.getDate()?" format: 'date-time',":"")+


                    (it.getType()=="boolean"?" ui: { checkedChildren: '开', unCheckedChildren: '关' }, ":

                    (it.getRange()? ("          minLength: "+it.getMin()+",\n" +
                            "          maxLength: "+it.getMax()+",\n"):"")+

                    (it.getRange()?"          ui: { errors: { required: '请输入"+it.getName()+",且长度在"+
                            (it.getMin()==it.getMax()?it.getMin():""+it.getMin()+"-"+it.getMax()+"")+"' } },\n"
                            :it.getRequired()?"          ui: { errors: { required: '请输入\"+it.getName()+\"' } },\n":"")

                    )+

                    (it.getReadonly()?" readOnly: true":"")+

                    "        },\n" ;
        }

        columnStr+=getNgComponentOperatorStr();
        String str="import { Component, OnInit, ViewChild, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';\n" +
                "import { NzMessageService, NzModalService } from 'ng-zorro-antd';\n" +
                "import { _HttpClient } from '@delon/theme';\n" +
                "import { STComponent, STColumn } from '@delon/abc';\n" +
                "import { testUrls } from '../../../../shared/utils/index';\n" +
                "import { AbstractList } from '../../../base/abstractList.component';\n" +
                "import { OperatorFlag } from 'src/app/routes/enum/operatorFlag';\n" +
                "import { SFSchema, SFComponent, SFSelectWidgetSchema } from '@delon/form';\n" +
                "\n" +
                "@Component({\n" +
                "  selector: 'app-table-list',\n" +
                "  templateUrl: './"+StringUtil.parse(name, StringUtil.StringFormat.InitialLetterUpperCaseLower)+".component.html',\n" +
                "  changeDetection: ChangeDetectionStrategy.OnPush,\n" +
                "})\n" +
                "export class "+name+(test?"Test":"")+"Component extends AbstractList implements OnInit {\n" +
                "  // 重写  即 覆盖\n" +
                "  protected q: any = {\n" +
                "    pi: 1, // 分页\n" +
                "    ps: 10, // 每页 记录\n" +
                "    sorter: '', // 排序 条件\n" +
                "  };\n" +
                "  validateForm: SFSchema;\n" +
                "  columns: STColumn[] = [\n" +
                columnStr
                +
                "  ];\n" +
                "  @ViewChild('sf', { static: true })\n" +
                "  sf: SFComponent;\n" +
                "  @ViewChild('st', { static: true })\n" +
                "  st: STComponent;\n" +
                "  constructor(http: _HttpClient, msg: NzMessageService, modalSrv: NzModalService, cdr: ChangeDetectorRef) {\n" +
                "    super(http, msg, modalSrv, cdr);\n" +
                "  }\n" +
                "  ngOnInit() {\n" +
                "    this.createForm();\n" +
                "    this.url.query = testUrls.seller.get_list;\n" +
                "    this.url.save = testUrls.seller.save;\n" +
                "    this.url.delete = testUrls.seller.delete;\n" +
                "    this.form = this.sf;\n" +
                "    this.getData();\n" +
                "  }\n" +
                "\n" +
                "  createForm() {\n" +
                "    //  不同 操作 需要 重新创建\n" +
                "    const temp: SFSchema = {\n" +
                "      properties: {\n" +
                             formColumnStr +
                "      },\n" +
                "      required: ["+requiredStr+"],\n" +
                "    };\n" +
                "    if (this.flag === OperatorFlag.Insert) {\n" +
                "      // tslint:disable-next-line: forin\n" +
                "      for (const key in temp.properties) {\n" +
                "        const element = temp.properties[key];\n" +
                "        if (element.readOnly) {\n" +
                "          delete temp.properties[key];\n" +
                "        }\n" +
                "      }\n" +
                "    } else if (this.flag !== OperatorFlag.Modify) {\n" +
                "      // tslint:disable-next-line: forin\n" +
                "      for (const key in temp.properties) {\n" +
                "        const element = temp.properties[key];\n" +
                "        if (!element.readOnly) {\n" +
                "          element.readOnly = true;\n" +
                "        }\n" +
                "      }\n" +
                "    }\n" +
                "    this.validateForm = temp;\n" +
                "  }\n" +
                "\n" +
                "  resetForm(): void {\n" +
                "    this.validateForm.reset();\n" +
                "  }\n" +
                "  /** 重置 查询 表单 */\n" +
                "  reset() {\n" +
                "    // wait form reset updated finished\n" +
                "    setTimeout(() => this.getData());\n" +
                "  }\n" +
                "  protected cleanCheck() {\n" +
                "    this.st.clearCheck();\n" +
                "  }\n" +
                "}\n";
        return str;
    }

    public static  class NgJson{
        private   String className;//类名
        private  String title;//标题
        private  String id;//主键
        private  boolean checkbox;//是否有复选框
        private List<NgColumnJson> ngColumnJsons;

        public String getClassName() {
            return className;
        }

        public void setClassName(String className) {
            this.className = className;
        }

        public String getTitle() {
            return title;
        }

        public void setTitle(String title) {
            this.title = title;
        }

        public String getId() {
            return id;
        }

        public void setId(String id) {
            this.id = id;
        }

        public boolean getCheckbox() {
            return checkbox;
        }

        public void setCheckbox(boolean checkbox) {
            this.checkbox = checkbox;
        }

        public List<NgColumnJson> getNgColumnJsons() {
            return ngColumnJsons;
        }

        public void setNgColumnJsons(List<NgColumnJson> ngColumnJsons) {
            this.ngColumnJsons = ngColumnJsons;
        }

    }
    public  static  class  NgColumnJson{
        private   String name;//中文名称
        private  String msg;//非空 错误 信息
        private boolean required;//非空？
        private int min;//最小
        private  int max;//最大 值
        private  boolean range;//是否 最小 最大 值
        private  String  index;//属性  名称
        private  String width="5%";//宽度
        private   String defaultValue="test";//默认值
        private String type="string";//默认 string 类型
        private  boolean select;//是否 时 选择 框
        private  boolean date;
        private  boolean readonly;
        public String getName() {
            return name;
        }

        public void setName(String name) {
            this.name = name;
        }

        public String getMsg() {
            return msg;
        }

        public void setMsg(String msg) {
            this.msg = msg;
        }

        public boolean getRequired() {
            return required;
        }

        public void setRequired(boolean required) {
            this.required = required;
        }

        public int getMin() {
            return min;
        }

        public void setMin(int min) {
            this.min = min;
        }

        public int getMax() {
            return max;
        }

        public void setMax(int max) {
            this.max = max;
        }

        public boolean getRange() {
            return range;
        }

        public void setRange(boolean range) {
            this.range = range;
        }

        public String getWidth() {
            return width;
        }

        public void setWidth(String width) {
            this.width = width;
        }

        public String getIndex() {
            return index;
        }

        public void setIndex(String index) {
            this.index = index;
        }

        public String getDefaultValue() {
            return defaultValue;
        }

        public void setDefaultValue(String defaultValue) {
            this.defaultValue = defaultValue;
        }

        public String getType() {
            return type;
        }

        public void setType(String type) {
            this.type = type;
        }

        public boolean getSelect() {
            return select;
        }

        public void setSelect(boolean select) {
            this.select = select;
        }

        public boolean getDate() {
            return date;
        }

        public void setDate(boolean date) {
            this.date = date;
        }

        public boolean getReadonly() {
            return readonly;
        }

        public void setReadonly(boolean readonly) {
            this.readonly = readonly;
        }

    }
}
