# 命令总览

```
openssl genrsa -out ca-key.pem 4096
openssl req -out ca-req.csr -key ca-key.pem -new
# 设定Common Name 为 azCA
openssl x509 -out ca-cert.cer -req -in ca-req.csr -signkey ca-key.pem -days 365

# 服务端
openssl genrsa -out azFTserver-key.pem 2048 
# 设定Common Name 为 azFTServer-cert
# OName ：Az-org
openssl req -out azFTserver-req.csr -key azFTserver-key.pem -new
openssl x509 -out azFTserver-cert.cer -req -in azFTserver-req.csr -CA ca-cert.cer -CAkey ca-key.pem -CAcreateserial -days 365

#客户端
openssl genrsa -out azFTclient-key.pem 2048
openssl req -out azFTclient-req.csr -key azFTclient-key.pem -new
# azFTClient-cert
openssl x509 -out azFTclient-cert.cer -req -in azFTclient-req.csr -CA ca-cert.cer -CAkey ca-key.pem -CAcreateserial -days 365 



openssl pkcs12 -export -out azFTclient.pfx -inkey azFTclient-key.pem -in azFTclient-cert.cer -certfile ca-cert.cer

openssl pkcs12 -export -out azFTserver.pfx -inkey azFTserver-key.pem -in azFTserver-cert.cer -certfile ca-cert.cer
```





```
您可以按照以下步骤使用OpenSSL来创建一个带有自定义主题名称的自签名证书：

1. 下载并安装OpenSSL工具，它是一个免费的开放源代码安全套接字层（SSL）和传输层安全（TLS）协议的实现。

2. 打开命令提示符或终端窗口。

3. 输入以下命令以生成一个私钥：

   openssl genpkey -algorithm RSA -out private.key

   这会生成一个2048位的RSA密钥对，并将私钥保存到名为"private.key"的文件中。

4. 输入以下命令以生成一个证书签名请求（CSR）：

   openssl req -new -key private.key -out cert.csr

   在这个命令中，您需要回答一些关于您的组织和证书的问题。当您被要求输入"Common Name"时，您可以输入您想要用作主题名称的名称。例如，如果您要将证书用于服务器，则可以输入服务器的完全限定域名（FQDN）作为主题名称。

5. 生成自签名证书，可以使用以下命令：

   openssl x509 -req -in cert.csr -signkey private.key -out cert.crt

   这会使用您的私钥对证书签名请求进行签名，并将签名证书保存到名为"cert.crt"的文件中。

现在您已经创建了一个带有自定义主题名称的自签名证书。请注意，在生产环境中，应使用受信任的证书颁发机构（CA）签名证书，以确保证书的真实性和可信度。
```





```
其中，private.key为您的私钥文件，certificate.crt为服务端/客户端证书文件，ca.crt为自签CA根证书文件。运行该命令后，将会要求您输入pfx文件的密码。

将生成的pfx文件拷贝到Windows系统上，双击打开该文件，输入您在步骤1中设置的密码。

在弹出的“证书导入向导”中，选择“将所有的证书都放入下列存储”，并点击“浏览”。

在“选择证书存储”对话框中，选择“个人”，并点击“确定”。

返回“证书导入向导”，点击“下一步”，然后点击“完成”。

现在您的证书和私钥已经成功导入到Windows的证书存储中。如果您在配置WCF服务时需要使用该证书和私钥，可以通过配置文件或代码来指定证书和私钥的存储位置和名称。





Regenerate response
```







```
System.InvalidOperationException
  HResult=0x80131509
  Message=未提供客户端证书。请在 ClientCredential 中指定一个客户端证书。
  Source=mscorlib
  StackTrace:
   at System.Runtime.Remoting.Proxies.RealProxy.HandleReturnMessage(IMessage reqMsg, IMessage retMsg)
   at System.Runtime.Remoting.Proxies.RealProxy.PrivateInvoke(MessageData& msgData, Int32 type)
   at AzFileTransfer.ServiceReference1.IService1.GetAllFiles()
   at AzFileTransfer.ServiceReference1.Service1Client.GetAllFiles() in C:\Users\jovy-\source\repos\AzFileTransfer\AzFileTransfer\Connected Services\ServiceReference1\Reference.cs:line 79
   at AzFileTransfer.MainWindow.GetFileInfoS() in C:\Users\jovy-\source\repos\AzFileTransfer\AzFileTransfer\MainWindow.xaml.cs:line 38
   at AzFileTransfer.MainWindow..ctor() in C:\Users\jovy-\source\repos\AzFileTransfer\AzFileTransfer\MainWindow.xaml.cs:line 29
```



certutil -store "My" | findstr az

```

openssl req -newkey rsa:2048 -nodes -keyout ca.key -x509 -days 365 -out ca.crt
openssl req -newkey rsa:2048 -nodes -keyout client.key -out client.csr
openssl req -newkey rsa:2048 -nodes -keyout server.key -out server.csr

AZS-cert



openssl x509 -req -in client.csr -CA ca.crt -CAkey ca.key -CAcreateserial -out client.crt -days 365 -extensions client_cert -extfile <(echo "[client_cert]\nextendedKeyUsage=clientAuth"))
openssl x509 -req -in server.csr -CA ca.crt -CAkey ca.key -CAcreateserial -out server.crt -days 365 -extensions server_cert -extfile <(echo "[server_cert]\nextendedKeyUsage=serverAuth"))

openssl pkcs12 -export -in client.crt -inkey client.key -out client.pfx
openssl pkcs12 -export -in server.crt -inkey server.key -out server.pfx

```

