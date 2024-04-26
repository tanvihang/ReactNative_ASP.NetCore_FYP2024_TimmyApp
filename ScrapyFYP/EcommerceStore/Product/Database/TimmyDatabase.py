import pyodbc
import configparser

config = configparser.ConfigParser()
config.read('config.ini')

class TimmyDatabase:
    def __init__(self):
        # 连接字符串
        self.server = config['Database']['server']
        self.database = config['Database']['database']
        self.driver = config['Database']['driver']  # 可能需要根据你安装的驱动程序版本进行调整"
        # self.server = '(localdb)\\localDB1'
        # self.database = 'TimmyDBV2'
        # self.driver = '{ODBC Driver 17 for SQL Server}'  # 可能需要根据你安装的驱动程序版本进行调整"


        self.tableName = config['Database']['tableName']

        print(self.server)
        self.ProductFullName = config['Database']['ProductFullName']
        self.ProductCategory = config['Database']['ProductCategory']
        self.ProductBrand = config['Database']['ProductBrand']
        self.ProductModel = config['Database']['ProductModel']
        self.ProductSubModel = config['Database']['ProductSubModel']
        self.ProductAdopted = config['Database']['ProductAdopted']

    def openConnection(self):
        # 尝试连接  
        try:
            self.connection = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};\
                            SERVER='+self.server+';\
                            DATABASE='+self.database+';\+')
            
            self.cursor = self.connection.cursor()
            print(f'{self.database} Connection Succeed')

        except Exception as e:
            print(f'{self.database} Connection Failed:', e)
    
    def getProductModel(self, category, brand):

        try:
            # 在这里可以执行SQL查询或其他数据库操作
            # Define the SQL query to retrieve data from the TimmyProduct table
            sql_query = f'SELECT {self.ProductModel} FROM {self.tableName} WHERE {self.ProductCategory} = ? AND {self.ProductBrand} = ? AND {self.ProductAdopted} = 1'

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
            sql_query = f'SELECT DISTINCT {self.ProductCategory} FROM {self.tableName}'

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
        print(f'{self.database} Connection Closed')
    

