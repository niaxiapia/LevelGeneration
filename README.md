# LevelGeneration
之前有人问到MVVM在Revit二开中怎么玩，刚好手头的项目里有一个简单的例子拿出来和大家分享一下。
自己测试过，应该可以直接拿去测试。（功能就别提了啊，只是为了给大家演示一下如何使用MVVM模式而已）

其中，View:WindowLevelGeneration.xaml

ViewModel:LevelGenerationViewModel.cs

程序入口：LevelGeneration.cs

文件夹Converter和Validation为WPF中用到的转换及有效验证类。

文件夹Resources中是WPF用到的资源字典。

文件夹Utils中的是两个工具类，为整个项目提供便利搜索。

DialogCloser.cs为WPF数据绑定用到的类，可以控制窗口的关闭（绑定必须绑定到依赖属性）
