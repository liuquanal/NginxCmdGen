# NginxCmdGen
Nginx的windows平台常用命令（如启动、停止、注册服务、移除服务等）自动生成，也支持其它类似的命令生成。如icecast、ffmpeg、node、java等windows平台命令行工具的各种常用命令生成。

# 使用方法
将NginxCmdGen.exe拷贝至应用程序运行目录，运行并填写相关参数，点击生成即可。有以下几个参数：
- 镜像名称：应用程序的进程名称，不含扩展名
- 服务名称：应用程序注册为系统服务时的服务名
- 启动目录：应用程序的运行目录
- 镜像目录：相对于启动目录的镜像相对路径，如bin目录。
- 启动参数：启动时，可以追加一些命令参数
- 使用通用命令模式：将不生成Nginx特有的命令，只生成通用的启动、停止、注册服务等命令。
- 崩溃时自动重启：对于有些需要崩溃重启的程序，可以设置此项。注意：适用于程序报错对系统无伤害的情况。若程序崩溃对系统有严重伤害（如弹出错误对话框，占用系统端口、连接数等），切记不要开启。

# Nginx的各种命令
针对Nginx的windows版本，将生成以下文件：
- WinSrvD/WinSrvD.exe
- WinSrvD/ReadMe.txt
- 1.1-startup.bat
- 1.2-stop.bat
- 1.3-check-statee.bat
- 1.4-service-install.bat
- 1.5-service-remove.bat
- 1.6-reload-cfg.bat
- 1.7-reopen-log.bat
- nginx-service.exe

# 通用命令生成
通用命令模式下，生成的命令脚本将不包含以下两个脚本：
- 1.6-reload-cfg.bat
- 1.7-reopen-log.bat

# 崩溃时自动重启
当选择崩溃自动重启，则1.1和1.2两个命令会被替换为以下两个命令：
- 1.1-startup-nohup.bat
- 1.2-stop-nohup.bat

# 程序截图
- 生成nginx各种命令:
![image](https://raw.githubusercontent.com/liuquanal/NginxCmdGen/master/NginxCmdGen/screenshot/screenshot01.png)
- 生成icecast的各种命令：（由于icecast不是很稳定，偶尔会崩溃，崩溃时需要重新启动，所以使用nohup模式，并且使用通用命令模式）
![image](https://raw.githubusercontent.com/liuquanal/NginxCmdGen/master/NginxCmdGen/screenshot/screenshot02.png)
# 版本记录
- 1.0.0.0 首次发布
- 编译工具：VS2010，.net framework版本：2.0
