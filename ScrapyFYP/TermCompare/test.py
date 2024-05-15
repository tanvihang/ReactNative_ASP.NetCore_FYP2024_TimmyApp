from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity

# 输入文本
input_text = "watch 1 ultra"

# 集合
corpus = ['watch ultra', 'watch 1']
mostSim = 0
minIndex = 999
# Split each string into words
corpus_words = [text.split() for text in corpus]
print(corpus_words)

# Initialize the index of the first difference to None
first_different_index = None

# Iterate through each word index in the first string
for i in range(len(corpus_words[0])):
    # Get the word at index i in the first string
    word = corpus_words[0][i]
    # Check if the word at index i is different in any other string
    if any(i >= len(text) or word != text[i] for text in corpus_words[1:]):
        first_different_index = i
        break

# If no difference is found, set the first different index to the length of the shortest word
if first_different_index is None:
    first_different_index = min(len(word) for word in (corpus_words[0]))

print("First different index:", first_different_index)


# After get first different index, see whose is lowest
for i in range(len(corpus)):
    index = input_text.find(corpus_words[i][first_different_index])
    
    print(index)
    minIndex = index if index < minIndex else minIndex
    mostSim = i if index<=minIndex else mostSim
    

print(corpus[mostSim])


# # 输出结果
# print("输入文本:", input_text)
# print("最相似的文本:", most_similar_text)
