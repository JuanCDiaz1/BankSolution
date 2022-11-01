using MailKit.Net.Smtp;
using MimeKit;
namespace BankConsole;

public static class EmailService{
    public static void SendMail(){
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress ("Juan Díaz", "juankid2003@gmail.com"));
        message.To.Add(new MailboxAddress("Admin", "yadigzzt@gmail.com"));
        message.Subject="BlankConsole: Usuarios nuevos";
        message.Body= new TextPart("Plain"){
            Text=GetEmailText()
        };
        using (var client= new SmtpClient()){
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("juankid2003@gmail.com", "(contraseña de 16 caracteres)");
            client.Send(message);
            client.Disconnect(true);
        }
    }


    public static string GetEmailText(){
        List<User> newUsers=Storage.GetNewUsers();
        if(newUsers.Count==0){
            return "No hay usuarios nuevos";
        }
        string emailText ="Uusarios registrados hoy:\n";

        foreach(User user in newUsers){
            emailText+="\t "+user.ShowData()+"\n";
        }
        return emailText;


    }
}