import pyodbc

class TimmyDatabase:
    def __init__(self):
        # 连接字符串
        self.server = '(localdb)\\localDB1'
        self.database = 'TimmyDBV2'
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
    
    def getProductModel(self, category, brand):

        try:
            # 在这里可以执行SQL查询或其他数据库操作
            # Define the SQL query to retrieve data from the TimmyProduct table
            sql_query = "SELECT timmy_product_model FROM TimmyProduct WHERE timmy_product_category = ? AND timmy_product_brand = ? AND timmy_product_adopted = 1"

            # Execute the SQL query
            self.cursor.execute(sql_query,(category,brand))

            # Fetch all rows from the result set
            rows = self.cursor.fetchall()

            productList = []

            for row in rows:
                productList.append(row[0])

            return productList
        except Exception as e:
            print("An error occured: ", e)

    def getCategories(self):
        try:
            # Define the SQL query to retrieve distinct categories from the TimmyProduct table
            sql_query = "SELECT DISTINCT timmy_product_category FROM TimmyProduct"

            # Execute the SQL query
            self.cursor.execute(sql_query)

            # Fetch all rows from the result set
            rows = self.cursor.fetchall()

            categoryList = []

            for row in rows:
                categoryList.append(row[0])

            return categoryList
        except Exception as e:
            print("An error occurred: ", e)

    def closeConnection(self):
        # 关闭连接
        self.cursor.close()
        self.connection.close()
        print("连接已关闭")
    

