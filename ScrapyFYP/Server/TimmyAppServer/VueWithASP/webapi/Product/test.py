import numpy as np

# 初始化数据集
data = []

# 添加新数据项
def add_data(new_data):
    data.extend(new_data)

# 判断数据正确性
def check_data():
    if len(data) >= 10:
        # 计算第1和第99百分位数以及平均价格
        p1 = np.percentile(data, 1)
        p99 = np.percentile(data, 99)
        avg = np.mean(data)
        
        # 判断新数据项是否在合理范围内
        for new_price in data[-10:]:
            if not (p1 <= new_price <= p99):
                print(f"价格 {new_price} 超出合理范围 ({p1}-{p99})，可能是不正确的数据")
                print(avg)
            else:
                print(f"价格 {new_price} 在合理范围内 ({p1}-{p99})")
                print(avg)
    else:
        print("数据量不足，无法判断数据正确性")

# 示例数据：每次添加十个数据项
new_data = [1700, 300, 1800, 2000, 1700, 4000, 2000, 2100, 1800, 1900, 1800, 20, 300]
add_data(new_data)

# 判断数据正确性
check_data()
