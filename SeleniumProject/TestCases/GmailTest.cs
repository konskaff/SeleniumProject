using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumProject.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.TestCases
{
    public class GmailTest : TestSetUp
    {
        [Test, Order(1)]
        public void LoginGmail()
        {
            //click to open gmail in order to add credentials
            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div/div[1]/div/div[1]/input")).Click();
            driver.FindElement(By.Id("identifierId")).SendKeys("konstantinakaffe93@gmail.com");
            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[1]/div/div/button")).Click();
            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div[1]/div/div/div/div/div[1]/div/div[1]/input")).SendKeys("123456789!K");
            //due to openQA.Selenium.StaleElementReferenceException: It means that either the element changed in the page, or element gets deleted.You should try again,
            bool staleElement = true;
            while (staleElement)
            {
                try
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[1]/div/div/button")).Click();
                    staleElement = false;
                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
            }
        }

        [Test, Order(2)]
        public void VerifyPrimarySelection()
        {
            IWebElement table = driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div[8]/div/div[1]/div[3]/div/table/tbody"));
            IList<IWebElement> tableRows = table.FindElements(By.TagName("tr"));
            string isSelected = driver.FindElement(By.Id(":1z")).GetAttribute("aria-selected");
            if (isSelected == "true")
            {
                string name = driver.FindElement(By.Id(":1z")).GetAttribute("aria-label");
                Assert.That(name, Is.EqualTo("Primary"));
                Console.WriteLine("The " + name + " is selected by default.");

            }
            else
            {
                driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div[5]/table/tbody/tr[1]/td[1]/div")).Click();
            }
        }

        [Test, Order(3)]
        public void TotalCountOfEmails()
        {
            IWebElement tableElement = driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div[8]/div/div[1]/div[3]/div/table/tbody"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            int emailCount = tableRow.Count;
            Console.WriteLine("The total number of emails in the Primary tab is " + emailCount + ".");
        }


        [Test, Order(4)]
        public void GetSenderAndSubjectOfNthEmail()
        {
            IWebElement table = driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div[8]/div/div[1]/div[3]/div/table/tbody"));
            IList<IWebElement> tableRows = table.FindElements(By.TagName("tr"));
            string divText = "";
            IList<IWebElement> allDivsInTableRow = tableRows[8].FindElements(By.TagName("div"));
            foreach (IWebElement div in allDivsInTableRow)
            {
                if (!string.IsNullOrEmpty(div.Text))
                {
                    divText = divText + div.Text + ",";
                }
            }
            List<string> emailInfo = divText.Split(',').ToList();
            Console.WriteLine("Sender Name: " + emailInfo[0] + "\n Subject: " + emailInfo[1]);
        }

        [Test, Order(5)]
        public void GetSenderAndSubjectOfAllEmails()
        {
            IWebElement table = driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div/div[2]/div[1]/div[2]/div/div/div/div/div[2]/div/div[1]/div/div/div[8]/div/div[1]/div[3]/div/table/tbody"));
            IList<IWebElement> tableRows = table.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRows)
            {
                string divText = "";
                IList<IWebElement> allDivsInTableRow = row.FindElements(By.TagName("div"));
                foreach (IWebElement div in allDivsInTableRow)
                {
                    if (!string.IsNullOrEmpty(div.Text))
                    {
                        divText = divText + div.Text + ",";
                    }
                }
                List<string> emailInfo = divText.Split(',').ToList();
                Console.WriteLine("Sender Name: " + emailInfo[0] + "\n Subject: " + emailInfo[1]);
            }
        }
    }
}
