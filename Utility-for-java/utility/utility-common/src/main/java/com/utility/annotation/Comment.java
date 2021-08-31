package com.utility.annotation;

import java.lang.annotation.*;

/** 注释 注解 生成 文档 用的 */
@Target({ElementType.TYPE,ElementType.CONSTRUCTOR, ElementType.FIELD, ElementType.METHOD, ElementType.ANNOTATION_TYPE})
@Retention(RetentionPolicy.RUNTIME)
@Documented
public @interface Comment {
  public String desc() default  "";
}
