import re

chinese_pattern="[\u4e00-\u9fa5]" #判断字符是汉字
number_pattern="[0-9]" #判断字符是数字

def is_match(str,pattern):
    return re.match(papattern,str) != None