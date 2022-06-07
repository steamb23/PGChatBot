using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Console;
using static System.Threading.Thread;

const string userId = "steamb23@outlook.com";
const string password = "~;5(Vg^t;X61r}D";
const string uid = "487";
var texts = new[]
{
    "햄스터김치볶음",
    "$$볼보자동차",
    "끼에에엑",
    "존프루시안테그는신인가",
    "하이접니다",
    "쵸닉",
    "https://media.githubusercontent.com/media/steamb23/PGChatBot/master/img/4eab27b42ba6da71.gif"
};

var driverOptions = new ChromeOptions();
driverOptions.AddArguments("--headless");

using var driver = new ChromeDriver(driverOptions)
{
    Url = "https://chat.zniq.co/",
};

// 로그인
WriteLine("Login...");
var loginIdInput = driver.FindElement(By.Id("login-id"));
var loginPasswordInput = driver.FindElement(By.Id("login-password"));

loginIdInput.SendKeys(userId);
loginPasswordInput.SendKeys(password);
loginPasswordInput.SendKeys(Keys.Enter);
Sleep(1000); // 갱신 대기

// 마지막 채팅 체크
WriteLine("Message duplicate test...");
var chatMessages = driver.FindElements(By.ClassName("chatMessage"));
var lastChatMessage = chatMessages.Last();
var lastChatName = lastChatMessage.FindElement(By.ClassName("chatName"));
if (lastChatName.GetAttribute("title") == uid)
{
    var lastChatText = lastChatMessage.FindElement(By.ClassName("chatText"));

    if (texts.Any(text => lastChatText.Text.Contains(text)))
    {
        WriteLine("Still the same message remains. Terminate jobs to avoid duplication.");
        return;
    }
}

// 메시지 입력 테스트
WriteLine("MessageBox enter...");
var messagebox = driver.FindElement(By.Id("message-text"));

messagebox.SendKeys(texts[Random.Shared.Next(0, texts.Length)]);
messagebox.SendKeys(Keys.Enter);
WriteLine("Done.");
