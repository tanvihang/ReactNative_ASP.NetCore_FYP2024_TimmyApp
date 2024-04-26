import configparser
import logging
from datetime import datetime
from testLoggingError import cls

config = configparser.ConfigParser()
config.read('config.ini')

server = '(localdb)\\localDB1'
print(type(server))
print(server)


db_server = config['Database']['server']
print(type(db_server))
print(db_server)


logging.basicConfig(level = logging.INFO, format='[%(asctime)s] {%(name)s} %(levelname)s:  %(message)s', 
                    datefmt='%y-%m-%d %H:%M:%S',
                    filename=f'./Log/test_{datetime.now().isoformat().replace(":","")}_logs.log')
logger = logging.getLogger('Test_logger')
logger.error("Scrapy Log to display Error messages")

clsObj = cls()

try:
    clsObj.errorFunc()
except Exception as e:
    logger.error(e)

filename = f'log_{datetime.now()}'
print(filename)