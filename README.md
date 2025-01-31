<div align="center">
<img src="logo.png" width="200"/>

# EleCho.GoCqHttpSdk

_✨ 专为 [Go-CqHttp](https://github.com/Mrs4s/go-cqhttp) 打造的, 便捷与优雅的通信 SDK ✨_

![LICENSE](https://img.shields.io/github/license/EleChoNet/EleCho.GoCqHttpSdk) 
![nuget](https://img.shields.io/nuget/vpre/EleCho.GoCqHttpSdk)

</div>

## 📖 简介:

虽然有很多的 OneBot 通信 SDK, 但没有一个是专为 `go-cqhttp` 打造的 .NET SDK. 秉持着 C# 的优雅开发理念, 这个库诞生了.

用户可以享受到完全遵守 C# 编码风格, 高度封装的各种接口, 以及优化过命名的接口, 事件, 数据成员, 枚举类型等.

> 如果你不了解 `go-cqhttp`, 可以从这里了解一下: [go-cqhttp 文档](https://docs.go-cqhttp.org/) / [go-cqhttp 仓库](https://github.com/Mrs4s/go-cqhttp)

## 🚀 使用:

你可以在 nuget.org 下载到本库的发布包, 也可以直接在 Visual Studio 中为项目添加引用.

> 通信协议支持: 正反向 HTTP 与正反向 WebSocket. \
> 上报格式支持: `array(json)`, `string`. \
> 功能支持: CqCode 转码, API 快速操作 \
> 设计模式: 上报为中间件模式, 同时也支持基于中间件的插件

### 🔗 连接

要与 go-cqhttp 建立一个 WebSocket 连接, 需要使用位于 `EleCho.GoCqHttpSdk` 命名空间下的 `CqWsSession` 来创建一个会话

```csharp
// 初始化一个 CqWsSession 用来与 go-cqhttp
CqWsSession session = new CqWsSession(new CqWsSessionOptions()
{
    BaseUri = new Uri("ws://127.0.0.1:6700"),  // WebSocket 地址
    UseApiEndPoint = true,                     // 使用 api 终结点
    UseEventEndPoint = true,                   // 使用事件终结点
});

session.Start();                               // 开始连接 (你也可以使用它的异步版本)
```

> 指定 *UseApiEndPoint* 和 *UseEventEndPoint* 将使用独立的 api 和 event 套接字来单独处理功能调用以及事件处理
> 参考文档: [Onebot11:正向WebSocket](https://github.com/botuniverse/onebot-11/blob/master/communication/ws.md)

### 📩 上报

上报数据也就是所谓的 "事件", 所有继承了 `EleCho.GoCqHttpSdk.ICqPostSession` 接口的类都将处理上报数据, 该接口规定必须要有一个名为 *PostPipeline* 的 `CqPostPipeline` 成员

`CqPostPipeline` 是用户处理上报的途径, 它符合中间件设计模型, 你可以直接使用使用它添加中间件.

```csharp
CqWsSession session;   // 要处理上报数据的会话
session.PostPipeline.Use(async (context, next) =>
{
    // context 为上报数据的上下文, 其中包含了具体的信息
    
    // 在这里添加你的逻辑代码 //
    
    // next 是中间件管道中的下一个中间件, 
    // 如果你希望当中间件执行时, 不继续执行下一个中间件
    // 可以选择不执行 next
    await next();
});
```

上述订阅方法将会处理所有的上报, 我们更推荐使用 `EleCho.GoCqHttpSdk.CqPostContextExtensions` 类所提供的拓展方法, 通过它你可以非常便捷的处理任何具体类型的事件

```csharp
CqWsSession session;   // 要处理上报数据的会话
session.PostPipeline.UseGroupMessage(async (context, next) =>
{
    // context 为 CqGroupMessagePostContext, 其中包含了群聊消息的具体信息
    
    // 在这里添加你的逻辑代码 //
    
    // 简单实现一个复读机:
    if (context.RawMessage.StartsWith("echo "))
    {
        string msg = context.RawMessage.SubString(5);                       // 获取 "echo " 后的字符
        context.SendGroupMessageAsync(context.GroupId, new CqMessage(msg)); // 发送它 (关于消息发送后面会详细讲解)
    }
    
    await next();
});
```

### 📝 消息发送

所有继承了 `EleCho.GoCqHttpSdk.ICqActionSession` 接口的类都将具备使用 `Action` 的能力, 消息发送属于 `Action`, 该接口规定必须有一个名为 *ActionSender* 的 `CqActionSender` 成员

`CqActionSender` 是程序向 go-cqhttp 发送 "Action" 的途径, 其中需要实现 `CqAction` 的发送逻辑以及响应逻辑, 你可以直接使用它来调用任何 `CqAction`

```csharp
CqWsSession session;   // 要使用 Action 的会话
session.ActionSender.SendActionAsync(new CqSendGroupMessageAction(群聊ID, new CqMessage { new CqTextMsg("一个文本消息") }));
```

可以看到, 使用 *session.ActionSender* 直接发送 `Action` 的步骤比较繁琐, 所以同样的, 推荐使用拓展方法, 它们由 `EleCho.GoCqHttpSdk.CqActionSessionExtensions` 提供.

```csharp
CqWsSession session;   // 要使用 Action 的会话
context.SendGroupMessageAsync(群聊ID, new CqMessage("一个文本消息")); // 发送它 (关于消息发送后面会详细讲解)
```

> `EleCho.GoCqHttpSdk.CqActionSessionExtensions` 类不直接为 `CqActionSender` 类提供拓展, 你只能在实现了 `ICqActionSession` 接口的类上调用这些拓展方法

### 📦 使用插件

在本库中, 你可以为能够进行上报的会话添加插件, 它本质还是一个中间件, 但是插件中, 它分离了所有类型的上报. 如果要处理某种类型的上报, 只需要 override 对应的方法即可.

```csharp
class MyPostPlugin : CqPostPlugin
{
    public override async Task OnGroupMessageAsync(CqGroupMessagePostContext context)
    {
        if (context.Session is not ICqActionSession actionSession)   // 判断是否能够发送 Action
            return;
        
        string text = context.Message.GetText();
        if (text.StartsWith("TTS:", StringComparison.OrdinalIgnoreCase))
        {
            await actionSession.SendGroupMessageAsync(context.GroupId, new CqTtsMsg(text[4..]));
        }
        else if (text.StartsWith("ToFace:"))
        {
            if (CqFaceMsg.FromName(text[7..]) is CqFaceMsg face)
            
            await actionSession.SendGroupMessageAsync(context.GroupId, face);
        }
    }

    public override async Task OnGroupMessageRecalledAsync(CqGroupMessageRecalledPostContext context)
    {
        if (context.Session is not ICqActionSession actionSession)   // 判断是否能够发送 Action
            return;

        var msg = (await actionSession.GetMessageAsync(context.MessageId));

        await actionSession.SendGroupMessageAsync(context.GroupId, CqMsg.Chain("让我康康你撤回了什么: ", msg.Message));
    }
}
```

它的使用也非常简单, 只需要在会话上调用 `UsePlugin` 方法即可

```csharp
session.UsePlugin(new MyPostPlugin());
```

> 与 ICqPostSession 的拓展方法 Use 不同, 一个插件拥有处理多种类型上报的能力, 但它本质是单个中间件, 而诸如 UseGroupMessage 这种拓展方法, 在使用的时候, 会创建一个新的中间件并添加到上报处理管线.

### 🪁 消息匹配

使用 `EleCho.GoCqHttpSdk.MessageMatching`, 你可以轻松实现对消息的正则匹配. 首先, 其提供的最基本的拓展方法如下:

```csharp
CqWsSession session;   // 需要添加处理中间件的会话

// 匹配开头是 `echo` 和空格的消息
session.UseGroupMessageMatch("$echo ", async (context, next) =>
{
    // 发送复读消息
    await session.SendGroupMessage(context.GroupId, context.Message.GetText()[5..];
});
```

当然, `MessageMatching` 还提供了更高级的功能, 它能正则中的分组数据, 自动传递到你的方法中, 供你使用. 这个功能由 `MessageMatching` 的拓展插件 `CqMessageMatchPostPlugin` 提供.

```csharp
// 继承 CqMessageMatchPostPlugin 以使用拓展消息匹配功能
public class MyMessageMatchPlugin : CqMessageMatchPostPlugin
{
    public MyMessageMatchPlugin(ICqActionSession actionSession)
    {
        ActionSession = actionSession;
    }

    public ICqActionSession ActionSession { get; }

    // 在插件类中, 为你的方法指定 CqMessageMatch 特性以处理消息
    // 通过 CqMessageMatch 来指定匹配规则 (例如这里非贪婪匹配两个中括号之间的任意内容, 并命名为 content 组)
    [CqMessageMatch(@"\[(?<content>.*?)\]")]
    public async Task MyMessageMatchPluginMethod(
        CqGroupMessagePostContext context,        // 在参数中指定一个合适的 CqMessagePostContext 用来接收消息上报数据
                                                  // 它可以是 CqMessagePostContext, CqPrivateMessagePostContext, CqGroupMessagePostContext
        Match match,                              // 如果你指定了一个 Match 类型的参数, 正则匹配返回的 Match 会被传入
        string content                            // 如果你指定了字符串类型的参数, 则会自动从正则的 Groups 中取值, 并传入
    )
    {
        // 将接收到的内容所匹配到的 context 值发送到消息所在群组
        await ActionSession.SendGroupMessageAsync(context.GroupId, $"Captured content: {content}, index: {match.Index}");
        
        // 如果当前方法的返回值是一个 Task, 那么这个 Task 会被等待, 如果你不希望它被等待, 你可以指定 void 作为返回值
    }

    /// 这里匹配所有消息并打印到控制台
    [CqMessageMatch(@"")]
    public void LogAllMessages()
    {
        // 即便你不在参数中指定 CqMessagePostContext, 你也可以通过插件的公开属性来获取当前上下文
        // 需要注意的是, 如果没有特意指定是群聊消息上下文或私聊消息上下文, 插件会处理任何消息

        Console.WriteLine(CurrentContext.Message.GetText());
    }
}
```

> 另外, `MessageMatching` 也提供了很多重载, 你可以选择适合你的使用



### 📎 小提示

1. `CqFaceMsg` 是 QQ 小黄脸消息, 它还提供了从中文名称转换为对应类型的方法, 例如 "斜眼笑", "可怜" 等中文名称.
2. 上报中的 `QuickOperation` 是不推荐使用的, 除非你使用的是反向 HTTP, 这是因为在 WebSocket 中, 快速操作是模拟出来的

### 📃 近期计划:

1. 实现反向 WebSocket 通信
2. 实现所有 Action

## 🧬 项目

### 💼 关于数据结构

因为 `go-cqhttp` 给的数据, JSON 都是小驼峰, 并且为了用户操作上的便捷, 所以 JSON 解析上使用了以下方法:

1. 分为用户的操作类和具体调用时使用的 Model 类
2. 在调用接口, 或者解析上报的时候, 两种类会相互转换
3. 一些原始 Model 类中的 `data` 字段, 或者 `params` 字段, 他们在用户的操作类中直接作为类型成员存在, 而不独立分出一个 `data` 或 `params` 成员存放.

同时, 为了用户操作的便捷, 用户所操作的类与实际传输使用的类, 字段格式是不一样的, 例如在 Music 消息中 sub_type 表示该 Music 消息的音乐类型, 于是在用户的操作类中, 它使用 MusicType 命名.

#### 📄 消息

首先是 `go-cqhttp` 中的基础消息类型, 也就是 CQ 码(CQ Code):

它的 JSON 格式是这样的:

```json
{
    "type": "消息类型",
    "data": {
        // 消息的数据
    }
}
```

如果让用户访问 data 然后访问它的成员, 肯定有些繁琐, 所以在用户操作的类中, 是这样的:
```csharp
public class CqXxxMsg : CqMsg
{
    public override string Type => "消息类型";  // Type 是不允许用户修改的, 一个类型对应一个 Type
    
    // 直接将消息数据作为消息的成员
}
```

#### 📥 上报

上报的原始数据 JSON 格式中, 并没有专门为数据抽出一个 data 字段, 所以不做特殊处理.

#### ✋ Action

Action 在 go-cqhttp 中的 JSON 格式与消息类似, 它为参数抽出了一个 params 字段, 然后将所有参数放在这个字段中. 所以在这方面, 做了与消息类型近似的处理, 也就是直接将参数独立出来, 而不是放在 params 字段中.

同样, ActionResult(Action 调用的返回结果) 也将数据放在了 data 字段中, 所以同样做了特殊处理.

## 🎉 贡献

关于任何对项目上的意见, 例如命名, 设计模式, 或者其他任何方面的问题, 直接提交一个 discussion 就可以啦, 然后咱们就可以讨论讨论具体要采取什么措施啦. ψ(｀∇´)ψ

如果你有什么好的想法, 也可以直接提交一个 PR, 我们一起来完善这个项目吧!

> 球球了, 有问题请直接提出来, 不要犹豫, 咱真的很需要用户意见, 尤其是如何提升这个库的 "优雅程度".