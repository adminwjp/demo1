""" db  """

import sqlite3
import threading

class SqliteUtil(object):
    db=""
    __thred_lock=threading.Lock()

    def __init__(self):
        pass

    def __new__(cls, *args, **kwargs):
        if not hasattr(SqliteUtil,"_instance"):
            with SqliteUtil.__thred_lock:
                 if not hasattr(SqliteUtil,"_instance"):
                     SqliteUtil._instance=object.__new__(cls)
        return SqliteUtil._instance

    def is_conn(self):
        if not hasattr(SqliteUtil,"conn") or self.conn!=None:
            with SqliteUtil.__thred_lock:
                if not hasattr(SqliteUtil,"conn") or self.conn!=None:
                    SqliteUtil.conn=sqlite3.connect(self.db)
                    print("Opened database successfully")
    
    def execute(self,sql,args):
        self.is_conn()
        conn=self.conn
        c=conn.cursor()
        c.execute(sql,args)
        conn.commit()

    def close(self):
        conn=self.conn
        if conn:
            conn.close()
            self.conn=None


