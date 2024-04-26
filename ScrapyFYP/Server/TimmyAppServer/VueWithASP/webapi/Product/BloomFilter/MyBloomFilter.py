import math
import hashlib
import array
import pickle
import json

class BloomFilter:
    """
    Initializes a Bloom Filter with given capacity and error rate.
    
    Args:
    - capacity: The expected number of elements to be stored in the Bloom Filter.
    - error_rate: The acceptable false positive rate for the Bloom Filter.
    """
    def __init__(self, capacity, error_rate):
        self.capacity = capacity
        self.error_rate = error_rate
        self.bit_array_size = self.calculate_bit_array_size()
        self.num_hashes = self.calculate_num_hashes()
        self.bit_array = array.array('b', [0] * self.bit_array_size)
        print(self.bit_array_size)
        print(self.num_hashes)


    # 计算出存储大小
    
    def calculate_bit_array_size(self):
        """
        Calculates the size of the bit array needed for the Bloom Filter.
        
        Returns:
        - The calculated size of the bit array.
        """
        m = - (self.capacity * math.log(self.error_rate)) / (math.log(2) ** 2)
        return int(math.ceil(m))

    def calculate_num_hashes(self):
        """
        Calculates the number of hash functions needed for the Bloom Filter.
        
        Returns:
        - The calculated number of hash functions.
        """        

        k = (self.bit_array_size / self.capacity) * math.log(2)
        return int(math.ceil(k))

    def add(self, element):
        """
        Adds an element to the Bloom Filter.
        
        Args:
        - element: The element to be added to the Bloom Filter.
        """
        for i in range(self.num_hashes):
            hash_val = self.hash_function(element, i)
            index = hash_val % self.bit_array_size
            self.bit_array[index] += 1

    def remove(self, element):
        """
        Removes an element from the Bloom Filter.
        
        Args:
        - element: The element to be removed from the Bloom Filter.
        """        
        for i in range(self.num_hashes):
            hash_val = self.hash_function(element,i)
            index = hash_val % self.bit_array_size
            if self.bit_array[index] > 0:
                self.bit_array[index] -= 1

    def __contains__(self, element):
        """
        Checks if an element is possibly in the Bloom Filter.
        
        Args:
        - element: The element to be checked.
        
        Returns:
        - True if the element is possibly in the Bloom Filter, False otherwise.
        """
        for i in range(self.num_hashes):
            hash_val = self.hash_function(element, i)
            index = hash_val % self.bit_array_size
            if self.bit_array[index] == 0:
                return False
        return True

    def save_to_file(self, filename):
        """
        Saves the Bloom Filter object to a file.
        
        Args:
        - filename: The name of the file to save the Bloom Filter object to.
        """        
        with open(filename, 'wb') as f:
            pickle.dump(self, f)

    @classmethod
    def load_from_file(cls, filename):
        """
        Loads a Bloom Filter object from a file.
        
        Args:
        - filename: The name of the file containing the Bloom Filter object.
        
        Returns:
        - The Bloom Filter object loaded from the file.
        """
        with open(filename, 'rb') as f:
            return pickle.load(f)

    def hash_function(self, element, seed):
        """
        Generates a hash value for an element using a seed value.
        
        Args:
        - element: The element to be hashed.
        - seed: The seed value for the hash function.
        
        Returns:
        - The hash value of the element.
        """
        hash_val = hashlib.md5((element + str(seed)).encode()).hexdigest()
        print(hash_val)
        print(type(hash_val))
        # add more hash function here to make it more unique for each input
        return int(hash_val, 16)
    
    def batch_add(self,filename):
        """
        Adds elements from a JSON file to the Bloom Filter.
        
        Args:
        - filename: The name of the JSON file containing elements to be added.

        Returns:
        - The count of the element added into Bloom Filter.
        """


        base_file_path="./Exampledata/"
        file_path = base_file_path + filename
        count = 0


        with open(file_path,'r' , encoding='utf-8') as file:
            jsondata = json.load(file)

            for item in jsondata:
                unique_id = str(item['unique_id'][0])
                if(unique_id in self):
                    # print(f"Item already added, banishing current item {unique_id}")
                    continue
                else:
                    count+=1
                    self.add(unique_id)
        
        return count



def TestFunction1():
    """
    Adds all data into the a newly created BloomFilter and prints out the item count.
    Compare it against a set of data with the BloomFilter and return the same element count.
    """

    # create a BloomFilter
    bloom_filter = BloomFilter(capacity=1000000, error_rate=0.001)

    test_data_file = ["Iphone15.json","Iphone14.json","Iphone13.json","Iphone12.json",
                        "HuaweiMate20.json","HuaweiMate30.json","HuaweiMate40.json","HuaweiMate50.json",
                        "Xiaomi8.json","Xiaomi9.json","Xiaomi10.json","Xiaomi11.json","Xiaomi12.json"]

    compare_file = "./Exampledata/Compare.json"
    total_item_bloom_filter = 0
    count_bloom = 0
    fit_item_list = []

    # Add data into bloom filter
    for file in test_data_file:
        total_item_bloom_filter += bloom_filter.batch_add(file)

    # Comparing same item in bloom filter
    with open(compare_file, 'r',encoding='utf-8') as file:
        json_data = json.load(file)

        for data in json_data:
            unique_id = str(data['unique_id'][0])

            ret = unique_id in bloom_filter

            if(ret):
                fit_item_list.append(unique_id)
                count_bloom+=1
    
    print(f"Same item count using bloom - {count_bloom}")

    # Compare fit_item_list with other files
    count_actual = 0
    for filename in test_data_file:

        base_file_path="./Exampledata/"
        file_path = base_file_path + filename
        with open(file_path, 'r', encoding='utf-8') as file:
            json_data = json.load(file)

            for data in json_data:
                unique_id = str(data['unique_id'][0])

                if unique_id in fit_item_list:
                    count_actual += 1
                    fit_item_list.remove(unique_id)
                    try:
                        print(f"same item {data['title']}")
                    except:
                        print(f"encode problem {unique_id}")

    print(f"Same item count in actual files - {count_actual}")

    print(f"Total item in bloom filter - {total_item_bloom_filter}")

