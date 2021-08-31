import base64 

class Base64Helper :
    @staticmethod
    def encodestring(str):
        base64_str = base64.encodestring(str)
        return base64_str

    @staticmethod
    def decodestring(base64_str):
        str = base64.decodestring(base64_str)
        return str
