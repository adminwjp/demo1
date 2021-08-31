package com.utility.util;

import org.elasticsearch.action.admin.indices.delete.DeleteIndexRequest;
import org.elasticsearch.action.admin.indices.delete.DeleteIndexRequestBuilder;
import org.elasticsearch.action.admin.indices.exists.indices.IndicesExistsResponse;
import org.elasticsearch.action.admin.indices.exists.types.TypesExistsResponse;
import org.elasticsearch.action.admin.indices.mapping.put.PutMappingRequest;
import org.elasticsearch.action.bulk.*;
import org.elasticsearch.action.delete.DeleteRequest;
import org.elasticsearch.action.delete.DeleteResponse;
import org.elasticsearch.action.get.GetResponse;
import org.elasticsearch.action.get.MultiGetItemResponse;
import org.elasticsearch.action.get.MultiGetRequest;
import org.elasticsearch.action.get.MultiGetRequestBuilder;
import org.elasticsearch.action.index.IndexRequest;
import org.elasticsearch.action.index.IndexResponse;
import org.elasticsearch.action.support.master.AcknowledgedResponse;
import org.elasticsearch.action.update.UpdateRequest;
import org.elasticsearch.client.Requests;
import org.elasticsearch.client.transport.TransportClient;
import org.elasticsearch.common.Strings;
import org.elasticsearch.common.settings.Settings;
import org.elasticsearch.common.transport.TransportAddress;
import org.elasticsearch.common.unit.ByteSizeUnit;
import org.elasticsearch.common.unit.ByteSizeValue;
import org.elasticsearch.common.unit.TimeValue;
import org.elasticsearch.common.xcontent.ToXContent;
import org.elasticsearch.common.xcontent.XContentBuilder;
import org.elasticsearch.common.xcontent.XContentFactory;
import org.elasticsearch.common.xcontent.XContentType;
import org.elasticsearch.script.Script;
import org.elasticsearch.transport.client.PreBuiltTransportClient;
import java.io.IOException;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ExecutionException;

/**
 * https://www.elastic.co/guide/en/elasticsearch/client/java-api/current/java-docs-bulk-processor.html
 * 在 >7.0.0中弃用。 TransportClient不推荐使用，而推荐使用Java High Level REST Client 目前 使用 不了 不知道 啥回事
 * @deprecated
 * **/
public class ElasticsearchManager {
    //private Logger logger
    public final static String HOST = "127.0.0.1";
    public final static int PORT = 9200;
    public TransportClient client = null;

    public ElasticsearchManager(){
        this(HOST,PORT);
    }

    public ElasticsearchManager(final  String host,final int port){
      this(host,port,null);
    }

    public ElasticsearchManager(final  String host,final int port,final String cluster_name){
        try {
            Settings settings;
            if(!StringUtil.isBlank(cluster_name)){
                settings = Settings.builder()
                        .put("cluster.name", cluster_name)
                        .put("client.transport.sniff", true).put("node.name","master").build();
            }
            else{
                settings=Settings.EMPTY;
            }
            client = new PreBuiltTransportClient(settings)
                    .addTransportAddress(
                            new TransportAddress( InetAddress.getByName(host), port)//elasticsearch-rest-high-level-client jar 包实现
                            //new InetSocketTransportAddress( InetAddress.getByName(host), port)
                    );
            /**
             * elasticsearch 包实现
             * */
           /* var builder= TransportClient.builder();
            if(cluster_name!=null){
                Settings settings = Settings.settingsBuilder()
                        .put("cluster.name", cluster_name).build();
                builder.settings(settings);
            }
            else{
                //builder.settings(Settings.EMPTY);
            }
            client = builder.build()
                    .addTransportAddress(
                            new InetSocketTransportAddress(
                                    InetAddress.getByName(host), port)
                    );*/
        }
        catch (UnknownHostException ex){
            ex.printStackTrace();
        }
        catch (Exception ex){
            ex.printStackTrace();
        }
    }

    public void closeConnectES(){
        client.close();
    }

    /**
     * 创建索引index--类似于数据库
     * */
    public boolean createIndex(String index){
        boolean res=false;
        try {
            res= client.admin().indices().prepareCreate(index).execute().get().isAcknowledged();
        }catch(Exception ex){
            ex.printStackTrace();
        }
      /*  CreateIndexRequestBuilder cirb =  client.admin().indices().prepareCreate(index);
        CreateIndexResponse response = cirb.execute().actionGet();
        res=response.isAcknowledged();*/
        if(res){
            System.out.println("Index created.");
        }else{
            System.err.println("Index creation failed.");
        }
        return  res;
    }

    public boolean deleteIndex(String... indexs){
        DeleteIndexRequestBuilder cirb =  client.admin().indices().prepareDelete(indexs);
        //DeleteIndexResponse response = cirb.execute().actionGet();
        AcknowledgedResponse response = cirb.execute().actionGet();//elasticsearch-rest-high-level-client jar 包实现
        boolean res=response.isAcknowledged();
        if(res){
            System.out.println("Index delete.");
        }else{
            System.err.println("Index delete failed.");
        }
        return res;
    }

