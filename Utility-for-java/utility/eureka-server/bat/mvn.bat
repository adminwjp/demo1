@echo off
e:
 cd E:\work\java\eureka-server
 mvn clean
 mvn install
rem echo correct && (mvn clean) &&(mvn install) || (echo "异常")
pause