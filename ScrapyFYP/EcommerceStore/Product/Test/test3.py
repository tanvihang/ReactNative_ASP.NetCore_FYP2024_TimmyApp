from BloomFilter.MyBloomFilter import BloomFilter
import os

file_path = f"./BloomFilter/aihuishouBloomFilter.bin"
if os.path.exists(file_path):
    myBloomFilter = BloomFilter.load_from_file(file_path)

hasItem = "ahs20240415133238219510" in myBloomFilter

print(hasItem)