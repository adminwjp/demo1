
import json

def json_deserialize(json_data, obj):
    py_data = json.loads(json_data)
    dic2class(py_data, obj)



def dic2class(py_data, obj):
    """
    Dict convert to Class
    通过setattr函数赋值属性，如果有值就赋值属性和值
    """
    for name in [name for name in dir(obj) if not name.startswith("_")]:
        if name not in py_data:
            setattr(obj, name, None)
        else:
            value = getattr(obj, name)
            setattr(obj, name, set_value(value, py_data[name]))




def set_value(value, py_data):
    """
    设置虚拟类属性值
    """
    if str(type(value)).__contains__("."):
        # value 为自定义类
        dic2class(py_data, value)
    elif str(type(value)) == "<class 'list'>":
        # value为列表
        if value.__len__() == 0:
            # value列表中没有元素，无法确认类型
            value = py_data
        else:
            # value列表中有元素，以第一个元素类型为准
            child_value_type = type(value[0])
            value.clear()
            for child_py_data in py_data:
                child_value = child_value_type()
                child_value = set_value(child_value, child_py_data)
                value.append(child_value)
    else:
        value = py_data
    return value