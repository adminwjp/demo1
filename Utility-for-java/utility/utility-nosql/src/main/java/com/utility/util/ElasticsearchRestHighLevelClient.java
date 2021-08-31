package com.utility.util;

import org.apache.http.Header;
import org.apache.http.HttpHost;
import org.apache.http.auth.AuthScope;
import org.apache.http.auth.UsernamePasswordCredentials;
import org.apache.http.client.CredentialsProvider;
import org.apache.http.client.config.RequestConfig;
import org.apache.http.impl.client.BasicCredentialsProvider;
import org.apache.http.impl.nio.client.HttpAsyncClientBuilder;
import org.apache.http.impl.nio.reactor.IOReactorConfig;
import org.apache.http.message.BasicHeader;
import org.apache.http.ssl.SSLContextBuilder;
import org.apache.http.ssl.SSLContexts;
import org.elasticsearch.action.admin.indices.delete.DeleteIndexRequest;
import org.elasticsearch.action.delete.DeleteRequest;
import org.elasticsearch.action.delete.DeleteResponse;
import org.elasticsearch.action.get.GetRequest;
import org.elasticsearch.action.get.GetResponse;
import org.elasticsearch.action.index.IndexRequest;
import org.elasticsearch.action.support.master.AcknowledgedResponse;
import org.elasticsearch.action.update.UpdateRequest;
import org.elasticsearch.client.RequestOptions;
import org.elasticsearch.client.RestClient;
import org.elasticsearch.client.RestClientBuilder;
import org.elasticsearch.client.RestHighLevelClient;
import org.elasticsearch.client.core.CountRequest;
import org.elasticsearch.client.core.CountResponse;
import org.elasticsearch.client.indices.CreateIndexRequest;
import org.elasticsearch.client.indices.CreateIndexResponse;
import org.elasticsearch.client.indices.GetIndexRequest;
import org.elasticsearch.common.xcontent.XContentBuilder;
import org.elasticsearch.common.xcontent.XContentType;
import org.elasticsearch.index.query.BoolQueryBuilder;
import org.elasticsearch.index.query.QueryBuilders;
import org.elasticsearch.index.query.SpanTermQueryBuilder;
import org.elasticsearch.rest.RestStatus;
import org.elasticsearch.search.builder.SearchSourceBuilder;
import javax.net.ssl.SSLContext;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.security.KeyManagementException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.CertificateException;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/**
 * https://www.elastic.co/guide/en/elasticsearch/client/java-rest/7.6/java-rest-high-document-index.html
 * */
public class ElasticsearchRestHighLevelClient {
    // public  static  final  ElasticsearchRestHighLevelClient EMPTY=new ElasticsearchRestHighLevelClient();
    public final static String HOST = "127.0.0.1";
    public final static int PORT = 9200;
    RestHighLevelClient client;

    /****  constractor start    **/

    public  ElasticsearchRestHighLevelClient(){
        this(ElasticsearchRestHighLevelClient.HOST,ElasticsearchRestHighLevelClient.PORT);
    }

    public  ElasticsearchRestHighLevelClient(String hostname,int port){
        this(hostname,port,null,null,null,null,null);
    }