    public  boolean  existsIndex(String... indexs){
        IndicesExistsResponse response1 = this.client.admin().indices()
                .exists(Requests.indicesExistsRequest(indexs)).actionGet();
        boolean exist1 = response1.isExists();
        return exist1;
      /*  // 2.1 方式二
        IndicesAdminClient indicesAdminClient = this.client.admin().indices();
        IndicesExistsResponse response2 = indicesAdminClient.prepareExists(indexs).get();
        boolean exist2 = response2.isExists();
        // 2.3 方式三
        IndicesExistsRequest indicesExistsRequest = new IndicesExistsRequest(indexs);
        IndicesExistsResponse response3 = this.client.admin().indices()
                .exists(indicesExistsRequest).actionGet();
        boolean exist3 = response3.isExists();*/
    }

    public boolean exists(String[] indexs, String... types){
        // 判断文档类型是否存在
        TypesExistsResponse typesExists = this.client
                .admin().indices()
                .prepareTypesExists(indexs)
                .setTypes(types)
                .get();
        boolean exist = typesExists.isExists();
        return exist;
    }

    public void create(String index, String type){
       try {
           XContentBuilder mapping = XContentFactory.jsonBuilder()
                   .startObject()
                   .startObject("properties")
                   .startObject("title") // 文档字段title
                   .field("type","text")
                   .field("analyzer", "ik_max_word")
                   .field("search_analyzer", "ik_max_word")
                   .endObject()
                   .startObject("publishDate") // 文档字段publishDate
                   .field("type", "date")
                   .field("format", "yyyy-MM-dd HH:mm:ss")
                   .endObject()
                   .startObject("content") // 文档字段content
                   .field("type","text")
                   .field("analyzer", "ik_max_word")
                   .field("search_analyzer", "ik_smart")
                   .endObject()
                   .startObject("director") // 文档字段director
                   .field("type", "keyword")
                   .endObject()
                   .startObject("price") // 文档字段price
                   .field("type", "float")
                   .endObject()
                   .endObject()
                   .endObject();
           PutMappingRequest putMappingRequest = Requests.putMappingRequest(index).type(type).source(mapping);
           AcknowledgedResponse  response = client.admin().indices().putMapping(putMappingRequest).actionGet();
           System.out.println(response.isAcknowledged());
       }
       catch (IOException ex){
           ex.printStackTrace();
       }

        //error
       /* BulkRequestBuilder bulkRequest = client.prepareBulk();
        add(bulkRequest,index,type);
        create(bulkRequest);*/
    }

    private void  create(BulkRequestBuilder bulkRequest){
        BulkResponse bulkResponse = bulkRequest.execute().actionGet();
        if (bulkResponse.hasFailures()) {
            System.out.println("批量创建索引错误！");
        }
    }

    private  void  add(BulkRequestBuilder bulkRequest,String index,String type){
        IndexRequest request = client.prepareIndex(index, type).request();
        bulkRequest.add(request);
    }

    public  boolean bulkAdd(String index,String type,String... jsons){
        BulkRequestBuilder bulkRequest = client.prepareBulk();

        // either use client#prepare, or use Requests# to directly build index/delete requests
        for (String it:jsons) {
            bulkRequest.add(client.prepareIndex(index, type).setSource(it,XContentType.JSON));
        }
        BulkResponse bulkResponse = bulkRequest.get();
        if (bulkResponse.hasFailures()) {
            // process failures by iterating through each bulk response item
            System.out.println("buld add fail");
            return  false;
        }else{
            System.out.println("buld add success");
            return  true;
        }
    }

    public  void  bulkOperator(IndexRequest[] indexRequests, DeleteRequest[] deleteRequests)   {
        BulkProcessor bulkProcessor = BulkProcessor.builder(
                client,
                new BulkProcessor.Listener() {
                    @Override
                    public void beforeBulk(long executionId,
                                           BulkRequest request) {  }

                    @Override
                    public void afterBulk(long executionId,
                                          BulkRequest request,
                                          BulkResponse response) {  }

                    @Override
                    public void afterBulk(long executionId,
                                          BulkRequest request,
                                          Throwable failure) {  }
                })
                .setBulkActions(10000)
                .setBulkSize(new ByteSizeValue(5, ByteSizeUnit.MB))
                .setFlushInterval(TimeValue.timeValueSeconds(5))
                .setConcurrentRequests(1)
                .setBackoffPolicy( BackoffPolicy.exponentialBackoff(TimeValue.timeValueMillis(100), 3))
                .build();
        // Add your requests
        if(indexRequests!=null){
            for (IndexRequest it:indexRequests) {
                bulkProcessor.add(it);
            }
        }
        if(deleteRequests!=null){
            for (DeleteRequest it:deleteRequests) {
                bulkProcessor.add(it);
            }
        }
        // Flush any remaining requests
        bulkProcessor.flush();
       /* try {
            bulkProcessor.awaitClose(10, TimeUnit.MINUTES);//方法1
        }
        catch (InterruptedException ex){
            ex.printStackTrace();
        }*/
        // Or close the bulkProcessor if you don't need it anymore
        bulkProcessor.close();//方法2
        client.admin().indices().prepareRefresh().get();// Refresh your indices
        client.prepareSearch().get();// Now you can start searching!
    }

