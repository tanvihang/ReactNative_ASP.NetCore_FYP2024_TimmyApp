from scrapy.crawler import CrawlerProcess
from scrapy.utils.project import get_project_settings
import argparse
import sys
import logging
from datetime import datetime

logging.basicConfig(level = logging.CRITICAL, format='[%(asctime)s] {%(name)s} %(levelname)s:  %(message)s', 
                    datefmt='%y-%m-%d %H:%M:%S',
                    filename=f'./Log/Crawler_Process_{datetime.now().isoformat().replace(":","")}_logs.log')
logger = logging.getLogger('CrawlerProcess_logger')


# EXECUTE script
# python .\Product\crawlerProcess.py -c mobile -b apple -m iphone 15 pro max -s mudah aihuishou -t 1 -i 10
def crawl_spiders(category = "", brandName = "", modelName = "", spiders = "", isTest = 0, iteration = 25):
    process = CrawlerProcess(get_project_settings())


    print(f"Search term: {category} + {brandName} + {modelName}")
    if(category == '' or brandName == ''):
        raise ValueError("input cannot be 'empty'")

    error_spiders = []

    for spider in spiders:
        print(f"Start crawling {spider}")
        try:
            process.crawl(spider, search = {"category":category, "brand":brandName,"model": modelName, "isTest": isTest, "iteration": iteration})
        except Exception as e:
            print(f"Exception occured while calling crawl : {e}")
            logger.critical(e)
            error_spiders.append(spider)

    process.start()

    if process.crawlers and any(crawler.has_error for crawler in process.crawlers):
        return "fail", error_spiders
    else:
        return "success", []

# -c mobile -b iphone -m 15 pro max -s mudah aihuishou
if __name__ == "__main__":
    # Create an ArgumentParser object
    parser = argparse.ArgumentParser(description='Description of your script')

    # Add arguments to the parser
    
    parser.add_argument('-c', '--category', type=str, nargs='+', help='Category name')
    parser.add_argument('-b', '--brand', type=str, nargs='+', help='Brand name')
    parser.add_argument('-m', '--model', type=str, nargs='*', help='Model name')
    parser.add_argument('-s', '--spider', type=str, nargs='+', help='List of spider')
    parser.add_argument('-t', '--test', type=str, nargs='+', help='Is test search')
    parser.add_argument('-i', '--iteration', type=str, nargs='+', help='Iteration')

    # Parse the command-line arguments
    args = parser.parse_args()


    categoryName = args.category or []

    brandName = args.brand or []

    modelName = args.model if args.model is not None else " "
    modelNameString = ' '.join(modelName)

    spiders = args.spider or []


    isTest = args.test
    isTest = int(isTest[0])

    iteration = args.iteration
    iteration = int(iteration[0])

    # Call the crawl_spiders function and get the status
    status, error_spiders = crawl_spiders(categoryName[0], brandName[0], modelNameString, spiders, isTest, iteration)

    # Log the error spiders
    if error_spiders:
        print("Spiders encountered errors:", error_spiders)

    # Return the status as the exit code
    if status == "fail":
        sys.exit(1)
    else:
        sys.exit(0)  # Exit with status code 0 for success
    

# def main(search, spider):
#     # Access the value of the 'search' argument
#     search_term = search

#     # Access the value of the 'spider' argument
#     spiders = spider

#     # Call the crawl_spiders function and get the status
#     status, error_spiders = crawl_spiders(search_term, spiders)

#     # Log the error spiders
#     if error_spiders:
#         print("Spiders encountered errors:", error_spiders)

#     # Return the status as the exit code
#     if status == "fail":
#         return False
#     else:
#         return True  # Exit with status code 0 for success

# main("iphone 13", ["mudah", "aihuishou"])