    public  ElasticsearchRestHighLevelClient(String hostname,int port,String userName,String password,Map<String,String> header
    ,String path,String type)   {
         CredentialsProvider credentialsProvider =null;
         if(!StringUtil.isBlank(userName) &&!StringUtil.isBlank(password)){
             credentialsProvider=new BasicCredentialsProvider();
             credentialsProvider.setCredentials(AuthScope.ANY, new UsernamePasswordCredentials(userName, password));
         }
        final   CredentialsProvider finalCredentialsProvider = credentialsProvider;
         Header[] defaultHeaders=null;
         if(header!=null&&header.size()>0){
             defaultHeaders=  new Header[header.size()];
             int i=0;
             Iterator<String> iterator=header.keySet().iterator();
             while ((iterator.hasNext())){
                 String it=iterator.next();
                 defaultHeaders[i]=new BasicHeader(it,header.get(it));
             }
         }
        SSLContext sslContext=null;
         if(!StringUtil.isBlank(path)&&!StringUtil.isBlank(type))
         {
             Path trustStorePath = Paths.get(path);
             try (InputStream is = Files.newInputStream(trustStorePath)) {
                 KeyStore truststore = KeyStore.getInstance(type);
                 String keyStorePass = null;
                 truststore.load(is, keyStorePass.toCharArray());
                 SSLContextBuilder  sslBuilder = SSLContexts.custom().loadTrustMaterial(truststore, null);
                 sslContext = sslBuilder.build();
             }
             catch (KeyStoreException ex){
                 ex.printStackTrace();
             }catch (NoSuchAlgorithmException ex){
                 ex.printStackTrace();
             }
             catch (CertificateException e) {
                 e.printStackTrace();
             } catch (IOException e) {
                 e.printStackTrace();
             } catch (KeyManagementException e) {
                 e.printStackTrace();
             }
         }
        SSLContext finalSslContext = sslContext;
        HttpHost httpHost=finalSslContext!=null?new HttpHost(hostname, port,"https"):new HttpHost(hostname, port);
        RestClientBuilder builder= RestClient.builder(httpHost)
                .setRequestConfigCallback(
                        new RestClientBuilder.RequestConfigCallback() {
                            @Override
                            public RequestConfig.Builder customizeRequestConfig(
                                    RequestConfig.Builder requestConfigBuilder) {
                                return requestConfigBuilder
                                        .setConnectTimeout(5000)
                                        .setSocketTimeout(60000);
                            }
                        })
                .setHttpClientConfigCallback(new RestClientBuilder.HttpClientConfigCallback() {
                                                 @Override
                                                 public HttpAsyncClientBuilder customizeHttpClient(
                                                         HttpAsyncClientBuilder httpClientBuilder) {
                                                     HttpAsyncClientBuilder httpAsyncClientBuilder= httpClientBuilder.setDefaultIOReactorConfig(
                                                             IOReactorConfig.custom()
                                                                     .setIoThreadCount(1)
                                                                     .build());
                                                     if(finalCredentialsProvider !=null)
                                                     {
                                                         httpAsyncClientBuilder.setDefaultCredentialsProvider(finalCredentialsProvider);
                                                     }
                                                     if(finalSslContext!=null)
                                                     {
                                                         httpClientBuilder.setSSLContext(finalSslContext);
                                                     }
                                                     return  httpAsyncClientBuilder ;
                                                 }
                                             }
                );
         if(defaultHeaders!=null){
             builder.setDefaultHeaders(defaultHeaders);
         }
        client = new RestHighLevelClient(builder);
    }

    /****  constractor end    **/

    /****  index  start   **/

    public boolean createIndexIfNotExists(String index){
        if(existsIndex(index)){
            return  true;
        }else{
            return  createIndex(index);
        }
    }

    public boolean createIndexIfExistsDrop(String index){
        boolean res=existsIndex(index)&&deleteIndex(index);
        return  createIndex(index);
    }

