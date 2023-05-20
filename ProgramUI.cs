
using System.Collections.Generic;
using System.Linq;

public class ProgramUI
{
    private ContactDictionaryRepository _contactRepo = new ContactDictionaryRepository();
    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;
        while (isRunning)
        {
            System.Console.WriteLine("Welcome to Lowell Organization Logistics.\n" +
            "If you want to make changes please select one of the following:\n" +
            "1. Display Dictionary notes.\n" +
            "2. Add acontact. \n" +
            "3. List all contacts.\n" +
            "4. Get contact(s) by Name.\n" +
            "5. Edit contact by ID.\n" +
            "6. Remove contact by ID.\n");

            var userInput = int.Parse(Console.ReadLine()!);
            switch (userInput)
            {
                case 1:
                    DicDisplayNotes();
                    break;
                case 2:
                    DicAddAContact();
                    break;
                case 3:
                    DicListAllContacts();
                    break;
                case 4:
                    DicGetContactByName();
                    break;
                case 5:
                    DicEditContactByID();
                    break;
                case 6:
                    DicRemoveContactByID();
                    break;
                case 0:
                isRunning = Quit();
                break;

                default:
                    System.Console.WriteLine("Invalid Selection");
                    break;

            }
        }
    }
    private void DicDisplayNotes()
    {
        Console.Clear();

        //
        //
        Dictionary<int, Contact> contactDic = new Dictionary<int, Contact>();

        contactDic.Add(1, new Contact
        {
            ID = 1,
            Name = "Jaiden",
            Address = "ashby Avenue",
            Email = "colton1@gmail.com",
            PhoneNumber = "4151111111",
        });

        contactDic.Add(2, new Contact
        {
            ID = 2,
            Name = "Luca",
            Address = "Alcatraz Ave",
            Email = "luca1@gmail.com",
            PhoneNumber = "2222222222",
        });

        contactDic.Add(3, new Contact
        {
            ID = 3,
            Name = "Sofia",
            Address = "2nd avenue",
            Email = "sofia1@gmail.com",
            PhoneNumber = "3333333333",
        });

        contactDic.Add(4, new Contact
        {
            ID = 4,
            Name = "Emily",
            Address = "11th Avenue",
            Email = "emily1@gmail.com",
            PhoneNumber = "4151111222",
        });

        // let's count by using .count

        System.Console.WriteLine(contactDic.Count());

        // seeing the data by constraining the keys  

        foreach (int contactKey in contactDic.Keys)
        {
            System.Console.WriteLine($"{contactKey}");
        }


        Console.ReadKey();
    }

    private void DicAddAContact()
    {
        Console.Clear();

        Contact contactForm = new Contact();

        System.Console.WriteLine($"Please add a Contact Name:");
        string userInputName = Console.ReadLine()!;
        contactForm.Name = userInputName;
        
        System.Console.WriteLine($"Please enter the contact's Email:");
        string userInputEmail = Console.ReadLine()!;
        contactForm.Email = userInputEmail;

        System.Console.WriteLine($"Please enter the Contact's Adress:");
        string userInputAddress = Console.ReadLine()!;
        contactForm.Address = userInputAddress;

        System.Console.WriteLine($"Please enter the Contact's Phone Number:");
        string userInputPhoneNumber = Console.ReadLine()!;
        contactForm.PhoneNumber = userInputPhoneNumber;
    
        if (_contactRepo.AddContact(contactForm))
        {
            System.Console.WriteLine("Succes!");
        }
        else
        {
            System.Console.WriteLine("Fail!");
        }

        Console.ReadKey();

    }

    private void DicListAllContacts()
    {
        Console.Clear();
        System.Console.WriteLine($"Contacts Listing");

        var contacts = _contactRepo.GetContacts();
        foreach (var contact in contacts)
        {
            System.Console.WriteLine($"Contact ID: {contact.Value.ID}\n" +
                                      $"Contact Name:{contact.Value.Name}\n"
                                      + $"Contact Adress:{contact.Value.Address}\n"
                                      + $"Contact Phone Number:{contact.Value.PhoneNumber}\n");
        }

        Console.ReadKey();
    }

    private void DicGetContactByName()
    {
        Console.Clear();
        System.Console.WriteLine($"Contact info:");
        System.Console.WriteLine("please enter the contact's Name.");
        string userInput = Console.ReadLine()!;

        var contact = _contactRepo.GetContactByName(userInput);

        System.Console.WriteLine($"Contact's Name: {contact.Name}\n"
                                + $"Contact's Adress:{contact.Address}\n"
                                + $"Contact's Email:{contact.Email}\n"
                                + $"Contact Phone Number:{contact.PhoneNumber}\n"); 
                                                                                       
            Console.ReadKey();
    }
    private void DicEditContactByID()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("Please Enter a Contact Id:");
            int userInputContactId = int.Parse(Console.ReadLine()!);
            Contact selectedContact = _contactRepo.GetContactByKey(userInputContactId);

            if (selectedContact != null)
            {
                Contact editedContactData = new Contact();

                System.Console.WriteLine("Please enter the Contact's Name:");
                string userInputName = Console.ReadLine()!;
                editedContactData.Name = userInputName;

                System.Console.WriteLine($"Please enter the contact's Email:");
                string userInputEmail = Console.ReadLine()!;
                editedContactData.Email = userInputEmail;

                System.Console.WriteLine($"Please enter the Contact's Adress:");
                string userInputAddress = Console.ReadLine()!;
                editedContactData.Address = userInputAddress;

                System.Console.WriteLine($"Please enter the Contact's Phone Number:");
                string userInputPhoneNumber = Console.ReadLine()!;
                editedContactData.PhoneNumber = userInputPhoneNumber;

                 if (_contactRepo.UpdateContact(editedContactData.ID, editedContactData))
                {
                    System.Console.WriteLine("Success!");
                }
                else
                {
                    System.Console.WriteLine("Fail!");
                }
            }
            else
            {
                System.Console.WriteLine($"Sorry the contact with the ID: {userInputContactId} does Not exist!");
            }
            Console.ReadKey();
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }
    private void DicRemoveContactByID()
    {
        Console.Clear();

        try
        {
            System.Console.WriteLine("Please select a contact by Id.");
            int userInputContactId = int.Parse(Console.ReadLine()!);

            var isValidated = ValidateContactInDatabase(userInputContactId);
                if(isValidated)
                {
                    System.Console.WriteLine("Do you want to remove this contact y/n ?");
                    string userInputRemoveContact = Console.ReadLine()!.ToLower()!;
                    if(userInputRemoveContact == "y") 
                    {
                        if(_contactRepo.DeleteContact(userInputContactId))
                        {
                            System.Console.WriteLine($"The Contact was successfully deleted!");
                        }
                        else
                        {
                           System.Console.WriteLine($"unfortunatly the Contact was NOT deleted please check what was wrong!"); 
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine($"The Contact with the {userInputContactId} deos NOT exist");
                }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            SomthingWentWrong();
        }
    }

    private void SomthingWentWrong()
    {
        System.Console.WriteLine("Sorry something went wrong");
            PressAnyKey();
    }

    private void PressAnyKey()
        {
            System.Console.WriteLine("Press Any Key");
            Console.ReadKey();
        }

    private bool ValidateContactInDatabase(int userInputContactId)
    {
        Contact contact = GetContactDataFromDb(userInputContactId);
            if (contact != null)
            {
                Console.Clear();
                DisplayContactData(contact);
                return true;
            }
            else
            {
                System.Console.WriteLine($"The Contact w/ the ID {userInputContactId} does NOT exist");
                return false;
            }
    }

    private void DisplayContactData(Contact contact)
    {
        System.Console.WriteLine(contact);
    }

    private Contact GetContactDataFromDb(int userInputContactId)
    {
        return _contactRepo.GetContactByKey(userInputContactId);
    }

    private bool Quit()
        {
            System.Console.WriteLine("Thank you see you soon.");
            Console.ReadKey();
            return false;
        }
}
    