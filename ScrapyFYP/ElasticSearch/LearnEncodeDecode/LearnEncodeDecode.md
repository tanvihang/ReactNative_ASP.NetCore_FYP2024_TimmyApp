# Learn "text" Encode Decode
>purpose: to be comfortable with different character such as 😆 and "誉" when encoding into file, and decode it perfectly, because sometimes the process is weird for me **because** i do not know type of encoding

### Type of text encoder
1. utf-8: contains emoji & chinese also
2. utf-16: it contains asian languages
3. ISO-8859-1(latin-1): most Western European language
4. ASCII: oldest and simplest encoding
5. GB2312, GBK, GB18030: specific character encoding developed for chinse characters

### Python encode and decode
`demotext.txt` included some chinese and emoji character, should be able to decode properly with `UTF-8`

```python
def readfile(fileurl):
    # open file with 'utf-8' encoding
    with open(file=fileurl, mode='r', encoding='utf-8') as file:
        for line in file:
            print(line.strip())

readfile("./LearnEncodeDecode/demotext.txt")
```

- **error** says 'gbk' codec can't encode character '\U0001f43c' in position 39: illegal multibyte sequence
  - maybe it's the problem when printing, i think it's using *gbk* when encoding to print out

```bash
Hi this is a text with most error type when decoding
Traceback (most recent call last):
  File "d:\UniversityFile\Year4\ScrapyFYP\ElasticSearch\LearnEncodeDecode\encodedecode.py", line 9, in <module>
    readfile("./LearnEncodeDecode/demotext.txt")
  File "d:\UniversityFile\Year4\ScrapyFYP\ElasticSearch\LearnEncodeDecode\encodedecode.py", line 7, in readfile
    print(line.strip())
UnicodeEncodeError: 'gbk' codec can't encode character '\U0001f43c' in position 39: illegal multibyte sequence
```

解决办法https://blog.csdn.net/weixin_54168711/article/details/129738939 ，改变系统terminal的encode方式即可

写和读都没有问题啦