    public boolean createIndex(String index){
        try {
            CreateIndexRequest createIndexRequest=new CreateIndexRequest(index);
            //Limit of total fields [1000] in index [index] has been exceeded
            Map map=new HashMap<String,Object>();
            map.put("index.mapping.total_fields.limit",999999999);
            createIndexRequest.settings(map);
            CreateIndexResponse createIndexResponse =client.indices().create(createIndexRequest, RequestOptions.DEFAULT);
            return createIndexResponse.isAcknowledged();
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
    }

    public boolean deleteIndexIfExists(String index){
        if(existsIndex(index)){
            return  deleteIndex(index);
        }
        return  true;
    }

    /**
     * 不存在删除异常
     * */
    public boolean deleteIndex(String index){
        try {
            DeleteIndexRequest deleteIndexRequest=new DeleteIndexRequest(index);
            AcknowledgedResponse acknowledgedResponse =client.indices().delete(deleteIndexRequest, RequestOptions.DEFAULT);
            return acknowledgedResponse.isAcknowledged();
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
    }

    public boolean existsIndex(String index){
        try {
            GetIndexRequest getIndexRequest=new GetIndexRequest(index);
            boolean exists =client.indices().exists(getIndexRequest, RequestOptions.DEFAULT);
            return exists;
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
    }

    /****  index end    **/

    /****  insert  start   **/

    public  boolean  insert(IndexRequest indexRequest)  {
        try {
            RestStatus status=client.index(indexRequest, RequestOptions.DEFAULT).status();
            //System.out.println(status);
            return status== RestStatus.CREATED||status== RestStatus.OK;
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
        finally {

        }
    }

    public  boolean  insert(String index, Map<String,Object> data)  {
        IndexRequest indexRequest=new IndexRequest(index).source(data);
        return  insert(indexRequest);
    }

    /**
     * @param  data "user", "kimchy", "postDate", new Date()
     * */

    public  boolean  insert(String index, Object... data)  {
        IndexRequest indexRequest=new IndexRequest(index).source(data);
        return  insert(indexRequest);
    }

    public  boolean  insert(String index, String json)  {
        IndexRequest indexRequest=new IndexRequest(index).source(json, XContentType.JSON);
        return  insert(indexRequest);
    }

    public  boolean  insert(String index, XContentBuilder contentBuilder)  {
        IndexRequest indexRequest=new IndexRequest(index).source(contentBuilder);
        return  insert(indexRequest);
    }

    /****  insert end    **/

    /****  update start    **/

    public  boolean  update(UpdateRequest  updateRequest)  {
        try {
            RestStatus status=client.update(updateRequest, RequestOptions.DEFAULT).status();
            System.out.println(status);
            return status== RestStatus.OK||status==RestStatus.CREATED;
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
    }

    public  boolean  update(String index,String id)  {
        UpdateRequest updateRequest=new UpdateRequest(index,id);
        return  update(updateRequest);
    }

    /****  update  end   **/

    /****  select  start   **/

    public GetResponse get(GetRequest getRequest){
        try {
            GetResponse getResponse=client.get(getRequest, RequestOptions.DEFAULT);
            return getResponse;
        }catch (IOException ex){
            ex.printStackTrace();
            return  null;
        }
    }

    /**
     * 查询单个
     * */
    public <T extends  Class > T get(String index,String id, Class<T> tClass){
        GetRequest getRequest=new GetRequest(index,id);
        GetResponse getResponse=get(getRequest);
        if(getResponse==null){
            return  null;
        }
        else{
            if(getResponse.isExists()){
                String json=getResponse.getSourceAsString();
                T obj= JsonUtil.toObject(json,tClass);
                return  obj;
            }
            return null;
        }
    }

    public Map<String,Object> get(String index,String id){
        GetRequest getRequest=new GetRequest(index,id);
        GetResponse getResponse=get(getRequest);
        if(getResponse==null){
            return  null;
        }
        else{
            if(getResponse.isExists()){
                Map obj=getResponse.getSource();
                return  obj;
            }
            return  null;
        }
    }



    /****  select end    **/

    /****  exists start    **/

    public boolean exists(GetRequest getRequest){
        try {
            boolean exists=client.exists(getRequest, RequestOptions.DEFAULT);
            return exists;
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
    }

    public boolean exists(String index,String id){
        GetRequest getRequest=new GetRequest(index,id);
        return  exists(getRequest);
    }


    /****  exists  end   **/

    /****  delete start    **/

    public boolean delete(DeleteRequest deleteRequest){
        try {
            DeleteResponse  deleteResponse=client.delete(deleteRequest, RequestOptions.DEFAULT);
            //System.out.println(deleteResponse.status());
            //ReplicationResponse.ShardInfo shardInfo = deleteResponse.getShardInfo();
            //System.out.println(shardInfo.getTotal());
            return deleteResponse.status()== RestStatus.OK;
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
    }

    public boolean delete(String index,String id){
        DeleteRequest deleteRequest=new DeleteRequest(index,id);
        return  delete(deleteRequest);
    }

    /****  delete  end   **/

    /****  count start    **/

    public long count(CountRequest request){
        try {
            CountResponse countResponse=client.count(request, RequestOptions.DEFAULT);
            return countResponse.getCount();
        }catch (IOException ex){
            ex.printStackTrace();
            return  -1;
        }
    }
    /**
     * @param  query val is not null exception
     * */
    public long count(String index, HashMap<String,Object> query){
        CountRequest request=new CountRequest(index);
        SearchSourceBuilder sourceBuilder =new SearchSourceBuilder();
        if(query!=null&&query.size()>0){
            BoolQueryBuilder boolBuilder = QueryBuilders.boolQuery();
            Iterator<String> iterator=query.keySet().iterator();
            while (iterator.hasNext()){
                String it=iterator.next();
                boolBuilder.must(new SpanTermQueryBuilder(it,query.get(it)));
            }
            sourceBuilder.query(boolBuilder);
        }
        request.source(sourceBuilder);
        return  count(request);
    }

    /****  count  end   **/

}
