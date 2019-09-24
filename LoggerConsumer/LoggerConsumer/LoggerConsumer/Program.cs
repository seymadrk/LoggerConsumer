using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoggerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseFolder = AppDomain.CurrentDomain.BaseDirectory;

            // dynamically load assembly from file Test.dll
            Assembly testAssembly = Assembly.LoadFile(@"" + baseFolder + "LoggerLibrary.dll");

            // get type of class Calculator from just loaded assembly
            Type lType = testAssembly.GetType("LoggerLibrary.ErrorModel");

            // get type of class Calculator from just loaded assembly
            Type loggerClassType = testAssembly.GetType("LoggerLibrary.Logger");

            // create instance of class Calculator
            object typeInstance = Activator.CreateInstance(lType);
            object loggerInstance = Activator.CreateInstance(loggerClassType);

            // get info about property: public double Number
            PropertyInfo messagePropertyInfo = lType.GetProperty("Message");
            PropertyInfo typePropertyInfo = lType.GetProperty("Type");

            // set value of property: public double Number
            messagePropertyInfo.SetValue(typeInstance, "Şeymanın programı hata yaptı", null);
            typePropertyInfo.SetValue(typeInstance, "Kullanıcı hatası", null);

            // get value of property: public double Number
            string valueMessage = (string)messagePropertyInfo.GetValue(typeInstance, null);
            string valueType = (string)typePropertyInfo.GetValue(typeInstance, null);

            MethodInfo methodInfo = loggerClassType.GetMethod("Log");
            ParameterInfo[] parameters = methodInfo.GetParameters();

            if (parameters.Length == 0)
            {
                // This works fine
                var result = methodInfo.Invoke(loggerInstance, null);
            }
            else
            {
                var result = methodInfo.Invoke(loggerInstance, new object[] { typeInstance });
                Console.WriteLine(result);
                Console.WriteLine(valueMessage + "\n" + valueType);
            }




            Console.Read();
        }
    }
}