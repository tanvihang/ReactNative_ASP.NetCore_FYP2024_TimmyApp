# TimmyAppServer

| Task                   | About            | Time      |
| ---------------------- | ---------------- | --------- |
| Create Server          | using api server | 2024/3/19 |
| Create Database Scheme | use SQL server   | 2024/3/19 |
| Create User Api's      | document in md   | 2024/3/20 |
| Create Search Api's    | document in md   | 2024/3/20 |
| Create subscribe Api's | ''               | 2024/3/20 |

---

# 1 Create Server
https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-vue?view=vs-2022
因为现在用的是ES javascript，模块化的，再导入的时候运行出错就去相应的文件修改就可以了，修改成这样


# 2 将Model移动进入C#
```cmd
dotnet ef dbcontext scaffold "Data Source=(localdb)\localDB1;Initial Catalog=TimmyDB;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context TimmyDbContext --no-build -f
```

# 3 将ElasticSearch移入C#里面
一整个过程比较煎熬，因为文档页好乱，可是最后可以成功了，备份好了
https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/writing-queries.html

