package com.utility.database.adapter;

import com.utility.database.*;

public  class OracleColumnAdapter extends AbstractColumnAdapter implements ColumnAdapter {
    @Override
    protected DatabaseFactory getDatabaseFactory(){
        if(databaseFactory==null){
            databaseFactory=new OracleFactory();
        }
        return databaseFactory;
    }

   
}