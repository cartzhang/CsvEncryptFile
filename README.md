# Csv Encrypt File
Csv Encrpt Decrypt

文件加密和解密C#代码

标签（空格分隔）：加密 C#

---

2016-11-02

最初版本：
  固定密码可以使用。
  
  
2016-11-03

 在修改密码在base64时候，由于长度或字符问题，造成的解码失败。

修改了原来的加密算法RijndaelManaged，改为它基类的加密SymmetricAlgorithm。

 现在可以设置任意密码加密。

2016-11-14

添加了Unity编辑器下右键，直接将CVS文件转换成二进制文件的快捷方式。

使用Convert.ToBase64String和Convert.FromBase64String()来处理为字节字符串。
里面代码由GT同事编写。



## 参考：

https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
http://www.codeproject.com/Articles/415732/Reading-and-Writing-CSV-Files-in-Csharp
http://stackoverflow.com/questions/11762/cryptographicexception-padding-is-invalid-and-cannot-be-removed




