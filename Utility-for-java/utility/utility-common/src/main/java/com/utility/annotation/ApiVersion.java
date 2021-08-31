package com.utility.annotation;

//import org.springframework.web.bind.annotation.Mapping;
import java.lang.annotation.*;

@Target({ ElementType.METHOD, ElementType.TYPE })
@Retention(RetentionPolicy.RUNTIME)
@Documented
//@Mapping
public @interface ApiVersion {
    double value() default 1.0;
}
