import os
from BPython import BClass

def printHello():
    print("Hello World")
    print("""
        Output: Search term: iphone 15
        Output: Start crawling mudah
        Output: Start crawling aihuishou
        Output: Successfully indexed: 82
        Output: Failed to index: []
        Output: Successfully indexed: 353
        Output: Failed to index: []
        Output: success
        """)

def printSomething(val):
    print(f'Hello you inputed: {val}')

def printCurrentDirectory():
    workingDir = os.getcwd()
    print(workingDir)

def usingAnotherClass():
    bclass = BClass()
    bclass.AFunction()

def classReturn():
    return True

printHello();