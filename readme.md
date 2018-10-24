<h1>一个Unity行为树框架。</h1>
带了两个例子，一个简单的例子，类似hello world。</br>
一个打boss的例子，这个例子也是仓促写完，模块的解耦度不是特别高，玩家用状态机可能更顺手一些，但是还是用了行为树。</br>

<h2>设计目的：</h2>
1.避免反射</br>
2.不用每次都从根节点运行。</br>
3.可以xml解析</br>

<h2>简单介绍：</h2>
Agent是BlackBoard的实现，用于数据共享，以及继承实现树与外部的代理。</br>
Genaral用来生成行为树的：所有代码用BehaviourCodeGenaral的接口来生成【统一管理】。BehaviourXmlGenaral生成xml的行为树对象。</br>
Node是节点对象类型</br>
用BehaviourTree来管理一棵树</br>
BTG是全局函数</br>
BTMapping用来注册所有类型的，避免反射。</br>

<h2>扩展</h2>
1.对象缓存（暂时未实现，具体扩展可以参考BTG，以及BehaviourPoolNode）</br>
2.前置条件以及后置条件，未实现，可以根据代码扩展(例子里面的打断用的是在行为里面写死的)</br>
3.未实现子树的概念，也可以在框架基础上进行扩展</br>
4.未实现工具化，暂时是手动填写xml</br>