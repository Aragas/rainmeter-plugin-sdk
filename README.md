Decided to do a lot more complex plugin system.  
For each **Rainmeter Skin** a **RainmeterSkinHandler** class is created.  
Each **RainmeterSkinHandler** contains **PluginSkin**'s from different **Plugin Assemblies**(.dll)  
Each **PluginSkin** contains **PluginMeasure**'s  
  
This structure system allows to write code without any static elements like before. Each **PluginSkin** will be unique for each **Rainmeter Skin**.  
That's one thing that always bothered me in Rainmeter SDK. You couldn't use one .dll for multiple **Rainmeter Skin**'s without having some troubles and some static's.  
Now, for example, I can make an Audio Player Plugin and use it for multiple **Rainmeter Skin**'s. There isn't really any point in multiple Audio Player's, but you got the idea.  
This system isn't one of the most beautiful things I created, but it will work.  
  
  
The architecture isn't really that bad. As a developer, you should know only couple of things.  
* Each **PluginSkin** is unique for each **Rainmeter Skin**. This means, most of your Plugin logic should be located there.  
* **PluginMeasure** is created only once for every **Rainmeter Measure** in your **Rainmeter Skin**. In theory, it should countain only return XXX logic as seen in examples.  
* Even if you don't really need a **PluginSkin**, it should be created.
* **Naming** is important. Your **PluginSkin** and **PluginMeasure** should share same names and should end with "...Skin" and "...Measure" (ExampleSkin and ExampleMeasure). **Enum** naming is not important.  
   
   
The **Rainmeter Meter** syntaxis changed a bit. Now you need some extra field: 
* **PluginAssemblyName** your Plugin assembly name (.dll)
* **PluginMeasureName** your **PluginSkin**/**PluginMeasure** name.
* **PluginMeasureType** enum field name that will be used by **Rainmeter Meter**.
   
   
And one thing that you should know. I'm sorry for this confusing documentation. See these examples or my [VKPlayer 2.0 Plugin](https://github.com/Aragas/VKPlayer-2.0). Good luck.
