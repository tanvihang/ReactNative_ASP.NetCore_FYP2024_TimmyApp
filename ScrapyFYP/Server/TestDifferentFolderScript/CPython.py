import os
from DPython import AFunction

def Hola():
    workingDir = os.getcwd()
    print(f"Hola from CPython that is in directory {workingDir}")

def CallAnotherPy():
    AFunction()