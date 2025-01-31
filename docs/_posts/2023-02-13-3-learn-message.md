---
layout: post
title:  "3. 了解 Go-CqHttp 中的消息"
date:   2023-02-13 17:18:00 +0800
categories: tutorial
---

QQ 的消息, 其实是由很多部分构成的. 它可以包含文本, 图片, 或者是对一个成员的 `@` 等等. 这样消息中的一部分，我们称之为 “消息段“。



## 创建消息

在 EleCho.GoCqHttpSdk 中，CqMessage 表示一个一整个消息，CqXxxMsg表示一个消息段，例如 CqTextMsg。

你可以这样创建一个消息：

```csharp
CqMessage message = new CqMessage(new CqTextMsg("this is a simple text message"));
```

当然，如果你只是想创建一个简单的文本消息，也可以直接使用他的重载：

```csharp
CqMessage message = new CqMessage("this is a simple text message");
```



## CQ 码

CQ 码是从酷 Q 诞生的，表示 QQ 消息的格式。它的一个消息段是这样表示的：

```
[CQ:类型,参数名=参数值]
```

举个例子，一个表示 at 某人的消息段是这样的：

```
[CQ:at,qq=114514]
```

如果我要 at 某人，并在后面直接跟一段文字，那么只需要写消息内容即可：

```
[CQ:at,qq=114514] 你好呀
```

CqMessage 支持从 CQ 码创建一个消息实例：

```
CqMessage message =CqMessage.FromCqCode("[CQ:at,qq=114514] 你好呀");
```



## 上报类型

相信你已经注意到在 Go-CqHttp 的配置文件中，有一个上报类型的配置项。

他的值有两种，array 或 string. 这里的 array 指的是，Go-CqHttp 在向机器人程序发送上报数据的时候，消息使用 JSON 数组的格式，而 string 就是包含 CQ 码格式的字符串格式了。