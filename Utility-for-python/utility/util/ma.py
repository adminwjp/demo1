# pip install object-mapper
# https://github.com/marazt/object-mapper
# https://pypi.org/project/object-mapper/
from mapper.object_mapper import ObjectMapper
mapper = ObjectMapper()
def create_map(obj1, obj2):
    obj2=mapper.create_map(obj1, obj2)

def map(obj1, obj2):
    obj2=mapper.map(obj1, obj2, ignore_case=True)

def set(obj1, obj2):
    """
    Class to Class
    通过setattr函数赋值属性，如果有值就赋值属性和值
    """
    for name in [name for name in dir(obj2) if not name.startswith("_")]:
        if name not in obj1:
            setattr(obj2, name, None)
        else:
            value = getattr(obj, name)
            setattr(obj2, name, value)
