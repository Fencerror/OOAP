using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Library
    {
        public string Name {get; set;}
        public sring Address {get; set;}
        private List <Room> Rooms {get; set;}

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            Rooms = new List<Room>();
        }
    }

public class ReadingRoom
{
         public string Name { get; set; }
         private room room { get; set; }

    public ReadignRoom(string name)
    {
        Name = name;
        rooms = new Room();
    }

    public void AddRoom(Room r)
    {
        room.Add(r);
    }

    public void RemoveRoom(Room r)
    {
        room.Remove(r);
    }
        
    public List<rooms> GetRoom()
    {
        return room;
    }
}

public class LibraryStaff
{
    public string PhoneNumber {get; set; }
    
    private List<Reservation> Rezesvation {get; set;}

    public LibraryStaff (string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Reservations = new List<Reservation>();
    }
    
    public Reservation BookReservation(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        Reservation = new Reservation();
    }


    public void BookReservation(Reader r, string complaints, List<preferences> pref)
    {
        BookCopy = chooseBookCopy(BookID);
        room = readingRoom.getRoom();

        foreach (var bookCopy in BookCopies)
        {
            var isAvaible = libraryStaff.checkAvaibality();
            if (isAvaible == true)
            {
             // Создание нового объекта бронирования
                Reservation reservation = new (Reservation(r, bookCopy));
                {
                    ReservationId = new Random().Next(1000, 9999),
                    Reservable = bookCopy,
                    Reader = reader,
                    ReservationDate = DateTime.Now,
                    Book = new Book { Id = bookId, Title = "Пример Названия", Author = "Пример Автора" },
                    Seat = new Seat { SeatNumber = 1, IsAvailable = true }
                };
                
                // Обновление запроса на бронирование
                BookCopies.updateReservationRequest(reservation, bookCopy);
                
                return reservation;
            }
            else
            {
                throw new Exception("Книга недоступна для бронирования.");
            }
        }

    public bool CheckAvailability(List<Book> books)
    {
        Category cat = Category.getCategory(books);
        // Проходим по каждой копии
        if (cat.checkCategory = true)
        {
            foreach (var copy in copies)
         {
             // Проверяем, содержится ли копия в списке доступных книг
              if (books.Exists(b => b.Id == copy.Id))
             {
                 // Копия найдена, возвращаем true
                return true;
              }
          }

         // Копия не найдена, возвращаем false
          return false; 
        }

    }


 public BookCopy ReaderService(Reader reader)
        {
            id_res = identificationAnalysis(identification.string[*]);
                
            int readerId = reader.getID();
            
            string idRes = IdentificationAnalysis(readerId.ToString());
            

            Preferences preferences = Reader.GetReaderPreferences();
            
   
            BookCopy new_BookCopy = ReaderCheckUp(reader, preferences, idRes);
            

            if (newBookCopy != null)
            {
                reader.UpdatePreferences(preferences);
            }
            
            return newBookCopy;
        }

        private string IdentificationAnalysis(string identification)
        {
            // Анализ идентификации
            return "id_res";
        }

        
        private BookCopy ReaderCheckUp(Reader reader, Preferences preferences, string idRes)
        {
            // Проверка предпочтений читателя и поиск новой копии книги
            return new BookCopy { Id = 101, Title = "Пример Названия Копии", Author = "Пример Автора" };
        }



public class Reservation
{
    public int ReservationId { get; set; }
    public IReservable Reservable { get; set; }
    public Reader Reader { get; set; }
    public DateTime ReservationDate { get; set; }
    public Seat Seat { get; set; }

    public override string ToString()
    {
        return $"Reservation ID: {ReservationId}, Reader: {Reader.ID}, Date: {ReservationDate}, Seat: {Seat.SeatNumber}";
    }
}
        
public class BookCopy : IReservable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}

        
public class Reader
{
    public preferences {get; set; }
    public ID {get; set;}


    public getID()
    { 
        return ID
    }

    public getPreferences()
    {
        return new preferences;
    }

    
    public UpdatePreferences(preferences)
    {
        preferences = getPrefereces();
        return preferecnes;    
    }
}

        
public class Preferences
{
    public string preferences = reader.getReaderPreferences();

    public UpdateBookCopy(preferences)
    {
        return reader.getReaderPreferences();
    }
}

public class Room
{
    public string RoomNumber { get; set; }
}

        
public class Seat
{
    public int SeatNumber { get; set; }
    public bool IsAvailable { get; set; }
}
        
public interface IReservable
{
    bool IsAvailable();
}
