using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EntityCache.Bussines;
using OpenQA.Selenium;
using PacketParser.Services;
using Cookie = OpenQA.Selenium.Cookie;

namespace Shop_Windows.Classes
{
    public class DivarAdv
    {
        private IWebDriver _driver;


        private static DivarAdv _me;
        public CancellationTokenSource TokenSource;
        public SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);

        private DivarAdv()
        {
            // if (string.IsNullOrEmpty(_advRootPath)) _advRootPath = ConfigurationManager.AppSettings.Get("RootPath");
        }

        public static async Task<DivarAdv> GetInstance()
        {
            return _me ?? (_me = new DivarAdv());
        }

        #region MyRegion

        private List<string> lstMessage = new List<string>();

        public async Task<bool> Login(long simCardNumber)
        {
            try
            {

                _driver = Utility.RefreshDriver(_driver);
                if (!_driver.Url.Contains("divar.ir"))
                    _driver.Navigate().GoToUrl("https://divar.ir");

                var simBusiness = await SimcardBussines.GetAsync(simCardNumber);
                var tokenInDatabase = simBusiness?.DivarToken ?? null;

                var listLinkItems = _driver.FindElements(By.TagName("a"));
                var isLogined = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                //اگر کاربر لاگین شده فعلی همان کاربر مورد نظر است نیازی به لاگین نیست 
                if (isLogined && !string.IsNullOrEmpty(tokenInDatabase))
                {
                    var currentTokenOnDivar = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                    if (!string.IsNullOrEmpty(currentTokenOnDivar) && currentTokenOnDivar == tokenInDatabase)
                        return true;
                }

                //اگر کاربرلاگین شده کاربر مد نظر ما نیست از آن کاربری خارج می شود
                if (isLogined)
                {
                    _driver.Manage().Cookies.DeleteCookieNamed("_gat");
                    _driver.Manage().Cookies.DeleteCookieNamed("token");
                }

                //در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                if (!string.IsNullOrEmpty(tokenInDatabase))
                {
                    var token = new Cookie("token", tokenInDatabase);
                    _driver.Manage().Cookies.AddCookie(token);
                    _driver.Navigate().Refresh();
                }
                //اگر قبلا توکن نداشته وارد صفحه دریافت کد تائید لاگین می شود 
                else
                {
                    _driver.Navigate().GoToUrl("https://divar.ir/my-divar/my-posts");
                    //کلیک روی دکمه ورود و ثبت نام
                    await Utility.Wait();
                    _driver.FindElement(By.ClassName("login-message__login-btn")).Click();
                    await Utility.Wait();
                    var currentWindow = _driver.CurrentWindowHandle;
                    _driver.SwitchTo().Window(currentWindow);
                    if (_driver.FindElements(By.Name("mobile")).Count > 0)
                        _driver.FindElement(By.Name("mobile")).SendKeys("0" + simCardNumber);
                }

                //انتظار برای لاگین شدن
                int repeat = 0;
                //حدود 120 ثانیه فرصت لاگین دارد
                while (repeat < 20)
                {

                    //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود
                    listLinkItems = _driver.FindElements(By.TagName("a"));
                    if (listLinkItems.Count < 5) return false;

                    var isLogin = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                    if (isLogin)
                    {
                        //var all = _driver.Manage().Cookies.AllCookies.ToList();
                        tokenInDatabase = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                        if (simBusiness is null)
                            simBusiness = new SimcardBussines() { Guid = Guid.NewGuid() };

                        simBusiness.DivarToken = tokenInDatabase;
                        simBusiness.Modified = DateTime.Now;
                        simBusiness.Number = simCardNumber;

                        await simBusiness.SaveAsync();

                        ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('لاگین انجام شد');");
                        await Utility.Wait();
                        _driver.SwitchTo().Alert().Accept();
                        return true;
                    }

                    var name = await SimcardBussines.GetAsync(simCardNumber);
                    var message = $@"مالک: {name.OwnerName} \r\nشماره: {name.Number}  \r\nلطفا لاگین نمائید ";
                    ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");

                    await Utility.Wait(3);
                    try
                    {
                        _driver.SwitchTo().Alert().Accept();
                        await Utility.Wait(3);
                        repeat++;
                    }
                    catch
                    {
                        await Utility.Wait(10);
                    }
                }
                return false;
            }
            catch (WebException) { return false; }
            catch (Exception ex)
            {
                if (ex.Source != "WebDriver")
                    WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }
        }
        public async Task<bool> LoginChat(long simCardNumber, bool isFromSimcard)
        {
            try
            {
                if (isFromSimcard)
                {
                    _driver = Utility.RefreshDriver(_driver);
                    if (!_driver.Url.Contains("https://chat.divar.ir/"))
                        _driver.Navigate().GoToUrl("https://chat.divar.ir/");

                    var sim = await SimcardBussines.GetAsync(simCardNumber);
                    var tokenInDatabase = sim?.ChatToken ?? null;

                    var listLinkItems = _driver.FindElements(By.TagName("a"));
                    var isLogined = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                    //اگر کاربر لاگین شده فعلی همان کاربر مورد نظر است نیازی به لاگین نیست 
                    if (isLogined && !string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var currentTokenOnDivar = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                        await Utility.Wait(1);
                        var aouth = _driver.FindElements(By.ClassName("auth__body__text")).Any();
                        if (aouth)
                            _driver.FindElement(By.TagName("input")).SendKeys(sim.OwnerName + '\n');

                        if (!string.IsNullOrEmpty(currentTokenOnDivar) && currentTokenOnDivar == tokenInDatabase)
                            return true;
                    }

                    //اگر کاربرلاگین شده کاربر مد نظر ما نیست از آن کاربری خارج می شود
                    if (isLogined)
                    {
                        _driver.Manage().Cookies.DeleteCookieNamed("_gat");
                        _driver.Manage().Cookies.DeleteCookieNamed("token");
                    }

                    //در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                    if (!string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var token = new Cookie("token", tokenInDatabase);
                        _driver.Manage().Cookies.AddCookie(token);
                        await Utility.Wait();
                        _driver.Navigate().Refresh();
                        await Utility.Wait();
                    }
                    //اگر قبلا توکن نداشته وارد صفحه دریافت کد تائید لاگین می شود 
                    else
                    {
                        _driver.Navigate().GoToUrl("https://chat.divar.ir/");
                        //کلیک روی دکمه ورود و ثبت نام
                        await Utility.Wait();
                        var currentWindow = _driver.CurrentWindowHandle;
                        _driver.SwitchTo().Window(currentWindow);
                        await Utility.Wait(1);
                        if (_driver.FindElements(By.TagName("input")).Count > 0)
                            _driver.FindElements(By.TagName("input")).FirstOrDefault()
                                ?.SendKeys("0" + simCardNumber + "\n");
                    }

                    var invalid = _driver.FindElements(By.TagName("p")).Any(q => q.Text == "invalid_token");
                    if (invalid)
                    {
                        _driver.Navigate().GoToUrl("https://chat.divar.ir/");
                        //کلیک روی دکمه ورود و ثبت نام
                        await Utility.Wait();
                        var currentWindow = _driver.CurrentWindowHandle;
                        _driver.SwitchTo().Window(currentWindow);
                        if (_driver.FindElements(By.TagName("input")).Count > 0)
                            _driver.FindElements(By.TagName("input")).FirstOrDefault()
                                ?.SendKeys("0" + simCardNumber + "\n");
                    }

                    //انتظار برای لاگین شدن
                    var repeat = 0;
                    //حدود 120 ثانیه فرصت لاگین دارد
                    while (repeat < 20)
                    {

                        //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود
                        listLinkItems = _driver.FindElements(By.TagName("a"));
                        if (listLinkItems.Count < 5) return false;

                        var isLogin = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                        if (isLogin)
                        {
                            //var all = _driver.Manage().Cookies.AllCookies.ToList();
                            tokenInDatabase = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                            if (sim is null)
                            {
                                sim = new SimcardBussines() { Guid = Guid.NewGuid() };
                            }

                            sim.ChatToken = tokenInDatabase;
                            sim.Modified = DateTime.Now;
                            sim.Number = simCardNumber;

                            await sim.SaveAsync();


                            await Utility.Wait(1);

                            var aouth = _driver.FindElements(By.ClassName("auth__body__text")).Any();
                            if (aouth)
                                _driver.FindElement(By.TagName("input")).SendKeys(sim.OwnerName + '\n');

                            ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('لاگین انجام شد');");
                            await Utility.Wait();
                            _driver.SwitchTo().Alert().Accept();


                            return true;
                        }
                        else
                        {

                            var message = $@"مالک: {sim.OwnerName} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                            ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");

                            await Utility.Wait(5);
                            try
                            {
                                _driver.SwitchTo().Alert().Accept();
                                await Utility.Wait(5);
                                repeat++;
                            }
                            catch
                            {
                                await Utility.Wait(10);
                            }
                        }
                    }
                }
                else
                {
                    _driver = Utility.RefreshDriver(_driver);
                    if (!_driver.Url.Contains("https://chat.divar.ir/"))
                        _driver.Navigate().GoToUrl("https://chat.divar.ir/");

                    var simBusiness = await SimcardBussines.GetAsync(simCardNumber);
                    var tokenInDatabase = simBusiness?.ChatToken ?? null;

                    var listLinkItems = _driver.FindElements(By.TagName("a"));
                    var isLogined = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                    //اگر کاربر لاگین شده فعلی همان کاربر مورد نظر است نیازی به لاگین نیست 
                    if (isLogined && !string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var currentTokenOnDivar = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                        await Utility.Wait(1);
                        var aouth = _driver.FindElements(By.ClassName("auth__body__text")).Any();
                        if (aouth)
                            _driver.FindElement(By.TagName("input")).SendKeys(simBusiness.OwnerName + '\n');

                        if (!string.IsNullOrEmpty(currentTokenOnDivar) && currentTokenOnDivar == tokenInDatabase)
                            return true;
                    }

                    //اگر کاربرلاگین شده کاربر مد نظر ما نیست از آن کاربری خارج می شود
                    if (isLogined)
                    {
                        _driver.Manage().Cookies.DeleteCookieNamed("_gat");
                        _driver.Manage().Cookies.DeleteCookieNamed("token");
                    }

                    //در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                    if (!string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var token = new Cookie("token", tokenInDatabase);
                        _driver.Manage().Cookies.AddCookie(token);
                        await Utility.Wait();
                        _driver.Navigate().Refresh();
                        await Utility.Wait();
                    }
                    //انتظار برای لاگین شدن
                    var repeat = 0;
                    //حدود 120 ثانیه فرصت لاگین دارد
                    while (repeat < 3)
                    {

                        //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود
                        listLinkItems = _driver.FindElements(By.TagName("a"));
                        if (listLinkItems.Count < 5) return false;

                        var isLogin = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                        if (isLogin)
                        {
                            //var all = _driver.Manage().Cookies.AllCookies.ToList();
                            tokenInDatabase = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                            if (simBusiness is null)
                            {
                                simBusiness = new SimcardBussines() { Guid = Guid.NewGuid() };
                            }

                            simBusiness.ChatToken = tokenInDatabase;
                            simBusiness.Modified = DateTime.Now;
                            simBusiness.Number = simCardNumber;

                            await simBusiness.SaveAsync();


                            await Utility.Wait(1);

                            var aouth = _driver.FindElements(By.ClassName("auth__body__text")).Any();
                            if (aouth)
                                _driver.FindElement(By.TagName("input")).SendKeys(simBusiness.OwnerName + '\n');

                            ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('لاگین انجام شد');");
                            await Utility.Wait();
                            _driver.SwitchTo().Alert().Accept();


                            return true;
                        }
                        else
                        {

                            var message = $@"مالک: {simBusiness.OwnerName} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                            ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");

                            await Utility.Wait(3);
                            try
                            {
                                _driver.SwitchTo().Alert().Accept();
                                await Utility.Wait(3);
                                repeat++;
                            }
                            catch
                            {
                                await Utility.Wait(10);
                            }
                        }
                    }
                }

                return false;
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception ex)
            {
                if (ex.Source != "WebDriver")
                    WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }

        }
        public async Task<bool> GetNumbersFromDivar(int Counter)
        {
            //var list = new List<string>();
            //var path = Path.Combine(Application.StartupPath, "divarNumbers.txt");
            //try
            //{
            //    for (var i = 0; i < Counter; i++)
            //    {
            //        var f = await DivarCityBussines.GetAllAsync();
            //        var rnd = new Random().Next(0, f.Count);
            //        var city = f[rnd].Name;
            //        var number = SimcardBussines.GetAsync(AdvertiseType.Divar);
            //        var tt = AdvTokensBussines.GetToken(number.Number, AdvertiseType.Divar);
            //        var hasToken = tt?.Token ?? null;
            //        if (string.IsNullOrEmpty(hasToken))
            //        { File.WriteAllLines(path, list); return false; }
            //        var log = await Login(number.Number);
            //        if (!log)
            //        { File.WriteAllLines(path, list); return false; }
            //        _driver.Navigate().GoToUrl("https://divar.ir/");
            //        await Utility.Wait();
            //        _driver.FindElement(By.ClassName("city-selector")).Click();
            //        await Utility.Wait();
            //        _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
            //        await Utility.Wait(2);
            //        for (var j = 0; j < 2; j++)
            //        {
            //            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            //            await Utility.Wait();
            //        }
            //        var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
            //        await Utility.Wait();
            //        for (var h = 1; h < counter.Count; h++)
            //        {
            //            await Utility.Wait();
            //            var u = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
            //            if (u.Count == 0)
            //                _driver.Navigate().Back();
            //            _driver.FindElements(By.ClassName("col-xs-12"))[h]?.Click();
            //            await Utility.Wait(1);
            //            _driver.FindElement(By.ClassName("post-actions__get-contact")).Click();
            //            await Utility.Wait();

            //            var a = _driver.FindElements(By.ClassName("primary"))
            //                .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم");
            //            if (a != null)
            //                _driver.FindElements(By.ClassName("primary"))
            //                    .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم")?.Click();
            //            await Utility.Wait();
            //            var txt = _driver.FindElements(By.ClassName("post-fields-item__value")).FirstOrDefault()?.Text;
            //            if (txt == "(پنهان‌شده؛ چت کنید)") continue;
            //            list.Add(txt);
            //            await Utility.Wait();
            //            _driver.Navigate().Back();
            //        }

            //        File.WriteAllLines(path, list);
            //    }
            //    var t = list.GroupBy(q => q).Where(q => q.Count() == 1).Select(q => q.Key).ToList();
            //    File.WriteAllLines(path, t);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    WebErrorLog.ErrorInstence.StartErrorLog(ex);
            //    File.WriteAllLines(path, list);
            //    return false;
            //}
            return true;
        }

        public async Task SendChat(List<string> msg, List<string> msg2, int count, string city, string cat1, string cat2, string cat3, string fileName, SimcardBussines sim)
        {
            //try
            //{
            //    //توکن چت نداشت برگرد
            //    var log2 = await SimcardBussines.GetAsync(sim.Number);
            //    if (log2.ChatToken != null && string.IsNullOrEmpty(log2.ChatToken)) return;

            //    //ورود به دیوار
            //    var log = await Login(sim.Number);
            //    if (!log) return;
            //    //_driver.Navigate().GoToUrl("https://divar.ir/");
            //    await Utility.Wait();
            //    //انتخاب شهر
            //    _driver.FindElement(By.ClassName("city-selector")).Click();
            //    await Utility.Wait();
            //    _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
            //    await Utility.Wait(2);

            //    //انتخاب دسته بندی
            //    if (!string.IsNullOrEmpty(cat1))
            //    {
            //        var p = _driver.FindElements(By.ClassName("category-dropdown__icon")).Any();
            //        if (!p) return;
            //        _driver.FindElements(By.ClassName("category-dropdown__icon")).FirstOrDefault()?.Click();
            //        await Utility.Wait();
            //        _driver.FindElements(By.ClassName("category-button")).FirstOrDefault(q => q.Text == cat1)
            //            ?.Click();
            //        if (string.IsNullOrEmpty(cat2))
            //            return;
            //        if (string.IsNullOrEmpty(cat3))
            //            _driver.FindElements(By.ClassName("category-button")).FirstOrDefault(q => q.Text == cat2)
            //                ?.Click();
            //        else
            //            _driver.FindElements(By.ClassName("category-button")).FirstOrDefault(q => q.Text == cat3)
            //                ?.Click();
            //        await Utility.Wait();
            //    }

            //    var ele = _driver.FindElements(By.ClassName("col-xs-12")).Any();
            //    while (!ele)
            //    {
            //        ele = _driver.FindElements(By.ClassName("col-xs-12")).Any();
            //    }
            //    await Utility.Wait(5);
            //    var j = 0;
            //    //اسکرول تا تعداد مشخص
            //    var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
            //    var total = counter.Count;
            //    var scroll = 0;
            //    while (counter.Count <= count)
            //    {
            //        if (scroll >= 10) break;
            //        ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            //        await Utility.Wait();
            //        counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
            //        if (total == counter.Count) break;
            //        scroll++;
            //    }

            //    if (counter.Count <= count) count = counter.Count - 1;

            //    for (var i = 0; j < count; i++)
            //    {
            //        //انتخاب آگهی
            //        await Utility.Wait();
            //        _driver.FindElements(By.ClassName("col-xs-12"))[i + 1]?.Click();
            //        await Utility.Wait(1);
            //        //دریافت شماره آگهی
            //        await Utility.Wait(5);
            //        _driver.FindElement(By.ClassName("post-actions__get-contact")).Click();
            //        await Utility.Wait();

            //        var a = _driver.FindElements(By.ClassName("primary"))
            //            .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم");
            //        if (a != null)
            //            _driver.FindElements(By.ClassName("primary"))
            //                .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم")?.Click();
            //        await Utility.Wait(1);
            //        //چت
            //        var el = _driver.FindElements(By.ClassName("post-actions__chat")).Any();
            //        var txt = _driver.FindElements(By.ClassName("post-fields-item__value")).FirstOrDefault()?.Text;
            //        if (txt == "(پنهان‌شده؛ چت کنید)")
            //        {
            //            //شروع چت
            //            _driver.FindElement(By.ClassName("post-actions__chat")).Click();
            //            var qanoon = _driver.FindElements(By.TagName("button"))
            //                .Where(q => q.Text == "با قوانین دیوار موافقم").ToList();
            //            if (qanoon.Count > 0)
            //                qanoon.FirstOrDefault()?.Click();
            //            var dc = _driver.WindowHandles.Count;
            //            if (dc > 1)
            //                _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            //            await Utility.Wait(2);

            //            var notAllowed = _driver.FindElements(By.TagName("p"))
            //                .Any(q => q.Text == "در حال حاضر امکان ارسال این نوع پیام وجود ندارد.");
            //            if (notAllowed)
            //            {
            //                _driver.Close();
            //                _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //                _driver.Navigate().Back();
            //                return;
            //            }


            //            await Utility.Wait(2);
            //            var notFound = _driver.FindElements(By.ClassName("content")).Any(q => q.Text == "یافت نشد!");
            //            if (notFound)
            //            {
            //                _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "بستن")?.Click();
            //                _driver.Close();
            //                _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //                _driver.Navigate().Back();
            //                continue;
            //            }
            //            var logEl = _driver.FindElements(By.ClassName("auth__input__view")).Any();
            //            if (logEl)
            //            {
            //                var tt = await LoginChat(sim.Number, false);
            //                if (!tt)
            //                {
            //                    _driver.Close();
            //                    _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //                    _driver.Navigate().Back();
            //                    return;
            //                }
            //            }
            //            await Utility.Wait(2);
            //            var chatBox = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //            while (!chatBox)
            //            {
            //                await Utility.Wait(2);
            //                chatBox = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //            }
            //            var rnd = new Random().Next(0, msg.Count);
            //            await Utility.Wait(2);
            //            try
            //            {
            //                //ارسال متن اول
            //                var thread = new Thread(() => Clipboard.SetText(msg[rnd]));
            //                thread.SetApartmentState(ApartmentState.STA);
            //                thread.Start();
            //                var f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //                while (!f)
            //                {
            //                    f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //                }
            //                var t = _driver.FindElement(By.ClassName("chat-box__input"));

            //                t.Click();
            //                await Utility.Wait();
            //                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
            //                t.SendKeys("" + '\n');
            //                var thread1 = new Thread(Clipboard.Clear);
            //                thread1.SetApartmentState(ApartmentState.STA);
            //                thread1.Start();





            //                await Utility.Wait(2);

            //                // ذخیره شماره در جدول که بعدا کسی باهاش چت نکنه
            //                var chatNumbers = new ChatNumberBussines()
            //                {
            //                    Guid = Guid.NewGuid(),
            //                    Number = txt.FixString(),
            //                    DateSabt = DateConvertor.M2SH(DateTime.Now),
            //                    Status = true,
            //                    Type = AdvertiseType.Divar,
            //                    DateM = DateTime.Now,
            //                    City = city,
            //                    Cat = cat1 + "_" + cat2 + "_" + cat3
            //                };
            //                await chatNumbers.SaveAsync();
            //                j++;
            //            }
            //            catch (Exception e)
            //            {
            //                WebErrorLog.ErrorInstence.StartErrorLog(e);
            //            }
            //            _driver.Close();
            //            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //            _driver.Navigate().Back();
            //            break;
            //        }
            //        else
            //        {
            //            if (!el)
            //            {
            //                //ذخیره شماره

            //                if (File.Exists(fileName))
            //                {
            //                    var numbers = File.ReadAllLines(fileName).ToList();
            //                    numbers.Add(txt.FixString());
            //                    //غیر تکراری بودن شماره
            //                    numbers = numbers.GroupBy(q => q).Where(q => q.Count() == 1).Select(q => q.Key).ToList();
            //                    File.WriteAllLines(fileName, numbers);
            //                }
            //                else
            //                {
            //                    File.WriteAllText(fileName, txt.FixString());
            //                }
            //                _driver.Navigate().Back();
            //                continue;
            //            }
            //            //اگر شماره قبلا چت شده بود چت نکن
            //            var day = DateTime.Now.AddDays(-clsSetting.DayCountForDelete);
            //            var allNumbers = ChatNumberBussines.GetAll(AdvertiseType.Divar).Where(q => q.DateM >= day)
            //                .ToList();

            //            var n = 0;
            //            foreach (var item in allNumbers)
            //            {
            //                if (item == null && string.IsNullOrEmpty(item.Number)) continue;
            //                if (txt.FixString() == item.Number)
            //                    n++;
            //            }

            //            if (n > 0)
            //            {
            //                _driver.Navigate().Back();
            //                continue;
            //            }
            //            //شروع چت
            //            await Utility.Wait(3);
            //            _driver.FindElement(By.ClassName("post-actions__chat")).Click();
            //            var qanoon = _driver.FindElements(By.TagName("button"))
            //                 .Where(q => q.Text == "با قوانین دیوار موافقم").ToList();
            //            if (qanoon.Count > 0)
            //                qanoon.FirstOrDefault()?.Click();
            //            var dc = _driver.WindowHandles.Count;
            //            if (dc > 1)
            //                _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            //            await Utility.Wait(2);

            //            var notAllowed = _driver.FindElements(By.TagName("p"))
            //                .Any(q => q.Text == "در حال حاضر امکان ارسال این نوع پیام وجود ندارد.");
            //            if (notAllowed)
            //            {
            //                _driver.Close();
            //                _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //                _driver.Navigate().Back();
            //                return;
            //            }



            //            var notFound = _driver.FindElements(By.ClassName("content")).Any(q => q.Text == "یافت نشد!");
            //            if (notFound)
            //            {
            //                _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "بستن")?.Click();
            //                _driver.Close();
            //                _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //                _driver.Navigate().Back();
            //                continue;
            //            }
            //            var logEl = _driver.FindElements(By.ClassName("auth__input__view")).Any();
            //            if (logEl)
            //            {
            //                var tt = await LoginChat(sim.Number, false);
            //                if (!tt)
            //                {
            //                    _driver.Close();
            //                    _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //                    _driver.Navigate().Back();
            //                    return;
            //                }
            //            }
            //            await Utility.Wait(2);
            //            var chatBox = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //            while (!chatBox)
            //            {
            //                await Utility.Wait(2);
            //                chatBox = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //            }
            //            var rnd = new Random().Next(0, msg.Count);
            //            await Utility.Wait(2);
            //            try
            //            {
            //                var f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //                while (!f)
            //                {
            //                    f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
            //                }
            //                //ارسال متن اول
            //                var thread = new Thread(() => Clipboard.SetText(msg[rnd]));
            //                thread.SetApartmentState(ApartmentState.STA);
            //                thread.Start();

            //                var t = _driver.FindElement(By.ClassName("chat-box__input"));
            //                t.Click();
            //                await Utility.Wait();
            //                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
            //                t.SendKeys("" + '\n');
            //                var thread1 = new Thread(Clipboard.Clear);
            //                thread1.SetApartmentState(ApartmentState.STA);
            //                thread1.Start();





            //                //_driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg[rnd] + '\n');
            //                await Utility.Wait(2);


            //                //اگر کاربر جواب داده بود، متن دوم رو بفرست
            //                var allChat = _driver.FindElements(By.ClassName("dimmable"))
            //                    .Where(q => q.Text.Contains("پیام جدید") && !q.Text.Contains("پستچی دیوار")).ToList();
            //                await Utility.Wait(1);
            //                if (allChat.Count > 0)
            //                {
            //                    foreach (var element in allChat)
            //                    {
            //                        element.Click();
            //                        var rnd2 = new Random().Next(0, msg2.Count);
            //                        await Utility.Wait(1);


            //                        var thread10 = new Thread(() => Clipboard.SetText(msg2[rnd2]));
            //                        thread10.SetApartmentState(ApartmentState.STA);
            //                        thread10.Start();

            //                        var t2 = _driver.FindElement(By.ClassName("chat-box__input"));
            //                        t2.Click();
            //                        await Utility.Wait();
            //                        t2.SendKeys(OpenQA.Selenium.Keys.Control + "v");
            //                        t2.SendKeys("" + '\n');
            //                        var thread101 = new Thread(Clipboard.Clear);
            //                        thread101.SetApartmentState(ApartmentState.STA);
            //                        thread101.Start();



            //                        //_driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg2[rnd2] + '\n');
            //                        await Utility.Wait(2);
            //                    }
            //                }

            //                // ذخیره شماره در جدول که بعدا کسی باهاش چت نکنه
            //                var chatNumbers = new ChatNumberBussines()
            //                {
            //                    Guid = Guid.NewGuid(),
            //                    Number = txt.FixString(),
            //                    DateSabt = DateConvertor.M2SH(DateTime.Now),
            //                    Status = true,
            //                    Type = AdvertiseType.Divar,
            //                    DateM = DateTime.Now,
            //                    City = city,
            //                    Cat = cat1 + "_" + cat2 + "_" + cat3
            //                };
            //                await chatNumbers.SaveAsync();
            //                j++;
            //            }
            //            catch (Exception e)
            //            {
            //                WebErrorLog.ErrorInstence.StartErrorLog(e);
            //            }
            //            _driver.Close();
            //            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //            _driver.Navigate().Back();
            //        }
            //    }
            //}
            //catch (StaleElementReferenceException) { }
            //catch (Exception ex)
            //{
            //    WebErrorLog.ErrorInstence.StartErrorLog(ex);
            //}
        }

        public async Task GetCategory()
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://divar.ir/new");
                await Utility.Wait();
                var listCat = _driver.FindElements(By.ClassName("expanded-category-selector__item"))
                    .ToList();
                var listAll = await DivarCategoryBussines.GetAllAsync();
                listAll = listAll.ToList();
                if (listAll.Count > 0)
                    await DivarCategoryBussines.RemoveRangeAsync(listAll.Select(q => q.Guid).ToList());
                foreach (var item in listCat)
                {
                    var a = new DivarCategoryBussines()
                    {
                        Guid = Guid.NewGuid(),
                        Name = item.Text.Trim(),
                        ParentGuid = Guid.Empty,
                    };
                    await a.SaveAsync();
                }
                listAll = await DivarCategoryBussines.GetAllAsync();
                listAll = listAll.ToList();
                if (listAll.Count <= 0) return;
                foreach (var element in listAll)
                {
                    _driver.FindElements(By.ClassName("expanded-category-selector__item"))
                        .FirstOrDefault(q => q.Text == element.Name)?.Click();
                    await Utility.Wait();
                    var listCat2 = _driver.FindElements(By.ClassName("expanded-category-selector__item"))
                        .ToList();
                    foreach (var item in listCat2)
                    {
                        var a = new DivarCategoryBussines()
                        {
                            Guid = Guid.NewGuid(),
                            Name = item.Text.Trim(),
                            ParentGuid = element.Guid,
                        };
                        await a.SaveAsync();
                    }


                    var newList = await DivarCategoryBussines.GetAllAsync(element.Guid);
                    if (newList.Count <= 0) continue;
                    foreach (var item in newList)
                    {
                        _driver.FindElements(By.ClassName("expanded-category-selector__item"))
                            .FirstOrDefault(q => q.Text == item.Name)?.Click();
                        await Utility.Wait();
                        var listCat3 = _driver.FindElements(By.ClassName("expanded-category-selector__item"))
                            .ToList();
                        if (listCat3.Count <= 0)
                        {
                            await Utility.Wait(2);
                            var element10 = _driver
                                .FindElements(By.ClassName("category-selector-action")).Any();
                            while (!element10)
                            {
                                element10 = _driver
                                    .FindElements(By.ClassName("category-selector-action")).Any();
                                await Utility.Wait(2);
                            }
                            _driver.FindElement(By.ClassName("category-selector-action")).Click();
                            await Utility.Wait();
                            continue;
                        }
                        foreach (var ne in listCat3)
                        {
                            var a = new DivarCategoryBussines()
                            {
                                Guid = Guid.NewGuid(),
                                Name = ne.Text.Trim(),
                                ParentGuid = item.Guid,
                            };
                            await a.SaveAsync();
                        }

                        await Utility.Wait(2);
                        _driver.FindElement(By.ClassName("expanded-category-selector__back-icon")).Click();
                        await Utility.Wait();
                    }

                    var a10 = _driver.FindElements(By.ClassName("expanded-category-selector__back-icon"))
                        .Any();
                    if (a10)
                        _driver.FindElement(By.ClassName("expanded-category-selector__back-icon")).Click();
                    await Utility.Wait();
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        #endregion
    }
}
