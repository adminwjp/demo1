/**
 * net framework not support mysql
 设计范式的分类
第一范式的目标是确定每一列的原子性
第二范式的要求每个表描述一件事情(即除了主键以外的其他列,都依赖与该主键,则满足第二范式(2NF))
第三范式的目标是不出现传递依赖
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{

    public class TypeInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }//编号

        public string Type { get; set; } //类型

        public int Num { get; set; } //数量

        public string Des { get; set; } //描述
    }

    public class PivotTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }//编号

        public int Year { get; set; }// 年

        public int Quarter { get; set; }// 数量

        public decimal Amount { get; set; } //余额
    }
    
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }//编号

        public string A { get; set; }

        public string B { get; set; }
    }

    /// <summary>
    /// 学员表
    /// </summary>
    public class Student 
    {
        /// <summary>
        /// 学号
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string Name { get; set; }

       
        public string Unit { get; set; }// 所属单位


        /// <summary>
        /// 学员年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 学员性别
        /// </summary>
        public string Sex { get; set; }


    }
    /// <summary>
    /// 课程表
    /// </summary>
    public class Course 
    {
        /// <summary>
        /// 课程编号 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        public Teacher Teacher { get; set; }

        /// <summary>
        /// 教师编号
        /// </summary>
        public long TeacherId { get; set; }
    }

    /// <summary>
    /// 成绩表
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 成绩编号
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public long StudentId { get; set; }

        /// <summary>
        /// 成绩
        /// </summary>
        public decimal Socre { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public long CourseId { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
    /// <summary>
    /// 教师表
    /// </summary>
    public class Teacher 
    {
        /// <summary>
        /// 教师编号
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        /// <summary>
        /// 课程姓名
        /// </summary>
        public string Name { get; set; }
    }
}


