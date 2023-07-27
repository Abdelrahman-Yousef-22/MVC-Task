using Library.DAL;

namespace Library.BL;

public class ClientsManager:IClientsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #region CRUD Operations


    #region 1-Select

    public IEnumerable<ClientReadVM> GetAll()
    {
        var clients = _unitOfWork.ClientsRepo.GetAll();
        var clientsVM = clients.Select(c =>
                        new ClientReadVM
                        {
                            Id=c.Id,
                            Name=c.Name,
                        });

        return clientsVM;
    }

    public ClientReadVM GetByID(int id)
    {
        var client = _unitOfWork.ClientsRepo.GetById(id);
        var clientVM = new ClientReadVM
        {
            Id = client.Id,
            Name = client.Name

        };

        return clientVM;
    }


    public ClientEditVM GetByIdToEdit(int id)
    {
        var client = _unitOfWork.ClientsRepo.GetById(id);
        var clientVM = new ClientEditVM
        {
            Id = client.Id,
            Name = client.Name,
        };

        return clientVM;
    }

    public ClientReadWithBorrowedBooks? GetByIDWithBooks(int id)
    {
        var client = _unitOfWork.ClientsRepo.GetByIDWithBorrowedBooks(id);
        var clientVM = new ClientReadWithBorrowedBooks
        {
            Id = client.Id,
            Name = client.Name,
            ReturnedBooks = client.BorrowedBooks.Where(b=>b.IsReturned==true).Select(book => new BooksReadVM
            {
                Id = book.Book.Id,
                Name = book.Book.Name,
                Quantity = book.Book.Quantity
            }),
            UnReturnedBooks = client.BorrowedBooks.Where(b => b.IsReturned == false).Select(book => new BooksReadVM
            {
                Id = book.Book.Id,
                Name = book.Book.Name,
                Quantity = book.Book.Quantity
            })
        };

        return clientVM;
    }

    //public clientEditVM GetByIdToEdit(int id)
    //{
    //    var client = _unitOfWork.ClientsRepo.GetById(id);
    //    var clientVM = new clientEditVM
    //    {
    //        Id = client.Id,
    //        Name = client.Name,
    //        Quantity = client.Quantity

    //    };

    //    return clientVM;
    //}



    //public IEnumerable<DoctorsReadVM> GetByPerformance(int id)
    //{
    //    var doctors = _unitOfWork.ClientsRepo.GetDoctorsByPerformance(id);
    //    var doctorsVM = doctors.Select(d => new DoctorsReadVM
    //    {
    //        Id = d.Id,
    //        Name = d.Name,
    //        PerformanceRate = d.PerformanceRate,
    //        Salary = d.Salary,
    //        Specialization = d.Specialization,
    //    });

    //    return doctorsVM;
    //}



    //public DoctorWithPatientVM GetByIDWithPatientsandIssues(Guid id)
    //{
    //    var doctor = _unitOfWork.ClientsRepo.GetByIdWithPatientandIssues(id);
    //    var doctorsVM = new DoctorWithPatientVM
    //    {
    //        Id = doctor.Id,
    //        Name = doctor.Name,
    //        PerformanceRate = doctor.PerformanceRate,
    //        Salary = doctor.Salary,
    //        Specialization = doctor.Specialization,
    //        Patients = doctor.Patients
    //    };

    //    return doctorsVM;
    //}
    #endregion

    #region 2-Add

    public RequestStatus Add(ClientAddVM client)
    {
        Client newClient = new Client
        {
            Name = client.Name,
        };
        _unitOfWork.ClientsRepo.Add(newClient);
        bool status = _unitOfWork.SaveChanges() > 0;
        if (status)
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.AddClient
            };
        }
        else
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.AddClientFailed
            };
        }
    }
    #endregion

    #region 3-Edit
    public RequestStatus Edit(ClientEditVM client)
    {
        Client clientToEdit = _unitOfWork.ClientsRepo.GetById(client.Id);
        if(clientToEdit is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.ClientdoesntExist
            };
        }
        clientToEdit.Name = client.Name;
        bool status = _unitOfWork.SaveChanges() > 0;
        if (status)
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.EditClient
            };
        }
        else
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.EditClientFailes
            };
        }
    }
    #endregion

    #region 4-Delete

    public RequestStatus Delete(int id)
    {
        //check Client exist in DB
        Client? client = _unitOfWork.ClientsRepo.GetById(id);
        if (client is null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.ClientDoesntExist
            };
        }
        //check if user has unreturned books 

        BorrowedBook? borrowedBook= _unitOfWork.BorrowedBooksRepo.CheckUnReturnedBorrowedbooks(id);

        if(borrowedBook is not null)
        {
            return new RequestStatus
            {
                Status = false,
                Message = Constants.DeleteClientHasUnReturnedBooks
            };
        }

        _unitOfWork.ClientsRepo.Delete(client);
        bool status = _unitOfWork.SaveChanges()>0;
        if(status)
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.DeleteClient
            };
        }
        else
        {
            return new RequestStatus
            {
                Status = status,
                Message = Constants.DeleteClientFailed
            };
        }
    }

   








    #endregion

    #endregion
}
