# 全部服务端配置

## 配置模板

```xml
<system.serviceModel>
    <behaviors>
        <!-- 定义服务行为 -->
    </behaviors>
    <bindings>
        <!-- 定义服务绑定 -->
    </bindings>
    <services>
        <!-- 定义服务 -->
    </services>
</system.serviceModel>
```

说明：

1. <behaviors> 元素：

   ```
   该元素用于定义服务行为，包括安全性、可靠性、实例管理等方面的配置。例如，您可以使用 <behaviors> 元素配置服务的身份验证模式、会话超时时间、错误处理策略等。
   ```

2. <bindings> 元素：

   ```
   该元素用于定义服务绑定，即服务使用的通信协议和消息格式。例如，您可以使用 <bindings> 元素配置服务使用的传输协议（如 TCP、HTTP、HTTPS 等）、编码方式（如 XML、JSON、二进制编码等）、消息大小限制等。
   ```

   

3. <services> 元素：

   ```
   该元素用于定义服务及其行为和绑定。例如，您可以使用 <services> 元素配置服务类型、服务地址、行为和绑定等。
   ```

## 示例配置文件

```xml


```

A<bindings>01. 配置文件中<security mode="Transport"> 是什么意思

```
在 WCF 配置文件中，<security> 元素用于指定服务的安全设置，例如安全模式、证书等。在给定的配置中，<security> 元素包含一个 mode 属性，用于指定安全模式。

在这个配置文件中，mode 属性被设置为 "Transport"，这意味着安全性将由传输层 (Transport Layer) 提供。也就是说，服务将使用安全的传输协议 (例如 HTTPS)，以确保传输的数据在传输过程中不会被窃听、篡改或伪造。

具体来说，"Transport" 安全模式将对服务绑定 (Binding) 所使用的传输协议进行加密，并且使用传输层安全性 (TLS) 协议进行身份验证和消息完整性验证。因此，在此模式下，服务和客户端之间的通信将是加密和安全的。

需要注意的是，<security> 元素的 mode 属性还可以设置为其他模式，例如 "Message"、"TransportWithMessageCredential" 等，用于提供不同级别的安全保护和身份验证方式。


```

B<behaviors>01.配置文件中 <clientCertificate>是什么意思？

```
在 WCF 配置文件中，<clientCertificate> 元素用于指定客户端证书的配置信息，例如证书存储位置、名称、查找方式等。其中，<authentication> 子元素用于指定客户端证书验证的选项。

在给定的配置中，<authentication> 子元素包含两个属性：

1. certificateValidationMode：用于指定客户端证书验证的模式。在此配置中，将该属性设置为 "PeerTrust"，表示客户端证书将由服务端信任的证书颁发机构 (CA) 进行验证。这意味着客户端证书必须由一个服务端信任的 CA 签发，才能被服务端信任和接受。如果客户端证书由不受信任的 CA 签发，服务端将无法验证该证书的有效性。
2. revocationMode：用于指定证书撤销检查的模式。在此配置中，将该属性设置为 "NoCheck"，表示服务端不会检查客户端证书是否已被撤销。这是一种弱化的验证方式，因为即使证书已被撤销，服务端也仍然会信任该证书。

需要注意的是，<authentication> 子元素的具体配置内容会因实际需求和场景而有所不同。例如，如果您需要使用更严格的证书验证方式，可以将 certificateValidationMode 属性设置为 "ChainTrust"，这将启用整个证书链的验证，并且要求所有证书都必须由信任的 CA 签发。如果您需要在服务端检查证书的撤销状态，可以将 revocationMode 属性设置为 "Online" 或 "Offline"，这将启用在线或离线的撤销检查。
```

B<behaviors>02.service_certificate_name名字就是我导入的文件名吗

```
不一定。在 WCF 中，服务端需要使用证书对服务进行身份验证，因此需要指定证书的名称。这个证书名称通常是在安装证书时指定的，而不是文件名。

当您从证书存储区域（例如 Windows 证书管理器）中导入证书时，您需要指定证书的主题名称（Subject）或指纹（Thumbprint）。在 WCF 配置文件中，服务端证书的名称（service_certificate_name）应该是指定证书的主题名称或指纹。

在这个示例中，服务端证书的名称是 "mycertificate"，它是根据证书存储区域中证书的主题名称（Subject Name）查找得到的，而不是文件名。

如果您不确定证书的名称，可以打开证书存储区域（例如 Windows 证书管理器），查找并选择相应的证书，然后在证书属性中查看证书的主题名称或指纹，以确定在 WCF 配置文件中应该使用的服务端证书名称。
```

B<behaviors>03.service_certificate_name名字就是我导入的文件名吗

:aC-cert





