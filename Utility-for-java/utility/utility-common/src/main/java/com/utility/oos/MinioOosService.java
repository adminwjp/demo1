package com.utility.oos;

import io.minio.*;
import io.minio.messages.Bucket;
import io.minio.messages.DeleteError;
import io.minio.messages.DeleteObject;
import io.minio.messages.Item;
import lombok.SneakyThrows;
//import org.springframework.beans.factory.annotation.Autowired;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

public class MinioOosService {
    //@Autowired
    private MinioClient minioClient;

    public MinioOosService(MinioClient minioClient) {
        this.minioClient = minioClient;
    }



        private static final int DEFAULT_EXPIRY_TIME = 7 * 24 * 3600;

        /**
         * 检查存储桶是否存在
         *
         * @param bucketName 存储桶名称
         * @return
         */
        @SneakyThrows
        public boolean bucketExists(String bucketName) {
            boolean flag = false;
            BucketExistsArgs.Builder args=BucketExistsArgs.builder().bucket(bucketName);
           try {
               flag = minioClient.bucketExists(args.build());
               if (flag) {
                   return true;
               }
           }
           catch (Exception ex){

           }
            return false;
        }

        /**
         * 创建存储桶
         *
         * @param bucketName 存储桶名称
         */
        @SneakyThrows
        public boolean makeBucket(String bucketName) {
            boolean flag = bucketExists(bucketName);
            if (!flag) {
               try {
                   MakeBucketArgs args= MakeBucketArgs.builder()
                           .bucket(bucketName).build();
                   minioClient.makeBucket(args);
                   return true;
               }
               catch (Exception ex){
                   return  false;
                }
            } else {
                return false;
            }
        }

        /**
         * 列出所有存储桶名称
         *
         * @return
         */
        @SneakyThrows
        public List<String> listBucketNames() {
            List<Bucket> bucketList = listBuckets();
            List<String> bucketListName = new ArrayList<>();
            for (Bucket bucket : bucketList) {
                bucketListName.add(bucket.name());
            }
            return bucketListName;
        }

        /**
         * 列出所有存储桶
         *
         * @return
         */
        @SneakyThrows
        public List<Bucket> listBuckets() {
            return minioClient.listBuckets();
        }

