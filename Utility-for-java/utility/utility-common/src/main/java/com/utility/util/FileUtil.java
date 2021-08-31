package com.utility.util;

import java.io.*;
import java.util.ArrayList;
import java.util.List;

public class FileUtil {
    public  static  final  String ReadMode="r";
    public  static  final  String ReadWriteMode="rw";
    public  static  final  String WriteMode="w";

    public  static void create(String path)   {
      File file=new File(path);
      if(!file.exists()){
          if(file.isDirectory())
          {
              file.mkdirs();
          }else{
              try {
                  //String p=file.getParent();
                 // String[] strings = path.split("\\\\");
                  //File file1=new File(path.replace(strings[strings.length-1],"")+"\\");
                  //if(file1.isDirectory())
                  {
                      file.mkdirs();
                  }
                 // file.createNewFile();
                  //file.mkdirs();
              }catch (Exception e){
                  e.printStackTrace();
              }
          }
      }
    }

    public  static String readString(String path){
        byte[] data=readByte(path);
        if(data==null)
        {
            return  "";
        }
        return  new String(data);
    }

    public  static byte[] readByte(String path){
        File file=new File(path);
        InputStream inputStream=null;
        try {
            inputStream=new FileInputStream(file);
            byte[] buffer=new byte[255];
            int count=0;
            byte[] data=new byte[(int)file.length()];
            int res=0;
            while ((res=inputStream.read(buffer,0,buffer.length))>-1){
                System.arraycopy(buffer, 0,data,count,res);
                count+=res;
            }
            return  data;
        }catch (FileNotFoundException ex){
            ex.printStackTrace();
            return null;
        }catch (IOException ex){
            ex.printStackTrace();
            return  null;
        }finally {
            if(inputStream!=null){
                try {
                    inputStream.close();
                }catch (IOException ex){
                    ex.printStackTrace();
                }
            }
        }
    }

    public  static char[] readChar(String path){
        Reader reader=null;
        try {
            reader=new FileReader(path);
            File file=new File(path);
            char[] buffer=new char[255];
            int count=0;
            char[] data=new char[(int)file.length()];
            int res=0;
            while ((res=reader.read(buffer,0,buffer.length))>-1){
                System.arraycopy(buffer, 0,data,count,res);
                count+=res;
            }
            return  data;
        }catch (FileNotFoundException ex){
            ex.printStackTrace();
            return null;
        }catch (IOException ex){
            ex.printStackTrace();
            return  null;
        }finally {
            if(reader!=null){
                try {
                    reader.close();
                }catch (IOException ex){
                    ex.printStackTrace();
                }
            }
        }
    }

    public  static String[] readLine(String path){
        BufferedReader reader=null;
        FileReader fileReader=null;
        try {
            fileReader=new FileReader(path);
            reader=new BufferedReader(fileReader);
            List<String> data=new ArrayList<>();
            while (reader.read()>-1){
               data.add(reader.readLine());
            }
            String[] strs=new String[data.size()];
            return  data.toArray(strs);
        }catch (FileNotFoundException ex){
            ex.printStackTrace();
            return null;
        }catch (IOException ex){
            ex.printStackTrace();
            return  null;
        }finally {
            if(fileReader!=null){
                try {
                    fileReader.close();
                }catch (IOException ex){
                    ex.printStackTrace();
                }
            }
            if(reader!=null){
                try {
                    reader.close();
                }catch (IOException ex){
                    ex.printStackTrace();
                }
            }
        }
    }

    public  static  byte[]  readRandom(String path,String mode) {
        RandomAccessFile randomAccessFile = null;
        try {
            randomAccessFile = new RandomAccessFile(path, mode);
            randomAccessFile.seek(0);
            byte[] buffer = new byte[255];
            int count = 0;
            byte[] data = new byte[(int) randomAccessFile.length()];
            int res = 0;
            while ((res = randomAccessFile.read(buffer, 0, buffer.length)) > -1) {
                System.arraycopy(buffer, 0, data, count, res);
                count += res;
            }
            return data;

        } catch (FileNotFoundException ex) {
            ex.printStackTrace();
            return null;
        } catch (IOException ex) {
            ex.printStackTrace();
            return null;
        } finally {
            if (randomAccessFile != null) {
                try {
                    randomAccessFile.close();
                } catch (IOException ex) {
                    ex.printStackTrace();
                }
            }
        }
    }

    public  static  boolean  appent(String path,String content){
        return  write(path, content, true);
    }

    public  static  boolean  write(String path,String content,boolean append){
        FileWriter fileWriter=null;
        try {
            //create(path);
            fileWriter=new FileWriter(path,append);
            fileWriter.write(content);
            return  true;
        }catch (IOException ex){
            ex.printStackTrace();
            return  false;
        }
        finally {
            if(fileWriter!=null){
                try {
                    fileWriter.close();
                }catch (IOException ex){
                    ex.printStackTrace();
                }
            }
        }

    }

    public static  List<String> getFiles(String  path){
        File file=new File(path);
        if(file.exists()){
            if(file.isDirectory()){
                List<String> files=new ArrayList<>();
                for (File f:file.listFiles()  ) {
                    files.add(f.getPath());
                }
                return  files;
            }
        }
        return  null;
    }
}
