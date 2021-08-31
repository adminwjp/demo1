# pip install pywin32
# pip install pyinstaller
# pyinstaller -F -w -i manage.ico app.py

# 怎么打包 的 其他 文件 信息 没打包 进去

# 打包成 安装包 如何打包 打包失败
# python setup.py sdist 
# 安装 安装包
# python setup.py install	
from distutils.core import setup

options ={"name" : "utility", "version" : "1.0", 
       "py_modules" : ["utility.util.db", "utility.util.j", "utility.util.ma", "utility.util.nosql", "utility.util.reg",
                      "utility.util.service","utility.util.common","utility.util.http", "utility.util.zk"]}
setup(**options)




# pip3 install  setuptools -i http://pypi.douban.com/simple --trusted-host pypi.douban.com
# python setup.py bdist_egg
# python setup.py install

# 打包 安装 没 看见 文件夹  找到.egg
# python setup.py sdist 
#  python setup.py install	
#from setuptools import find_packages,setup

#  setuptools 调用 from distutils.core import setup  
"""
setup(
    name = "utility",
    version = "0.1",
    packages = find_packages(),
)
"""

#setup()

