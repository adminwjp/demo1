-- sqlite 不支持 存储 过程
-- 用户 登录 存储 过程
DROP proc
IF
	EXISTS ProcByUserLogin;
CREATE proc ProcByUserLogin ( account varchar ( 20 ), password varchar ( 50 ), result varchar ( 20 ) out ) AS status int BEGIN
	BEGIN
		SELECT
			status = Status 
		FROM
			tn_Users 
		WHERE
			Password =@password 
			AND ( UserName =@account OR AccountMobile =@account OR AccountEmail =@account )
		IF
			status = 1 SELECT
			result = 'IsActivated' ELSE
		IF
			a =- 1 SELECT
			result = 'Delete' ELSE
		IF
			a = 0 SELECT
		result = 'NotActivated' ELSE SELECT
	result = 'UnknownError' END;