mvn clean
mvn install

cd consul-gateway
mvn clean
mvn install

cd ../eureka-gateway
mvn clean
mvn install

cd ../eureka-server
mvn clean
mvn install

cd ../zookeeper-gateway
mvn clean
mvn install


rem jar not update delete jar