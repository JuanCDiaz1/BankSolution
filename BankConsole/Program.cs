using BankConsole;

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
    
     VerificacionInt(ID);



    Console.WriteLine("Nombre: ");
    string name= Console.ReadLine();

      Console.WriteLine("Email: ");
    string email= Console.ReadLine();

      Console.WriteLine("Saldo: ");
    decimal balance= decimal.Parse(Console.ReadLine());

    Console.WriteLine("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
    char userType= char.Parse(Console.ReadLine());

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
      int ID= Int16.Parse(Console.ReadLine());
      string result = Storage.DeleteUser(ID);
 if(result.Equals("Success")){
    Console.WriteLine("Usuario Eliminado");
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
     numValido=true;
     Console.WriteLine("Se ha registrado exitósamente");
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