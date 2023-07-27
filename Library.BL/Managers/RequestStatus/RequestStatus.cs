namespace Library.BL;

public class RequestStatus
{
    public bool Status { get; set; } = false;
    public string Message { get; set; } = string.Empty;


}

public class Constants
{

    public static string Operation = "Operation";

    public static string Status = "Status";
    public static string Message = "Message";



    public static string AddBook = "New Book Added Successfully";
    public static string AddBookFailed = "Failed To add The new Book";

    public static string EditBook = "Book Edited Successfully";
    public static string WrongCopiesNewQuantity = "An error occurred. Type the number of available copies correctly ";

    public static string DeleteBook = "Book Deleted Successfully";
    public static string DeleteBookFailed = "Failed to delete the Book";
    public static string DeleteBorrowedBook = "we cant delete that book there are some copies with clients";

    public static string AddClient = "New Client Added Successfully";
    public static string AddClientFailed = "Failed To add The new Client";
    public static string ClientdoesntExist = "This Client Doesnt Exist";
    public static string EditClient = "Client Edited Successfully";
    public static string EditClientFailes = "Failed To edit the Client  ";



    public static string DeleteClient = "Client Deleted Successfully";
    public static string DeleteClientFailed = "Failed to delete the Client";
    public static string DeleteClientHasUnReturnedBooks = "we cant delete that Client he has unReturned Books";

    public static string BookDoesntExist = "Book doesnt exist";
    public static string ClientDoesntExist = "Client doesnt exist";

    public static string BorrowBooksucceeded = "Book Borrowed Successfully";
    public static string BorrowBookFailed = "Failed ... Client  has acopy of the book he cant borrow it again";
    public static string noAvailableCopies = "Sorry ... All copies are borrowed ";


    public static string ReturnBooksucceeded = "Book Returned Successfully";
    public static string ReturnBookFailed = "Failed... Client didnt get this book from us";

   


}
