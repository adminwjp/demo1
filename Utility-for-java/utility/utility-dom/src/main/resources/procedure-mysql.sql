DROP PROCEDURE  IF EXISTS total; CREATE PROCEDURE  total(seller_id int,out buyerCount int,out orderCount int,out orderPayFee double ,out productCount int)
 begin
   -- 声明
    DECLARE buyerCountTemp INT DEFAULT 0;
    DECLARE orderCountTemp INT DEFAULT 0;
    DECLARE orderPayFeeTemp double DEFAULT 0.0;
    DECLARE productCountTemp INT DEFAULT 0;
     -- 获取查询
    set @buyerCountTemp=select count(*) from user where active=1 and flag=0 and seller_id=seller_id;
    set @orderCountTemp=select count(id) from t_order where seller_id=seller_id;
    set @orderPayFeeTemp=SELECT sum(pay_fee) AS total_income FROM t_order    WHERE date_format(create_date, '%Y-%m') = date_format(now(), '%Y-%m') AND payment_status = 2;
    set @productCountTemp=select count(*) from t_product where active=1 and is_marketable=1 and seller_id=seller_id;
     -- 输出影响函数
    select @buyerCountTemp into buyerCount,@orderCountTemp into orderCount,@orderPayFeeTemp into orderPayFee,@productCountTemp into productCount
end