        /**
         * 删除存储桶
         *
         * @param bucketName 存储桶名称
         * @return
         */
        @SneakyThrows
        public boolean removeBucket(String bucketName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                Iterable<Result<Item>> myObjects = listObjects(bucketName);
                for (Result<Item> result : myObjects) {
                    Item item = result.get();
                    // 有对象文件，则删除失败
                    if (item.size() > 0) {
                        return false;
                    }
                }
                // 删除存储桶，注意，只有存储桶为空时才能删除成功。
                RemoveBucketArgs args=RemoveBucketArgs.builder()
                        .bucket(bucketName).build();
                minioClient.removeBucket(args);
                flag = bucketExists(bucketName);
                if (!flag) {
                    return true;
                }

            }
            return false;
        }

        /**
         * 列出存储桶中的所有对象名称
         *
         * @param bucketName 存储桶名称
         * @return
         */
        @SneakyThrows
        public List<String> listObjectNames(String bucketName) {
            List<String> listObjectNames = new ArrayList<>();
            boolean flag = bucketExists(bucketName);
            if (flag) {
                Iterable<Result<Item>> myObjects = listObjects(bucketName);
                for (Result<Item> result : myObjects) {
                    Item item = result.get();
                    listObjectNames.add(item.objectName());
                }
            }
            return listObjectNames;
        }

        /**
         * 列出存储桶中的所有对象
         *
         * @param bucketName 存储桶名称
         * @return
         */
        @SneakyThrows
        public Iterable<Result<Item>> listObjects(String bucketName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                ListObjectsArgs args=ListObjectsArgs.builder()
                        .bucket(bucketName).build();
                return minioClient.listObjects(args);
            }
            return null;
        }

        /**
         * 通过文件上传到对象
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @param fileName   File name
         * @return
         */
        @SneakyThrows
        public boolean putObject(String bucketName, String objectName, String fileName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                PutObjectArgs args=PutObjectArgs.builder()
                        .bucket(bucketName).object(objectName)
                        .build();
                minioClient.putObject(args);
                StatObjectResponse statObject = statObject(bucketName, objectName);
                if (statObject != null && statObject.size() > 0) {
                    return true;
                }
            }
            return false;

        }

        /**
         * 文件上传
         *
         * @param bucketName
         * @param multipartFile
         */
      /*  @SneakyThrows
        public void putObject(String bucketName, MultipartFile multipartFile, String filename) {
            //PutObjectOptions putObjectOptions = new PutObjectOptions(multipartFile.getSize(), PutObjectOptions.MIN_MULTIPART_SIZE);
           // putObjectOptions.setContentType(multipartFile.getContentType());
           // minioClient.putObject(bucketName, filename, multipartFile.getInputStream(), putObjectOptions);
        }*/

        /**
         * 通过InputStream上传对象
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @param stream     要上传的流
         * @return
         */
        @SneakyThrows
        public boolean putObject(String bucketName, String objectName, InputStream stream) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                //10m
                PutObjectArgs args=PutObjectArgs.builder().bucket(bucketName)
                        .object(objectName).stream(stream,stream.available(),10*1024*1024).build();
                minioClient.putObject(args);
                StatObjectResponse statObject = statObject(bucketName, objectName);
                if (statObject != null && statObject.size() > 0) {
                    return true;
                }
            }
            return false;
        }

        /**
         * 以流的形式获取一个文件对象
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @return
         */
        @SneakyThrows
        public InputStream getObject(String bucketName, String objectName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                StatObjectResponse statObject = statObject(bucketName, objectName);
                if (statObject != null && statObject.size() > 0) {
                    GetObjectArgs args=GetObjectArgs.builder().bucket(bucketName)
                            .object(objectName).build();
                    InputStream stream = minioClient.getObject(args);
                    return stream;
                }
            }
            return null;
        }

        /**
         * 以流的形式获取一个文件对象（断点下载）
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @param offset     起始字节的位置
         * @param length     要读取的长度 (可选，如果无值则代表读到文件结尾)
         * @return
         */
        @SneakyThrows
        public InputStream getObject(String bucketName, String objectName, long offset, Long length) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                StatObjectResponse statObject = statObject(bucketName, objectName);
                if (statObject != null && statObject.size() > 0) {
                    GetObjectArgs args = GetObjectArgs.builder().bucket(bucketName)
                            .object(objectName).offset(offset).length(length).build();
                    InputStream stream = minioClient.getObject(args);
                    return stream;
                }
            }
            return null;
        }

        /**
         * 下载并将文件保存到本地
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @param fileName   File name
         * @return
         */
        @SneakyThrows
        public boolean getObject(String bucketName, String objectName, String fileName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                StatObjectResponse statObject = statObject(bucketName, objectName);
                if (statObject != null && statObject.size() > 0) {
                    GetObjectArgs args = GetObjectArgs.builder().bucket(bucketName)
                            .object(objectName).build();
                    GetObjectResponse response = minioClient.getObject(args);
                    byte[] buffer = new byte[255];
                    int res = 0;
                    while ((res = response.read(buffer, 0, buffer.length)) > 0) {
                        //写入文件
                    }
                    return true;
                }
            }
            return false;
        }

        /**
         * 删除一个对象
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         */
        @SneakyThrows
        public boolean removeObject(String bucketName, String objectName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                RemoveObjectArgs args=RemoveObjectArgs.builder()
                        .bucket(bucketName).object(objectName).build();
                minioClient.removeObject(args);
                return true;
            }
            return false;
        }

        /**
         * 删除指定桶的多个文件对象,返回删除错误的对象列表，全部删除成功，返回空列表
         *
         * @param bucketName  存储桶名称
         * @param objectNames 含有要删除的多个object名称的迭代器对象
         * @return
         */
        @SneakyThrows
        public List<String> removeObject(String bucketName, List<String> objectNames) {
            List<String> deleteErrorNames = new ArrayList<>();
            boolean flag = bucketExists(bucketName);
            if (flag) {
                List<DeleteObject> deleteObjects=new ArrayList<>();
                for (String name:objectNames) {
                    deleteObjects.add(new DeleteObject(name));
                }
                RemoveObjectsArgs args=RemoveObjectsArgs.builder()
                        .bucket(bucketName).objects(deleteObjects)
                        .build();
                Iterable<Result<DeleteError>> results = minioClient.removeObjects(args);
                for (Result<DeleteError> result : results) {
                    DeleteError error = result.get();
                    deleteErrorNames.add(error.objectName());
                }
            }
            return deleteErrorNames;
        }

        /**
         * 生成一个给HTTP GET请求用的presigned URL。
         * 浏览器/移动端的客户端可以用这个URL进行下载，即使其所在的存储桶是私有的。这个presigned URL可以设置一个失效时间，默认值是7天。
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @param expires    失效时间（以秒为单位），默认是7天，不得大于七天
         * @return
         */
        @SneakyThrows
        public String presignedGetObject(String bucketName, String objectName, Integer expires) {
            //boolean flag = bucketExists(bucketName);
            String url = "";
            /*if (flag) {
                if (expires < 1 || expires > DEFAULT_EXPIRY_TIME) {
                    throw new InvalidExpiresRangeException(expires,
                            "expires must be in range of 1 to " + DEFAULT_EXPIRY_TIME);
                }
                url = minioClient.presignedGetObject(bucketName, objectName, expires);
            }*/
            return url;
        }

        /**
         * 生成一个给HTTP PUT请求用的presigned URL。
         * 浏览器/移动端的客户端可以用这个URL进行上传，即使其所在的存储桶是私有的。这个presigned URL可以设置一个失效时间，默认值是7天。
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @param expires    失效时间（以秒为单位），默认是7天，不得大于七天
         * @return
         */
        @SneakyThrows
        public String presignedPutObject(String bucketName, String objectName, Integer expires) {
            //boolean flag = bucketExists(bucketName);
            String url = "";
            /*if (flag) {
                if (expires < 1 || expires > DEFAULT_EXPIRY_TIME) {
                  System.out.println ( "expires must be in range of 1 to " + DEFAULT_EXPIRY_TIME);
                }
                url = minioClient.presignedPutObject(bucketName, objectName, expires);
            }*/
            return url;
        }

        /**
         * 获取对象的元数据
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @return
         */
        @SneakyThrows
        public StatObjectResponse statObject(String bucketName, String objectName) {
            boolean flag = bucketExists(bucketName);
            if (flag) {
                StatObjectArgs args=StatObjectArgs.builder().bucket(bucketName)
                        .object(objectName).build();
                StatObjectResponse statObject = minioClient.statObject(args);
                return statObject;
            }
            return null;
        }

        /**
         * 文件访问路径
         *
         * @param bucketName 存储桶名称
         * @param objectName 存储桶里的对象名称
         * @return
         */
        @SneakyThrows
        public String getObjectUrl(String bucketName, String objectName) {
            boolean flag = bucketExists(bucketName);
            String url = "";
            if (flag) {
                GetPresignedObjectUrlArgs args=GetPresignedObjectUrlArgs.builder()
                        .bucket(bucketName).object(objectName).build();
                url = minioClient.getPresignedObjectUrl(args);
            }
            return url;
        }



        public void downloadFile(String bucketName, String fileName) {
            try {
                DownloadObjectArgs args1 = DownloadObjectArgs.builder()
                        .bucket(bucketName).filename(fileName)
                        .build();
                minioClient.downloadObject(args1);
            }catch (Exception ex){
                ex.printStackTrace();
            }
        }
}
