using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PGChatBot;
using static System.Console;
using static System.Threading.Thread;

const string userId = "steamb23@outlook.com";
const string password = "~;5(Vg^t;X61r}D";
const string uid = "487";
var texts = new WeightData<string>[]
{
    new(5, "햄스터김치볶음"),
    new(1, "잘자보동포"),
    new(1, "끼에에엑"),
    new(1, "존프루시안테그는신인가"),
    new(1, "하이접니다"),
    new(2, "초닠"),
    new(2, "존익"),
    new(1, "야레야레다제"),
};
var links = new WeightData<string>[]
{
    new(1, "https://youtu.be/WQYN2P3E06s"),
    new(1, "https://youtu.be/-4788Tmz9Zo"),
    new(1, "https://youtu.be/miomuSGoPzI"),
    new(1, "https://youtu.be/NUrjZ1UPWTE"),
    new(0.5, "https://youtu.be/gkTb9GP9lVI"),
    new(0.5, "https://youtu.be/dQw4w9WgXcQ")
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
var lastChatMessage = chatMessages.SkipLast(1).Last();
var lastChatName = lastChatMessage.FindElement(By.ClassName("chatName"));
if (lastChatName.GetAttribute("title") == uid)
{
    var lastChatText = lastChatMessage.FindElement(By.ClassName("chatText"));

    if (texts.Any(text => lastChatText.Text.Contains(text.Value)))
    {
        WriteLine("Still the same message remains. Terminate jobs to avoid duplication.");
        return;
    }
}

// 메시지 입력 테스트
WriteLine("MessageBox enter...");
var messagebox = driver.FindElement(By.Id("message-text"));

WriteLine("MessageBox enter...");
var text = WeightData<string>.Random(texts);
var link = WeightData<string>.Random(links);
var textResult = $"{text} {link}";
messagebox.SendKeys(textResult);
messagebox.SendKeys(Keys.Enter);
WriteLine(textResult);
WriteLine("Done.");