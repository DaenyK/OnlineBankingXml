using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OnlineBanking.Admin.Lib.Model;
using OnlineBanking.Customer.Lib.Model;

namespace OnlineBanking
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceXmlDocument service = new ServiceXmlDocument(@"\\dc\Студенты\ПКО\SMP-172.1\dana.xml");

            //Operator oper = new Operator();
            //oper.Name = "kcell";
            //oper.Percent = 2;
            //oper.Logo = "фиолетовое что-то";
            //Prefix pref = new Prefix();
            //pref.pref = 777;

            //oper.Prefixes.Add(pref);
            //try
            //{
            //    service.CreateOperator(oper);
            //    Console.WriteLine("оператор успешно добавлен");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            ServiceXmlDocument service = new ServiceXmlDocument(@"\\dc\Студенты\ПКО\SMP-172.1\ДК");

            try
            {
                User user = new User();
                user.Email = "sobaka@dh.com";
                user.Login = "loala54";
                user.Password = "4156";

                service.CreateUser(user);
                Console.WriteLine("пользователь успешно добавлен\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
