namespace Utility.Database
{
    /// <summary>
    /// MySQL 5.0 以上的版本： 
    ///<para> 1、一个汉字占多少长度与编码有关：  UTF－8：一个汉字＝3个字节  GBK：一个汉字＝2个字节</para>
    ///<para>2、varchar(n) 表示 n 个字符，无论汉字和英文，Mysql 都能存入 n 个字符，仅是实际字节长度有所区别 </para>
    ///<para>3、MySQL 检查长度，可用 SQL 语言来查看：</para>
    /// </summary>
    public enum MySqlDataType
    {
        #region 数值类型
        /// <summary>
        ///类型:TINYINT
        /// <para>大小:1 字节</para>
        /// <para>范围（有符号）:(-128，127)</para>
        /// <para>范围（无符号）:(0，255)</para>
        /// <para>用途:小整数值</para>
        /// </summary>
        TINYINT,
        /// <summary>
        ///类型:SMALLINT
        /// <para>大小:2 字节</para>
        /// <para>范围（有符号）:(-32 768，32 767)</para>
        /// <para>范围（无符号）:(0，65 535)</para>
        /// <para>用途:大整数值</para>
        /// </summary>
        SMALLINT,
        /// <summary>
        ///类型:MEDIUMINT
        /// <para>大小:3 字节</para>
        /// <para>范围（有符号）:(-8 388 608，8 388 607)</para>
        /// <para>范围（无符号）:(0，16 777 215)</para>
        /// <para>用途:大整数值</para>
        /// </summary>
        MEDIUMINT,
        /// <summary>
        ///类型:INT或INTEGER
        /// <para>大小:4 字节</para>
        /// <para>范围（有符号）:(-2 147 483 648，2 147 483 647)</para>
        /// <para>范围（无符号）:(0，4 294 967 295)</para>
        /// <para>用途:大整数值</para>
        /// </summary>
        INT,
        /// <summary>
        ///类型:INT或INTEGER
        /// <para>大小:4 字节</para>
        /// <para>范围（有符号）:(-2 147 483 648，2 147 483 647)</para>
        /// <para>范围（无符号）:(0，4 294 967 295)</para>
        /// <para>用途:大整数值</para>
        /// </summary>
        INTEGER,
        /// <summary>
        ///类型:BIGINT
        /// <para>大小:8 字节</para>
        /// <para>范围（有符号）:(-9,223,372,036,854,775,808，9 223 372 036 854 775 807)</para>
        /// <para>范围（无符号）:(0，18 446 744 073 709 551 615)</para>
        /// <para>用途:极大整数值</para>
        /// </summary>
        BIGINT,
        /// <summary>
        ///类型:FLOAT
        /// <para>大小:4 字节</para>
        /// <para>范围（有符号）:(-3.402 823 466 E+38，-1.175 494 351 E-38)，0，(1.175 494 351 E-38，3.402 823 466 351 E+38)</para>
        /// <para>范围（无符号）:0，(1.175 494 351 E-38，3.402 823 466 E+38)</para>
        /// <para>用途:单精度 浮点数值</para>
        /// </summary>
        FLOAT,
        /// <summary>
        ///类型:DOUBLE
        /// <para>大小:8 字节</para>
        /// <para>范围（有符号）:(-1.797 693 134 862 315 7 E+308，-2.225 073 858 507 201 4 E-308)，0，(2.225 073 858 507 201 4 E-308，1.797 693 134 862 315 7 E+308)</para>
        /// <para>范围（无符号）:0，(2.225 073 858 507 201 4 E-308，1.797 693 134 862 315 7 E+308)</para>
        /// <para>用途:双精度 浮点数值</para>
        /// </summary>
        DOUBLE,
        /// <summary>
        ///类型:DECIMAL
        /// <para>大小:对DECIMAL(M,D) ，如果M>D，为M+2否则为D+2</para>
        /// <para>范围（有符号）:依赖于M和D的值</para>
        /// <para>范围（无符号）:依赖于M和D的值</para>
        /// <para>用途:小数值</para>
        /// </summary>
        DECIMAL,
        #endregion 数值类型

        #region 日期和时间类型
        /// <summary>
        ///类型:DATE
        /// <para>大小 (字节):3</para>
        /// <para>范围:1000-01-01/9999-12-31</para>
        /// <para>格式:YYYY-MM-DD</para>
        /// <para>用途:日期值</para>
        /// </summary>
        DATE,
        /// <summary>
        ///类型:TIME
        /// <para>大小 (字节):3</para>
        /// <para>范围:'-838:59:59'/'838:59:59'</para>
        /// <para>格式:HH:MM:SS</para>
        /// <para>用途:时间值或持续时间</para>
        /// </summary>
        TIME,
        /// <summary>
        ///类型:YEAR
        /// <para>大小 (字节):1</para>
        /// <para>范围:1901/2155</para>
        /// <para>格式:YYYY</para>
        /// <para>用途:年份值</para>
        /// </summary>
        YEAR,
        /// <summary>
        ///类型:DATETIME
        /// <para>大小 (字节):8</para>
        /// <para>范围:1000-01-01 00:00:00/9999-12-31 23:59:59</para>
        /// <para>格式:YYYY-MM-DD HH:MM:SS</para>
        /// <para>用途:混合日期和时间值</para>
        /// </summary>
        DATETIME,
        /// <summary>
        ///类型:TIMESTAMP
        /// <para>大小 (字节):4</para>
        /// <para>范围: 1970-01-01 00:00:00/2038  结束时间是第 2147483647 秒，北京时间 2038-1-19 11:14:07，格林尼治时间 2038年1月19日 凌晨 03:14:07  YYYYMMDD HHMMSS</para>
        /// <para>格式:混合日期和时间值，时间戳</para>
        /// </summary>
        TIMESTAMP,
        #endregion 日期和时间类型

        #region 字符串类型
        /// <summary>
        ///类型:VARCHAR
        /// <para>大小:0-65535 字节</para>
        /// <para>用途 CHAR:变长字符串 TINYBLOB</para>
        /// <para>0-255字节:0-255字节</para>
        /// <para>定长字符串:不超过 255 个字符的二进制字符串</para>
        /// </summary>
        VARCHAR,
        /// <summary>
        ///类型:TINYTEXT
        /// <para>大小:0-255字节</para>
        /// <para>用途 CHAR:短文本字符串 BLOB</para>
        /// <para>0-255字节:0-65 535字节</para>
        /// <para>定长字符串:二进制形式的长文本数据</para>
        /// </summary>
        TINYTEXT,
        /// <summary>
        ///类型:TEXT
        /// <para>大小:0-65 535字节</para>
        /// <para>用途 CHAR:长文本数据 MEDIUMBLOB</para>
        /// <para>0-255字节:0-16 777 215字节</para>
        /// <para>定长字符串:二进制形式的中等长度文本数据</para>
        /// </summary>
        TEXT,
        /// <summary>
        ///类型:MEDIUMTEXT
        /// <para>大小:0-16 777 215字节</para>
        /// <para>用途 CHAR:中等长度文本数据 LONGBLOB</para>
        /// <para>0-255字节:0-4 294 967 295字节</para>
        /// <para>定长字符串:二进制形式的极大文本数据</para>
        /// </summary>
        MEDIUMTEXT,
        /// <summary>
        ///类型:LONGTEXT
        /// <para>大小:0-4 294 967 295字节</para>
        /// <para>用途 CHAR:极大文本数据</para>
        /// </summary>
        LONGTEXT,
        #endregion 字符串类型

    }
}
