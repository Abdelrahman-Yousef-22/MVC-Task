using Library.DAL;
using Microsoft.EntityFrameworkCore;

namespace Library.BL;

public class BooksManager : IBooksManager
{
    private readonly IUnitOfWork _unitOfWork;

    public BooksManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    #region 1-Select

    public IEnumerable<BooksReadVM> GetAll()
    {
        IEnumerable<Book> books = _unitOfWork.BooksRepo.GetAll();
        IEnumerable<BooksReadVM> booksVM = books.Select(b =>
                        new BooksReadVM
                        {
                            Id = b.Id,
                            Name = b.Name,
                            Quantity = b.Quantity,
                            AvailableQuantity = b.AvailableQuantity
                        });

        return booksVM;
    }

    public BooksReadVM GetByID(int id)
    {
        var book = _unitOfWork.BooksRepo.GetById(id);
        var bookVM = new BooksReadVM
        {
            Id = book.Id,
            Name = book.Name,
            Quantity = book.Quantity

        };

        return bookVM;
    }

    public BookReadWithClientsVM? GetByIDWithClients(int id)
    {


        Book book = _unitOfWork.BooksRepo.GetByIDWithClients(id);
        var bookVM = new BookReadWithClientsVM
        {
            Id = book.Id,
            Name = book.Name,
            Quantity = book.Quantity,
            AvailableQuantity = book.AvailableQuantity,
            Clients = book.BorrowedBooks.Where(b => b.IsReturned == false).Select(BorrowedBook => new ClientReadVM
            {
                Id = BorrowedBook.Client.Id,
                Name = BorrowedBook.Client.Name,
            })
        };

        return bookVM;
    }

   
    #endregion

    #region 2-Add

