using BankConsole;
using System.Globalization;
using System.Text.RegularExpressions;

if (args.Length==0){
    EmailService.SendMail();
}
else{
    ShowMenu();
}
void ShowMenu(){
    Console.Clear();
    Console.WriteLine("Selecciona una opción: ");
    Console.WriteLine("1.- Crear un nuevo usuario");
    Console.WriteLine("2.- Eliminar un usuario existente.");
    Console.WriteLine("3.- Salir ");


    int option=0;
    do{
        string input=Console.ReadLine();
        if(!int.TryParse(input, out option)){
            Console.WriteLine("Debes ingresar un número (1, 2 o 3)");
        }else if(option>3){
            Console.WriteLine("Debes ingresar un número válido (1, 2 o 3)");
        }
    }while(option==0||option>3);
    switch (option){
        case 1:
        CreateUser();
        break;
        case 2:
        DeleteUser();
        break;
        case 3:
        Environment.Exit(0);
        break;
    }
}
void CreateUser(){
    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario: ");
    
     int ID=0;
      ID = VerificacionInt(ID);
      
    
   while(Storage.IDExists(ID) == true){
        Console.WriteLine("El ID ingresado ya existe, inténtelo de nuevo.");
        Console.Write("Ingresa el ID del usuario a añadir: ");
        ID = VerificacionInt(ID);
    }

    
     Console.WriteLine("Nombre: ");
     string name= Console.ReadLine();

     string email="email";
     email = ValEmail1(email);

      decimal balance=0;
      balance=VerificacionDecimal(balance);

     char userType= 'a';
     userType=VerificacionUserType(userType);







 User newUser;

 if(userType.Equals('c')){
     Console.WriteLine("Régimen Fiscal: ");
    char TaxRegime= char.Parse(Console.ReadLine());
    newUser = new Client(ID,name,email,balance,TaxRegime);
 }else{
     Console.WriteLine("Departamento: ");
    string department=Console.ReadLine();
    newUser = new Employee(ID,name,email,balance,department);
 }
 Storage.AddUser(newUser);
  Console.WriteLine("Usuario Creado.");
  Thread.Sleep(2000);
  ShowMenu();
 }

 void DeleteUser(){
    Console.Clear();
    Console.WriteLine("Ingresa el ID del usuario a eliminar: ");
       int ID=0;
        ID = VerificacionInt(ID);
      string result = Storage.DeleteUser(ID);
 if(result.Equals("Success")){
    Console.WriteLine("Usuario Eliminado");
     Thread.Sleep(2000);
     ShowMenu();
 } else{
    Console.WriteLine("No se ha encontrado el usuario con el ID ingresado");
    Thread.Sleep(2000);
     ShowMenu();
 }
 
}

static int VerificacionInt(int ID){
 bool numValido=false;
    while(!numValido){
        try{
     Console.WriteLine("ID: ");
     ID= int.Parse(Console.ReadLine());
     if(ID>0){
     numValido=true;}
     else{
        Console.WriteLine("El número introducido es menor o igual a 0, vuelva a intentarlo");
     }
     }
     catch(FormatException){
         Console.WriteLine("El dato ingresado no es un número entero, vuelva a intentarlo");
         numValido=false;
         
     }catch(OverflowException){
         Console.WriteLine("El número es muy chico o muy grande, vuelva a intentarlo");
         numValido=false;
     }
 }
     return ID;
}
static bool IsValidEmail(string email)

      
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    
                    var idn = new IdnMapping();

                   
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
   
        }

static string ValEmail1(string email){
           bool emailValid=false;
    while(!emailValid){
       Console.WriteLine("Email: ");
       email= Console.ReadLine();
       emailValid=IsValidEmail(email);
       if(emailValid==false){
            Console.WriteLine("El correo introducido no está en el formato correcto, inténtelo de nuevo.");
       }
        }
        return email;
        }


static decimal VerificacionDecimal(decimal Balance){
 bool numValido=false;
    while(!numValido){
        try{
     Console.WriteLine("Saldo: ");
     Balance= decimal.Parse(Console.ReadLine());
     if(Balance>0){
     numValido=true;
     Console.WriteLine("Se ha registrado exitósamente.");}else{Console.WriteLine("El número ingresado es menor a 0, vuelva a intentarlo");}
     }
     catch(FormatException){
         Console.WriteLine("El dato ingresado no es un número válido, vuelva a intentarlo");
         numValido=false;
         
     }catch(OverflowException){
         Console.WriteLine("El número es muy chico o muy grande, vuelva a intentarlo");
         numValido=false;
     }
 }
     return Balance;
}
static char VerificacionUserType(char userType){
        bool numValido=false;
        while(!numValido){

            try{
            Console.WriteLine("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
             userType= Char.Parse(Console.ReadLine());
             if(userType=='c'||userType=='e'){
              Console.WriteLine("Se ha registrado con éxito.");
              numValido=true;
             }else{
                 Console.WriteLine("El dato ingresado no es una 'c' o una 'e', vuelva a intentarlo.");
             }}
             catch(FormatException){
                  Console.WriteLine("El dato ingresado no es una 'c' o una 'e', vuelva a intentarlo.");
             }

        }
        return userType;
}