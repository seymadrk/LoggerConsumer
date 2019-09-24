using ExampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ExampleApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region ClassLibrary yi çalıştırıyoruz ve bize değer dönüyor
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

            var result = methodInfo.Invoke(loggerInstance, new object[] { typeInstance });
            #endregion

            return View(new ResultModel() { Text = (string)result });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}