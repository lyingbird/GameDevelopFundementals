# Game Develop Fundamentals
第二次作业

选作状态机，采用c#在Unity中实现，将目前的状态通过UI打印到屏幕上，如下图所示：

![image-20201001224626760](C:\Users\yanle\AppData\Roaming\Typora\typora-user-images\image-20201001224626760.png)

一共有四种状态：Sleep,Study,Play,Eat.每种状态的转换如图所示：


|  状态   | 可转换到的状态  |   | |
|  ----  | ----  |
| Sleep  | Study |
| Study  | Play | Eat |
| Play   | Sleep | Study |
| eat    | Study | play | sleep |

根据提示，按下不同按键即可完成状态转换。

状态机的实现参考了untiy的官方wiki:

http://wiki.unity3d.com/index.php?title=Finite_State_Machine#C.23_-_FSMSystem.cs