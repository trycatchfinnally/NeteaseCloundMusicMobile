网易云音乐的blazor实现
============

前端使用blazor的实现，所有接口来自于 https://hub.fastgit.org/Binaryify/NeteaseCloudMusicApi.git 
如果你也是一位.net开发人员，并且对前端感兴趣，欢迎提交你的代码
github地址：https://github.com/trycatchfinnally/NeteaseCloundMusicMobile
gitee地址：https://gitee.com/ngserve/neteaseclound-music-mobile.git

## 演示地址
http://home.tangkh.top:63311

## 项目结构
### NeteaseCloundMusicMobile.Server
用作服务器端，在代理blazor应用，并且通过consule转发接口，使接口与客户端同域。核心代码位于ProxyHttpRequestMiddleware.cs中
### NeteaseCloundMusicMobile.Client
blazor客户端部分代码，实现页面数据的绑定，使用flex布局