    /**批量创建索引**/
    public  void  create(String indexs[],String[] types) {
        BulkRequestBuilder bulkRequest = client.prepareBulk();
        for (int i = 0; i < indexs.length && i < types.length; i++) {
            add(bulkRequest,indexs[i], types[i]);
        }
        create(bulkRequest);
    }

    /**
     * 添加文档
     * */
    public boolean insert(String index,String type,String id,XContentBuilder obj)   {
        IndexResponse response = client.prepareIndex(index, type, id).setSource(obj).get();
        return   response.status().getStatus()>0;
        //return response.isCreated();//elasticsearch 包实现
    }

    public boolean insert(String index,String type,String id,Map<String,Object> obj)   {
        IndexResponse response = client.prepareIndex(index, type, id).setSource(obj).get();
        return   response.status().getStatus()>0;
        //return response.isCreated();//elasticsearch 包实现
    }

    public String toJson(ToXContent content){
        return Strings.toString(content);
    }

    public boolean insert(String index,String type,String id,String json)   {
        IndexResponse response = client.prepareIndex(index, type, id).setSource(json, XContentType.JSON).get();
        return   response.status().getStatus()>0;
        //return response.isCreated();//elasticsearch 包实现
    }

    /**
     * 修改文档
     * */
    public void update(String index,String type,String id,XContentBuilder contentBuilder) {
       try {
           UpdateRequest updateRequest = new UpdateRequest(index, type, id).doc(contentBuilder);
           boolean created= client.update(updateRequest).get().status().getStatus()>0;
           //elasticsearch 包实现
           /*var created= client.update(updateRequest).get().isCreated();//方法 1*/
           System.out.println(created);
       }catch (InterruptedException ex){
           ex.printStackTrace();
       }catch (ExecutionException ex){
           ex.printStackTrace();
       }
        //elasticsearch 包实现
        //var created= client.prepareUpdate(index, type, id).setDoc(contentBuilder).get().isCreated();//方法 2
        //System.out.println(created);
    }
    /**
     * @param script  ctx._source.gender = "male"
     * */
    public  boolean  updateScript(String index,String type,String id,String script) throws ExecutionException, InterruptedException {
        boolean created=  client.prepareUpdate("ttl", "doc", "1")
                .setScript(new Script(script)).get().status().getStatus()>0;
        //elasticsearch 包实现
       /* try {
            UpdateRequest updateRequest = new UpdateRequest(index, type, id).script(new Script(script));
            var created= client.update(updateRequest).get().isCreated();//方法 1
            return  created;
        }
        catch (InterruptedException ex){
            ex.printStackTrace();
        }catch (ExecutionException ex){
            ex.printStackTrace();
        }*/

      //elasticsearch 包实现
      /*  var created=  client.prepareUpdate("ttl", "doc", "1")
                .setScript(new Script(script,ScriptService.ScriptType.INLINE, null, null)).get().isCreated();//方法2*/
        return created;
    }

    /**
     * 存在修改 不存在 添加
     * */
    public void updateIfNoExist(String index,String type,String id,XContentBuilder insert,XContentBuilder update) {
        try {
            IndexRequest indexRequest = new IndexRequest(index, type, id).source(insert);
            UpdateRequest updateRequest = new UpdateRequest(index, type, id).doc(update).upsert(indexRequest);	//upsert:更新插入;
            boolean created= client.update(updateRequest).get().status().getStatus()>0;
            //var created= client.update(updateRequest).get().isCreated(); //elasticsearch 包实现
            System.out.println(created);
        }catch (InterruptedException ex){
            ex.printStackTrace();
        }catch (ExecutionException ex){
            ex.printStackTrace();
        }
    }

    /**
     * 删除文档
     * */
    public boolean delete(String index,String type,String id){
        DeleteResponse deleteReponse = client.prepareDelete(index, type, id).get();
        boolean isFound = deleteReponse.status().getStatus()>0;
       /* boolean isFound = deleteReponse.isFound();//elasticsearch 包实现*/
        return  isFound;
    }

    /**
     * 删除索引
     * */
    public void deleteIndex(String index){
        DeleteIndexRequest delete = new DeleteIndexRequest(index);
        client.admin().indices().delete(delete);
    }

    public Map<String,Object> query(String index,String type,String id)  {
        GetResponse response = client.prepareGet(index, type, id).get();
        Map<String,Object> result= response.getSource();
        return  result;
    }

    public List<Map<String,Object>> multiGet(MultiGetRequest.Item... items){
        MultiGetRequestBuilder multiGetRequestBuilder=client.prepareMultiGet();
        for (MultiGetRequest.Item it:items) {
            multiGetRequestBuilder.add(it);
        }
        List<Map<String,Object>> datas=new ArrayList<>();
        for (MultiGetItemResponse it:multiGetRequestBuilder.get()) {
            GetResponse response = it.getResponse();
            if (response.isExists()) {
                //String json = response.getSourceAsString();
                datas.add( response.getSource());
            }
        }
        return  datas;
    }
}
