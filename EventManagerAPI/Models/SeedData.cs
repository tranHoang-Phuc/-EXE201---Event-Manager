//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;

//namespace EventManagerAPI.Models
//{
//    public static class SeedData
//    {

//        public static void ApplySeedData(ModelBuilder modelBuilder)
//        {
//            // **📌 2. Seed EventCategory**
//            var categories = new List<EventCategory>
//            {
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "Seminar" },
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "Workshop" },
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "Entertainment" },
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "Team Building" },
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "Product Launch" },
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "Sports" },
//                new EventCategory { CategoryId = Guid.NewGuid(), CategoryName = "eSports" }
//            };
//            modelBuilder.Entity<EventCategory>().HasData(categories);

//            // **📌 3. Seed Events**
//            var events = new List<Event>
//            {
//                new Event { EventId = Guid.NewGuid(), EventName = "AI Conference", CategoryId = categories[0].CategoryId, OrganizerId = "478075a3-eeb0-4b8d-b41e-4fa89be24e0b", StartDate = new DateTime(2025, 3, 1), ParticipantCount = 100, EventCode="boinhangheo" },
//                new Event { EventId = Guid.NewGuid(), EventName = "Web Development Bootcamp", CategoryId = categories[1].CategoryId, OrganizerId = "a8d5aaf1-cb63-485d-9c9e-e32ea46a972c", StartDate = new DateTime(2025, 3, 5), ParticipantCount = 150, EventCode="deptraicogisai" },
//                new Event { EventId = Guid.NewGuid(), EventName = "Music Festival", CategoryId = categories[2].CategoryId, OrganizerId = "f9bab8f9-9a59-47cb-80a1-d14747c65e8b", StartDate = new DateTime(2025, 3, 10), ParticipantCount = 500, EventCode="khodai" },
//                new Event { EventId = Guid.NewGuid(), EventName = "Team Building Phu Quoc", CategoryId = categories[3].CategoryId, OrganizerId = "f9bab8f9-9a59-47cb-80a1-d14747c65e8b", StartDate = new DateTime(2025, 3, 15), ParticipantCount = 200, EventCode="baiduoi" },
//                new Event { EventId = Guid.NewGuid(), EventName = "Music Festival", CategoryId = categories[4].CategoryId, OrganizerId = "a8d5aaf1-cb63-485d-9c9e-e32ea46a972c", StartDate = new DateTime(2025, 3, 20), ParticipantCount = 50, EventCode="chimloi" }

//            };
//            modelBuilder.Entity<Event>().HasData(events);

//            // **📌 4. Seed Agendas**
//            var agendas = new List<Agenda>
//            {
//                new Agenda { AgendaId = Guid.NewGuid(), EventId = events[0].EventId, TimeStart = DateTime.UtcNow, TimeEnd = DateTime.UtcNow.AddHours(2), Description = "Keynote Speech" },
//                new Agenda { AgendaId = Guid.NewGuid(), EventId = events[1].EventId, TimeStart = DateTime.UtcNow, TimeEnd = DateTime.UtcNow.AddHours(3), Description = "Hands-on Coding" }
//            };
//            modelBuilder.Entity<Agenda>().HasData(agendas);

//            // **📌 5. Seed EventParticipants**
//            // **📌 5. Seed EventParticipants** 
//            var participants = new List<EventParticipant>
//            {
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[0].EventId, UserId = "478075a3-eeb0-4b8d-b41e-4fa89be24e0b", IsActive = true }, // User 1 tham gia AI Conference
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[0].EventId, UserId = "a8d5aaf1-cb63-485d-9c9e-e32ea46a972c", IsActive = false }, // User 2 tham gia AI Conference
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[1].EventId, UserId = "a8d5aaf1-cb63-485d-9c9e-e32ea46a972c", IsActive = true }, // User 2 tham gia Web Development Bootcamp
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[1].EventId, UserId = "f9bab8f9-9a59-47cb-80a1-d14747c65e8b", IsActive = false }, // User 3 tham gia Web Development Bootcamp
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[2].EventId, UserId = "478075a3-eeb0-4b8d-b41e-4fa89be24e0b", IsActive = false }, // User 1 tham gia Music Festival
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[2].EventId, UserId = "f9bab8f9-9a59-47cb-80a1-d14747c65e8b", IsActive = true }, // User 3 tham gia Music Festival
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[3].EventId, UserId = "a8d5aaf1-cb63-485d-9c9e-e32ea46a972c", IsActive = true }, // User 2 tham gia Team Building Phu Quoc
//                new EventParticipant { Id = Guid.NewGuid(), EventId = events[4].EventId, UserId = "f9bab8f9-9a59-47cb-80a1-d14747c65e8b", IsActive = true }  // User 3 tham gia Music Festival
//            };
//            modelBuilder.Entity<EventParticipant>().HasData(participants);
            
//        }

           
//    }
//}