    public RequestStatus Add(BookAddVM book)
    {
        Book newBook = new Book
        {
            Name = book.Name,
            Quantity = book.Quantity,
            AvailableQuantity = book.Quantity
        };
        _unitOfWork.BooksRepo.Add(newBook);
        bool status = _unitOfWork.SaveChanges()> 0;
        if (status)
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.AddBook
            };
        }
        else
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.AddBookFailed
            };
        }
    }
    #endregion

    #region 3-Edit
    public RequestStatus Edit(BookEditVM book)
    {
        //check new quantity is sot less than the number of unreturned borrowed copies
        int UnreturnedCopies = _unitOfWork.BorrowedBooksRepo.GetcountOfUnReturnedCopies(book.Id);
        if (book.Quantity < UnreturnedCopies)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.WrongCopiesNewQuantity
            };
        }
        var bookToEdit = _unitOfWork.BooksRepo.GetById(book.Id);

        var oldBookQuantity = bookToEdit!.Quantity;
        var oldBookAvailableQuantity = bookToEdit.AvailableQuantity;

        bookToEdit.Quantity = book.Quantity;
        bookToEdit.AvailableQuantity = book.Quantity - oldBookQuantity + oldBookAvailableQuantity;
        bookToEdit.Name = book.Name;
        _unitOfWork.SaveChanges();
        return new RequestStatus
        {
            Status = true,
            Message = Constants.EditBook
        };
    }
    #endregion

    #region 4-Delete

    public RequestStatus Delete(int id)
    {
        //check book exist in DB
        Book? book = _unitOfWork.BooksRepo.GetById(id);
        if (book is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.BookDoesntExist
            };
        }
        //check every copies of that book in library  not borrowed to any client
        //so we can delete it
        if (book.Quantity == book.AvailableQuantity)
        {
            _unitOfWork.BooksRepo.Delete(book);
            bool status = _unitOfWork.SaveChanges() > 0;
            if (status)
            {
                return new RequestStatus
                {
                    Status = status,
                    Message = Constants.DeleteBook
                };
            }
            else
            {
                return new RequestStatus
                {
                    Status = status,
                    Message = Constants.DeleteBookFailed
                };

            }
        }
        else
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.DeleteBorrowedBook
            };

        }

    }



    BookEditVM IBooksManager.GetByIdToEdit(int id)
    {
        var book = _unitOfWork.BooksRepo.GetById(id);
        var bookVM = new BookEditVM
        {
            Id = book.Id,
            Name = book.Name,
            Quantity = book.Quantity

        };

        return bookVM;
    }


    #endregion

    #region Borrow book V2


    public RequestStatus Borrow(BookBorrowVM book)
    {
        //check Client and Book exist
        Book? bookFromDB = _unitOfWork.BooksRepo.GetById(book.BookId);
        if (bookFromDB is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.BookDoesntExist
            };
        }

        Client? clientFromDB = _unitOfWork.ClientsRepo.GetById(book.ClientId);
        if (clientFromDB is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.ClientDoesntExist
            };
        }
        //check if there are available copy of the book or not
        if (bookFromDB.AvailableQuantity <= 0)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.noAvailableCopies
            };
        }

        //check if user has borrow this book before and didnt return it

        BorrowedBook? LastBorrow = _unitOfWork.BorrowedBooksRepo.GetLastBorrow(book.BookId, book.ClientId);

        if (LastBorrow is null || LastBorrow.IsReturned)
        {
            BorrowedBook borrowedBook = new BorrowedBook
            {
                BookId = book.BookId,
                ClientId = book.ClientId,
                DateShouldReturn = book.DateShouldReturn
            };
            _unitOfWork.BorrowedBooksRepo.Add(borrowedBook);
            var bookToEditAvailableQuantity = _unitOfWork.BooksRepo.GetById(book.BookId);
            bookToEditAvailableQuantity.AvailableQuantity--;

            return new RequestStatus
            {
                Status = _unitOfWork.SaveChanges() > 0,
                Message = Constants.BorrowBooksucceeded
            };
        }
        else
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.BorrowBookFailed
            };
        }




    }

    #endregion



    #region Borrow book V1

    //public bool Borrow(BookBorrowVM book)
    //{

    //    //check if user has borrow this book before and didnt return it
    //    //we will do that by count the times he borrow and return that book
    //    //if the number of borrowTimes > returnTimes  this mean he didnt return
    //    //the book he took last time 
    //   int  borrowCount=_unitOfWork.BorrowedBooksRepo.Getcount(book.BookId,book.ClientId);

    //    if(borrowCount!=0)
    //    {
    //        int returnCount = _unitOfWork.ReturnedBooksRepo.Getcount(book.BookId, book.ClientId);
    //        if (borrowCount > returnCount)
    //        {
    //            return false;
    //        }

    //    }


    //    BorrowedBook borrowedBook = new BorrowedBook
    //    {
    //        BookId = book.BookId,
    //        ClientId = book.ClientId,
    //        DateShouldReturn = book.DateShouldReturn
    //    };
    //    _unitOfWork.BorrowedBooksRepo.Add(borrowedBook);
    //    var bookToEditAvailableQuantity =_unitOfWork.BooksRepo.GetById(book.BookId);
    //    bookToEditAvailableQuantity.AvailableQuantity--;
    //    return _unitOfWork.SaveChanges()>0;

    //}

    #endregion

    #region Return book
    public RequestStatus Return(BookReturnVM book)
    {
        //check Client and Book exist
        Book? bookFromDB = _unitOfWork.BooksRepo.GetById(book.BookId);
        if (bookFromDB is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.BookDoesntExist
            };
        }

        Client? clientFromDB = _unitOfWork.ClientsRepo.GetById(book.ClientId);
        if (clientFromDB is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.ClientDoesntExist
            };
        }

        //check if user has return this book last time he borrow it 

        BorrowedBook? LastBorrow = _unitOfWork.BorrowedBooksRepo.GetLastBorrow(book.BookId, book.ClientId);

        if (LastBorrow is not null && !LastBorrow.IsReturned)
        {
            ReturnedBook returnedBook = new ReturnedBook
            {
                BookId = book.BookId,
                ClientId = book.ClientId,
            };
            _unitOfWork.ReturnedBooksRepo.Add(returnedBook);
            LastBorrow.IsReturned = true;
            var bookToEditAvailableQuantity = _unitOfWork.BooksRepo.GetById(book.BookId);
            bookToEditAvailableQuantity.AvailableQuantity++;
            return new RequestStatus
            {
                Status = _unitOfWork.SaveChanges() > 0,
                Message = Constants.ReturnBooksucceeded
            };
        }
        else
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.ReturnBookFailed
            };
        }


    }





    #endregion




}
