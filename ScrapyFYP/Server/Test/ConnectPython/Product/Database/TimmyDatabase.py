import pyodbc

class TimmyDatabase:
    def __init__(self, category, brand):
        self.category = category
        self.brand = brand

        # 连接字符串
        self.server = '(localdb)\\localDB1'
        self.database = 'TimmyDB'
        self.driver = '{ODBC Driver 17 for SQL Server}'  # 可能需要根据你安装的驱动程序版本进行调整"

    def openConnection(self):
        # 尝试连接  
        try:
            self.connection = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};\
                            SERVER='+self.server+';\
                            DATABASE='+self.database+';\+')
            
            self.cursor = self.connection.cursor()
            print("连接成功")

        except Exception as e:
            print("连接失败:", e)
    
    def getProduct(self):
        # 在这里可以执行SQL查询或其他数据库操作
        # Define the SQL query to retrieve data from the ProductsTimmy table
        sql_query = "SELECT model FROM ProductsTimmy WHERE category = ? AND brand = ?"

         # Execute the SQL query
        self.cursor.execute(sql_query,(self.category,self.brand))

        # Fetch all rows from the result set
        rows = self.cursor.fetchall()

        productList = []

        for row in rows:
            productList.append(row[0])

        return productList
    
    def closeConnection(self):
        # 关闭连接
        self.cursor.close()
        self.connection.close()
        print("连接已关闭")
    

