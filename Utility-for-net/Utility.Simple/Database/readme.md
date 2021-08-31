access or sqlite not support proc

mysql
DROP PROCEDURE  if EXISTS aa;
create PROCEDURE  aa(a int)
 BEGIN
end;
call aa(1)

oracle
 ---创建添加存储过程，切记数据不能带长度
 --带参数(in模式参数)
 create or replace procedure proc_insert_aaa
 (
 descs in nvarchar2,
 ename in varchar2,
 eaccount in nvarchar2
 )
 is 
 begin
 insert into "aaa"
 (names,accounts,pwds,descs,createdate) values
 (ename,eaccount,'笑了',descs,to_date('2015-11-11','yyyy-mm-dd'));
 --commit;
 end proc_insert_aaa;
 proc_sel_aaa('xx',acc,pwd);

 /*=====函数====*/
create or replace function f_cc(ac number)
return number is/*创建一个函数，传入参数*/
n_pay number;/*保存内部变量*/
begin
select avg(salary) into n_pay from ccc where ids=ac;
return (round(n_pay,2));/*返回*/
exception
when  no_data_found
then 
dbms_output.put_line('编号不存在');
return(0);
end;
select ids,f_cc(11) from ccc;

--2、创建自动增长序列
     -- drop sequence aaa_tb_seq;
      create sequence aaa_tb_seq minvalue 1 maxvalue 99999999
               increment by 1
               start with 1;   /*步长为1*/
--3、创建触发器
      create or replace trigger aaa_tb_tri
          before insert on "aaa"     /*触发条件：当向表dectuser执行插入操作时触发此触发器*/
          for each row                       /*对每一行都检测是否触发*/
          begin                                  /*触发器开始*/
                 select aaa_tb_seq.nextval into :new.ids from dual;   /*触发器主题内容，即触发后执行的动作，在此是取得序列dectuser_tb_seq的下一个值插入到表dectuser中的userid字段中*/
          end;
    
          /                                        /*退出sqlplus行编辑*/

set serveroutput on
declare varname char(1000);
vpwd nvarchar2(2000);
/*声明游标*/
cursor cur_c is select names,pwds from ccc;
begin
/*打开游标*/
open cur_c;
/*读取游标*/
fetch cur_c into varname,vpwd;
while cur_c%found loop
dbms_output.put_line('姓名：'||varname||'，密码：'||vpwd);
fetch cur_c into varname,vpwd;
end loop;
close cur_c;/*关闭游标*/
end;