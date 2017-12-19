
# Console Password Masker v1.0.0

![image1](https://raw.githubusercontent.com/mirzaevolution/ConsolePasswordMasker/master/Capture.PNG)

**Console Password Masker** is a lightweight library for **.NET Framework 4.0+** and **.NET Core 2.0** that handles password masking for console application. It works by replacing every char that users type into asterisk * or custom char that can be set by yourself. Not only masking chars into specified char replacement, it also supports char filtering that can filter every char that meets the given filter criteria. 

```csharp
char[] definedChars =
        {   '~','!','@','#','$','%','^','&','*','(',')','_','+','-','=',
            '`','1','2','3','4','5','6','7','8','9','0','Q','q','W','w','E','e',
            'R','r','T','t','Y','y','U','u','I','i','O','o',
            'P','p','[','{',']','}','A','a','S','s','D','d','F','f','G','g','H','h',
            'J','j','K','k','L','l',';',':','\'','\"','\\',
            'Z','z','X','x','C','c','V','v','B','b','N','n','M',
            'm','N',',','<','.','>','/','?',' '
        };
```

By default, it only filters common chars that users see on their keyboard (see code above) but you can define your own char filtering by using customChecker parameter that has *Func<ConsoleKeyInfo, bool>* data type.

Basically, this library only contains one method called **Mask** with many overloaded versions that you can use to customize your need.

Here are some samples on how to use this library.

```csharp
//for .NET Framework 4.0+
using ConsolePasswordMasker; 

//for .NET Core 2.0
using ConsolePasswordMasker.Core;

static PasswordMasker masker = new PasswordMasker();

```

1. **Sample #1**
```csharp
        static void Sample1()
        {
           //type your password here, hit enter to get the result
            string password = masker.Mask();
            //print the result here
            Console.WriteLine(password);
            
            //Login: *****
            //password => Hello
        }
```

2. **Sample #2**


```csharp
        static void Sample2()
        {
        	//when you specify useBeep to true, every char you input will have a beep.
            string password = masker.Mask(loginText: "Please Login: ", charMask: '#', useBeep: true);
            Console.WriteLine(password);
            
            //Please Login: #####
            //password => Hello
        }
```

3. **Sample #3**

```csharp
        static void Sample3()
        {
            ConsoleKeyData consoleKeyData = masker.Mask("Login: ", cancelOnEscape: true);
            if (consoleKeyData.IsCancelled)
                Console.WriteLine("Cancelled");
            else
                Console.WriteLine(consoleKeyData.Data);
        }
```


4. **Sample #4**

```csharp
        static void Sample4()
        {
        	//it'll only filter number from 0-9
            Func<ConsoleKeyInfo, bool> filter = (x) => char.IsDigit(x.KeyChar);
            
            ConsoleKeyData consoleKeyData = 
                      masker.Mask(loginText: "Please login:", 
                                  customChecker: filter, 
                                  charMask: '*', 
                                  useBeep: false, 
                                  cancelOnEscape: true);
                                  
            if (consoleKeyData.IsCancelled)
                Console.WriteLine("Cancelled");
            else
                Console.WriteLine(consoleKeyData.Data);
        }
```



Our project called [MGR-21 Process Killer](https://github.com/mirzaevolution/Mgr21-Process-Killer) uses this library too for the console version to handle set up master key and user login.


You can grab this library from nuget
## [Nuget.org](https://www.nuget.org/packages/ConsolePasswordMasker/1.0.0)

Or from Nuget package manager console

#### PM> Install-Package ConsolePasswordMasker -Version 1.0.0



Best Regards,


[Mirza Ghulam Rasyid](twitter.com/mirzaevolution)
